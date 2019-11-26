using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cUiScrollBar : MonoBehaviour {
    
    public UIScrollBar m_scroll;

    public UILabel m_label;
    
    void Start()
    {
        m_scroll = GetComponent<UIScrollBar>();        
    }
    
}
