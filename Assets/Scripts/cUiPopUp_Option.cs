using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cUiPopUp_Option : cUiPopUp {
    
    public void Quit()
    {        
        if (Time.timeScale != 1.0f) Time.timeScale = 1.0f;
        FadeOut();
    }	
}
