using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cUiScrollBar_Touch : cUiScrollBar {

    public void TouchApplication()
    {
        m_label.text = m_scroll.value.ToString("N1");
        cDataManager.INSTANCE.PLAYER.m_deltaToch = System.Single.Parse(m_scroll.value.ToString("N1"));        
    }
    
	// Use this for initialization
	void Start () {

        m_scroll.value = cDataManager.INSTANCE.PLAYER.m_deltaToch;
        m_scroll.onChange.Add(new EventDelegate(this, "TouchApplication"));
    }		
}
