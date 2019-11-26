using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cUiCheckBox : MonoBehaviour {

    UIToggle m_toggle;
    public cUiScrollBar m_scroll;

    // Use this for initialization
    void Start () {
        m_toggle = GetComponent<UIToggle>();
        m_toggle.onChange.Add(new EventDelegate(this, "SoundApplication"));
	}

    public void SoundApplication()
    {
        if (m_toggle.value == true) m_scroll.m_scroll.value = 0.0f;
    }	
}
