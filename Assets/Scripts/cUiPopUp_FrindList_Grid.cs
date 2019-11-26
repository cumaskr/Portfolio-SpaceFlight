using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cUiPopUp_FrindList_Grid : MonoBehaviour {

    public UILabel[] m_labelList;
            
    public int m_index;
    
    public void SetData(cUserInfo _UserInfo, int _index)
    {
        if (m_labelList.Length != 3) Debug.LogError("라벨 셋팅이 잘못되었습니다.");
        m_index = _index;
        m_labelList[0].text = _UserInfo.m_name;
        m_labelList[1].text = _UserInfo.m_level.ToString(); ;
        m_labelList[2].text ="[" + _UserInfo.m_time.ToString() + "초전]";        
    }
    
    void DeleteEvent()
    {
        cEventListner.INSTANCE.Execute(cEventListner.EVENTKEY.cUiPopUp_FrindList_Grid_DeleteClick, m_index);
    }

}
