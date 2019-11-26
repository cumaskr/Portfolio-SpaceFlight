using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cUiPopUp_Notify : cUiPopUp {

    public UILabel m_mainText;

     public void TextReset(string _msg)
    {
        m_mainText.text = _msg;
    }    
}
