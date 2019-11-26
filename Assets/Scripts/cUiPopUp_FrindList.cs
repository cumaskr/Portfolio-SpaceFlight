using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class cUiPopUp_FrindList : cUiPopUp
{    
    public List<cUserInfo>  m_userList;
    public UIScrollView     m_scrollView;
    public UIGrid           m_grid;        
    public UILabel          m_numberOfUser;
    
    
    // 드래그를 위냐?아래냐? 판단하는 기준점
    float m_standard_posY = 0.0f;
    // 유저리스트의 몇번째부터 디스플레이 할거냐?
    int m_FirstIndexByList = 0;
    // 처음 유저리스트 인원
    int m_firstFriendCount = 0;
    
    int m_gridCount = 6;
        
    private void Start()
    {
        m_userList = new List<cUserInfo>();

        cObjectPool.INSTANCE.Setting(cPrefabManager.INSTANCE.FindPrefab("Prefab_Popup_FriendListGrid").name, 10, cPrefabManager.INSTANCE.FindPrefab("Prefab_Popup_FriendListGrid"));

        for (int i = 0; i < 1000000; i++)
        {
            cUserInfo tmpUser = new cUserInfo();
            tmpUser.m_name = "Name" + i.ToString();
            tmpUser.m_level = i;
            tmpUser.m_time = Random.Range(0, 10);
            m_userList.Add(tmpUser);
        }

        if (m_userList.Count <= m_gridCount)
        {
            for (int i = 0; i < m_userList.Count; i++)
            {
                ObjectPoolGridPush();
            }
        }
        else
        {
            for (int i = 0; i < m_gridCount; i++)
            {
                ObjectPoolGridPush();
            }
        }

        m_FirstIndexByList = 0;
        m_firstFriendCount = m_userList.Count;
        ChangeNumberOfFriend();
        
        UIScrollView.m_offsetY = 0.0f;
        m_grid.Reposition();
        m_scrollView.ResetPosition();

        

        m_standard_posY = m_scrollView.transform.localPosition.y;
        GridReChangeData(0);

        cEventListner.INSTANCE.Register(cEventListner.EVENTKEY.cUiPopUp_FrindList_Grid_DeleteClick, DeleteEvent);
    }


    void GridReChangeData(int _userStartIndex)
    {
        cUiPopUp_FrindList_Grid[] visibleList = m_grid.transform.GetComponentsInChildren<cUiPopUp_FrindList_Grid>();
        int tmpData = _userStartIndex;
        foreach (cUiPopUp_FrindList_Grid on in visibleList)
        {
            if (on.gameObject.activeSelf)
            {
                on.SetData(m_userList[tmpData], tmpData);
                tmpData++;
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {             
        PannelMoveByObjPool();
    }

    

    void ObjectPoolGridPush()
    {
        GameObject tmpObj = cObjectPool.INSTANCE.GetObject(cPrefabManager.INSTANCE.FindPrefab("Prefab_Popup_FriendListGrid").name, cPrefabManager.INSTANCE.FindPrefab("Prefab_Popup_FriendListGrid"));        
        tmpObj.transform.SetParent(m_grid.transform);
        tmpObj.transform.localPosition = Vector3.zero;
        tmpObj.transform.localRotation = Quaternion.identity;
        tmpObj.transform.localScale = Vector3.one;
        tmpObj.layer = m_grid.gameObject.layer;
        tmpObj.SetActive(true);
    }
    void ObjectPoolGridPull(int _index)
    {
        cObjectPool.INSTANCE.ReturnObject(cPrefabManager.INSTANCE.FindPrefab("Prefab_Popup_FriendListGrid").name, m_grid.GetChild(_index).gameObject);
    }

    public void DeleteEvent(object index, System.EventArgs e)
    {
        int _index = (int)index;
        //그리도 최소갯수보다 작은경우
        if (m_userList.Count <= m_gridCount)
        {
            m_userList.RemoveAt(_index);
            ObjectPoolGridPull(_index);            
        }
        else
        {
            m_userList.RemoveAt(_index);
            if (m_FirstIndexByList > 0)
            {
                m_FirstIndexByList--;
            }                        
        }
        m_grid.Reposition();
        ResetGridDisplay();
        ChangeNumberOfFriend();
    }
    void ResetGridDisplay()
    {       
        //처음부분
        if (m_FirstIndexByList == 0)
        {
            GridReChangeData(0);
        }                      
        else
        {
            GridReChangeData(m_FirstIndexByList - 1);
        }
    }

    //패널 드래그 + 오브젝트풀 연관 기능
    //=====================================================================================================================================
    public void PannelMoveByObjPool()
    {        
        if (m_userList.Count <= m_gridCount) return;
       
        //첫 구간
        if (m_FirstIndexByList == 0)
        {
            if (m_scrollView.transform.localPosition.y > m_standard_posY + m_grid.cellHeight)
            {                
                m_FirstIndexByList++;                               
                UIScrollView.m_offsetY = -m_grid.cellHeight;
                ObjectPoolGridPull(0);
                ObjectPoolGridPush();
                m_grid.Reposition();
                m_scrollView.ResetPosition();
                ResetGridDisplay();
            }
        }
        //중간구간
        else if (m_FirstIndexByList > 0 && m_FirstIndexByList < m_userList.Count - (m_gridCount - 1))
        {
            if (m_scrollView.transform.localPosition.y > m_standard_posY + m_grid.cellHeight * 2)
            {
                m_FirstIndexByList++;                
                ObjectPoolGridPull(0);
                ObjectPoolGridPush();
                m_grid.Reposition();
                m_scrollView.ResetPosition();
                ResetGridDisplay();
            }
            if (m_scrollView.transform.localPosition.y < m_standard_posY)
            {                
                m_FirstIndexByList--;                
                if (m_FirstIndexByList == 0) return;

                ObjectPoolGridPull(0);
                ObjectPoolGridPush();
                m_grid.Reposition();
                m_scrollView.ResetPosition();
                ResetGridDisplay();
            }
        }
        //끝구간
        else
        {            
            if (m_scrollView.transform.localPosition.y < m_standard_posY)
            {
                m_FirstIndexByList--;
                ObjectPoolGridPull(0);
                ObjectPoolGridPush();
                m_grid.Reposition();
                m_scrollView.ResetPosition();
                ResetGridDisplay();
            }            
        }
    }

    //정렬관련 기능
    //=====================================================================================================================================
    bool SortByLevel(cUserInfo _a, cUserInfo _b)
    {
        if (_a.m_level > _b.m_level)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    int QuiqSort(cUserInfo _a, cUserInfo _b)
    {
        if (_a.m_level > _b.m_level)
        {
            return 1;
        }
        else if(_a.m_level < _b.m_level)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }


    int QuiqSortDown(cUserInfo _a, cUserInfo _b)
    {
        if (_a.m_level > _b.m_level)
        {
            return -1;
        }
        else if (_a.m_level < _b.m_level)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }



    bool SortByTime(cUserInfo _a, cUserInfo _b)
    {
        if (_a.m_time > _b.m_time)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ButtonLvSortUp()
    {
        cSort sort = new cSortList<cUserInfo>(m_userList, SortByLevel);
        m_userList.Sort(QuiqSort);
        //sort.Ascending();
        ResetGridDisplay();
    }
    public void ButtonLvSortDown()
    {
        cSort sort = new cSortList<cUserInfo>(m_userList, SortByLevel);
        m_userList.Sort(QuiqSortDown);
        //sort.Descending();
        ResetGridDisplay();
    }
    public void ButtonTimeSortUp()
    {
        cSort sort = new cSortList<cUserInfo>(m_userList, SortByTime);
        sort.Ascending();
        ResetGridDisplay(); 
    }
    public void ButtonTimeSortDown()
    {
        cSort sort = new cSortList<cUserInfo>(m_userList, SortByTime);
        sort.Descending();
        ResetGridDisplay(); 
    }

    
    //기타 기능
    //=====================================================================================================================================
    public void ChangeNumberOfFriend()
    {
        m_numberOfUser.GetComponent<UILabel>().text = "[ " + m_userList.Count.ToString() + " 명]";
    }





}
