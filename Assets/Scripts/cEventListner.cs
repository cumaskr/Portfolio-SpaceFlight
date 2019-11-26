using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class cEventListner : MonoBehaviour {

    static cEventListner m_instance;

    public static cEventListner INSTANCE
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<cEventListner>() as cEventListner;
                if (m_instance == null)
                {
                    Debug.LogError("이벤트 매니져 싱글톤 객체 생성이 안되었습니다.");
                }
            }
            return m_instance;
        }
    }

    public enum EVENTKEY
    {
        cUiPopUp_Shop_Inventory_Grid_Click = 0,
        cUiPopUp_Shop_Shop_Grid_Click,
        cUiItemList_Info_Click,
        cUiPopUp_FrindList_Grid_DeleteClick,
        cUiMaptoolButton_Click,
    }
    
    Dictionary<EVENTKEY, System.EventHandler> m_eventListner = new Dictionary<EVENTKEY, System.EventHandler>();
    
    public void Register(EVENTKEY _eventName, System.EventHandler _del)
    {
        if (m_eventListner.ContainsKey(_eventName))
        {
            m_eventListner.Remove(_eventName);
            m_eventListner[_eventName] = _del;
            return;
        }
        if (m_eventListner.ContainsKey(_eventName) == false) m_eventListner.Add(_eventName, _del);
    }

    public void Remove(EVENTKEY _eventName)
    {
        if (m_eventListner.ContainsKey(_eventName)) m_eventListner.Remove(_eventName);
    }
    
    public void Execute(EVENTKEY _eventName, object _objects)
    {        
        if (m_eventListner.ContainsKey(_eventName)) m_eventListner[_eventName].Invoke(_objects, null);
    }

}
