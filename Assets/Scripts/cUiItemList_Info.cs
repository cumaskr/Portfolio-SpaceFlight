using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class cUiItemList_Info : MonoBehaviour {
        
    public int m_index = -1;
    
    void OnClick()
    {        
        cEventListner.INSTANCE.Execute(cEventListner.EVENTKEY.cUiItemList_Info_Click, m_index);
    }
    
}
