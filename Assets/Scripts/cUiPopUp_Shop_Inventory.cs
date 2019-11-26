using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class cUiPopUp_Shop_Inventory : MonoBehaviour
{
    //인벤토리
    public List<cItem>  m_inventoryList;
    //플레이어 데이터의 아이템리스트
    public cItem[]      m_itemList;
   
        
    cUiItemList         m_uiItemList;

    public UITexture[] m_ArrItemList_Texture;
    public UILabel[] m_ArrItemList_Label;
    public UILabel[] m_ArrItemList_SkillLabel;

    public UILabel m_UiItemListLength;
    public UIScrollView m_scrollView;
    public UIGrid       m_grid;    
    public UILabel      m_numberOfUser;

    GameObject m_uiPurchasePrefab;
    GameObject m_uiNotifyPrefab;

    float   m_standard_posY = 0.0f;    
    int     m_FirstIndexByList = 0;    
    int     m_firstListCount = 0;
    int     m_gridCount = 8;
    
    private void Awake()
    {
        m_inventoryList = new List<cItem>();

        cObjectPool.INSTANCE.Setting(cPrefabManager.INSTANCE.FindPrefab("Prefab_Popup_InventoryListGrid").name, 10, cPrefabManager.INSTANCE.FindPrefab("Prefab_Popup_InventoryListGrid"));        
        m_inventoryList = cDataManager.INSTANCE.INVENTORY.m_inventoryList;
        m_itemList = cDataManager.INSTANCE.PLAYER.m_itemList;

        if (m_inventoryList.Count <= m_gridCount)
        {
            for (int i = 0; i < m_inventoryList.Count; i++)
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


        m_uiItemList = new cUiItemList();
        m_uiItemList.SetData(m_ArrItemList_SkillLabel,m_ArrItemList_Label, m_ArrItemList_Texture);

        m_firstListCount = m_inventoryList.Count;
        ChangeNumberOfFriend();


        UIScrollView.m_offsetX = 0.0f;

        m_grid.Reposition();
        m_scrollView.ResetPosition();                
        m_standard_posY = m_scrollView.transform.localPosition.x;        
        GridReChangeData(0);

        m_uiItemList.UI_ItemList_ChangeData();
        m_UiItemListLength.text = "[ " + cDataManager.INSTANCE.PLAYER.ReturnItemListLength().ToString() + " ]";
        
        cEventListner.INSTANCE.Register(cEventListner.EVENTKEY.cUiItemList_Info_Click, ItemListEvent);
        cEventListner.INSTANCE.Register(cEventListner.EVENTKEY.cUiPopUp_Shop_Inventory_Grid_Click, InventoryEvent);
        cEventListner.INSTANCE.Register(cEventListner.EVENTKEY.cUiPopUp_Shop_Shop_Grid_Click, ShopEvent);        
    }

   public void ShopEvent(object sender, System.EventArgs e)
    {
        if (m_uiPurchasePrefab == null)
        {
            m_uiPurchasePrefab = cPrefabManager.INSTANCE.MakePrefab("Prefab_Popup_Purchase", "UI Root");
            m_uiPurchasePrefab.GetComponent<cUiPopUp_Purchase>().m_itemName = (sender as string);
            m_uiPurchasePrefab.GetComponent<cUiPopUp_Purchase>().TextReset();
            m_uiPurchasePrefab.GetComponent<cUiPopUp_Purchase>().m_Inventory = this;
            m_uiPurchasePrefab.GetComponent<cUiPopUp>().FadeIn();
        }
        else
        {
            m_uiPurchasePrefab.GetComponent<cUiPopUp_Purchase>().m_itemName = (sender as string);
            m_uiPurchasePrefab.GetComponent<cUiPopUp_Purchase>().TextReset();
            m_uiPurchasePrefab.GetComponent<cUiPopUp>().FadeIn();
        }        
    }

    public void InventoryEvent(object sender, System.EventArgs e)
    {
        DeleteByInventoryToItemList((int)sender);
        m_UiItemListLength.text = "[ " + cDataManager.INSTANCE.PLAYER.ReturnItemListLength().ToString() + " ]";

    }

    public void ItemListEvent(object sender, System.EventArgs e)
    {
        AddToInventoryByItemList((int)sender);
        m_UiItemListLength.text = "[ " + cDataManager.INSTANCE.PLAYER.ReturnItemListLength().ToString() + " ]";
        m_scrollView.ResetPosition();
    }

    void GridReChangeData(int _listStartIndex)
    {
        cUiPopUp_Shop_Inventory_Grid[] visibleList = m_grid.transform.GetComponentsInChildren<cUiPopUp_Shop_Inventory_Grid>();
        int tmpData = _listStartIndex;
        foreach (cUiPopUp_Shop_Inventory_Grid on in visibleList)
        {
            if (on.gameObject.activeSelf)
            {
                on.SetData(m_inventoryList[tmpData], tmpData);
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
        GameObject tmpObj = cObjectPool.INSTANCE.GetObject(cPrefabManager.INSTANCE.FindPrefab("Prefab_Popup_InventoryListGrid").name, cPrefabManager.INSTANCE.FindPrefab("Prefab_Popup_InventoryListGrid"));        
        tmpObj.transform.SetParent(m_grid.transform);
        tmpObj.transform.localPosition = Vector3.zero;
        tmpObj.transform.localRotation = Quaternion.identity;
        tmpObj.transform.localScale = Vector3.one;
        tmpObj.layer = m_grid.gameObject.layer;
        tmpObj.SetActive(true);
    }
    void ObjectPoolGridPull(int _index)
    {
        cObjectPool.INSTANCE.ReturnObject(cPrefabManager.INSTANCE.FindPrefab("Prefab_Popup_InventoryListGrid").name, m_grid.GetChild(_index).gameObject);
    }

    void DeleteByInventoryToItemList(int _index)
    {
        //그리도 최소갯수보다 작은경우
        if (m_inventoryList.Count <= m_gridCount)
        {
            //if (m_inventoryList[_index].m_name.Equals("아이템_체력") || m_inventoryList[_index].m_name.Equals("아이템_공격력"))
            //{
            //    m_inventoryList[_index].Use();
            //    ObjectPoolGridPull(_index);
            //    MakePopUpNotify("[ "+m_inventoryList[_index].m_name.Remove(0, 4)+" ]" + "\n아이템을 사용하셨습니다. ");
            //    m_grid.Reposition();
            //    ResetGridDisplay();
            //    ChangeNumberOfFriend();
            //    return;
            //}
            if (InventoryToItemList(_index))
            {
                ObjectPoolGridPull(_index);
                m_uiItemList.UI_ItemList_ChangeData();
            }
            else
            {
                MakePopUpNotify("아이템 목록이\n초과 되었습니다.");
            }
        }
        else
        {
            //if (m_inventoryList[_index].m_name.Equals("아이템_체력") || m_inventoryList[_index].m_name.Equals("아이템_공격력"))
            //{
            //    m_inventoryList[_index].Use();
            //    if (m_FirstIndexByList > 0)
            //    {
            //        m_FirstIndexByList--;
            //    }
            //    MakePopUpNotify("[ " + m_inventoryList[_index].m_name.Remove(0, 4) + " ]" + "\n아이템을 사용하셨습니다. ");
            //    m_grid.Reposition();
            //    ResetGridDisplay();
            //    ChangeNumberOfFriend();
            //    return;
            //}
            if (InventoryToItemList(_index))
            {
                if (m_FirstIndexByList > 0)
                {
                    m_FirstIndexByList--;
                }
                m_uiItemList.UI_ItemList_ChangeData();
            }
            else
            {
                MakePopUpNotify("아이템 목록이\n초과 되었습니다.");
            }
        }
        m_grid.Reposition();
        ResetGridDisplay();
        ChangeNumberOfFriend();
    }

    void AddToInventoryByItemList(int _index)
    {
        //그리도 최소갯수보다 작은경우
        if (m_inventoryList.Count < m_gridCount)
        {
            ItemListToInventory(_index);
            ObjectPoolGridPush();
            m_uiItemList.UI_ItemList_ChangeData();
        }
        else
        {
            ItemListToInventory(_index);
            m_FirstIndexByList++;
            m_uiItemList.UI_ItemList_ChangeData();
        }
        m_grid.Reposition();
        ResetGridDisplay();
        ChangeNumberOfFriend();
    }

   public void AddByShopToInventory(string _itemName)
    {
        //그리도 최소갯수보다 작은경우
        if (m_inventoryList.Count < m_gridCount)
        {
            m_inventoryList.Add(cItem_Factory.MakeItem(_itemName));
            ObjectPoolGridPush();
        }
        else
        {
            m_inventoryList.Add(cItem_Factory.MakeItem(_itemName));
            m_FirstIndexByList++;
            m_uiItemList.UI_ItemList_ChangeData();
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
    void PannelMoveByObjPool()
    {
        if (m_inventoryList.Count <= m_gridCount)
        {
            return;
        }
        //첫 구간
        if (m_FirstIndexByList == 0)
        {
            if (m_scrollView.transform.localPosition.x < m_standard_posY - m_grid.cellWidth)
            {
                m_FirstIndexByList++;
                UIScrollView.m_offsetX = m_grid.cellWidth;
                ObjectPoolGridPull(0);
                ObjectPoolGridPush();
                m_grid.Reposition();
                m_scrollView.ResetPosition();
                ResetGridDisplay();
            }
        }
        //중간구간
        else if (m_FirstIndexByList > 0 && m_FirstIndexByList < m_inventoryList.Count - (m_gridCount - 1))
        {
            if (m_scrollView.transform.localPosition.x < m_standard_posY - m_grid.cellWidth * 2)
            {
                m_FirstIndexByList++;
                ObjectPoolGridPull(0);
                ObjectPoolGridPush();
                m_grid.Reposition();
                m_scrollView.ResetPosition();
                ResetGridDisplay();
            }
            else if (m_scrollView.transform.localPosition.x > m_standard_posY - 1.0f)
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
            if (m_scrollView.transform.localPosition.x > m_standard_posY - 1.0f)
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

    bool InventoryToItemList(int _index)
    {
        for (int i = 0; i < m_itemList.Length; i++)
        {
            if (m_itemList[i] == null)
            {
                m_itemList[i] = cItem_Factory.MakeItem(m_inventoryList[_index].m_name);
                m_inventoryList.RemoveAt(_index);
                return true;
            }
        }
        return false;
    }

    void ItemListToInventory(int _index)
    {
        m_inventoryList.Add(cItem_Factory.MakeItem(m_itemList[_index].m_name));
        m_itemList[_index] = null;
    }

    public void MakePopUpNotify(string _msg)
    {
        if (m_uiNotifyPrefab == null)
        {
            m_uiNotifyPrefab = cPrefabManager.INSTANCE.MakePrefab("Prefab_Popup_Notify", "UI Root");
            m_uiNotifyPrefab.GetComponent<cUiPopUp_Notify>().TextReset(_msg);
            m_uiNotifyPrefab.GetComponent<cUiPopUp_Notify>().FadeIn();
        }
        else
        {
            m_uiNotifyPrefab.GetComponent<cUiPopUp_Notify>().TextReset(_msg);
            m_uiNotifyPrefab.GetComponent<cUiPopUp_Notify>().FadeIn();
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

    //public void ButtonLvSortUp()
    //{
    //    cSort sort = new cSortList<cUserInfo>(m_userList, SortByLevel);
    //    sort.Ascending();
    //    ResetGridDisplay();
    //}
    //public void ButtonLvSortDown()
    //{
    //    cSort sort = new cSortList<cUserInfo>(m_userList, SortByLevel);
    //    sort.Descending();
    //    ResetGridDisplay();
    //}
    //public void ButtonTimeSortUp()
    //{
    //    cSort sort = new cSortList<cUserInfo>(m_userList, SortByTime);
    //    sort.Ascending();
    //    ResetGridDisplay();
    //}
    //public void ButtonTimeSortDown()
    //{
    //    cSort sort = new cSortList<cUserInfo>(m_userList, SortByTime);
    //    sort.Descending();
    //    ResetGridDisplay();
    //}


    //기타 기능
    //=====================================================================================================================================

    void ChangeNumberOfFriend()
    {
        m_numberOfUser.GetComponent<UILabel>().text ="[ " + m_inventoryList.Count.ToString() + " ]";
    }

}
