using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class cUiPopUp_Shop_Shop_Grid : MonoBehaviour {
        
    public string m_itemName = "인스펙터창에서 입력할것";
    public UILabel m_label;
    
    private void Awake()
    {        
        m_label.text = "[" + m_itemName.Remove(0, 4) + "]";        
        GetComponent<UITexture>().mainTexture = Resources.Load(cItem_Factory.MakePath(m_itemName)) as Texture;        
    }

    void OnClick()
    {        
        cEventListner.INSTANCE.Execute(cEventListner.EVENTKEY.cUiPopUp_Shop_Shop_Grid_Click, m_itemName);
    }    
}
