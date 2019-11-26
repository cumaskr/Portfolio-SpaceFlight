using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cUiMenu : MonoBehaviour{

    //UICamera m_camera;
    float m_duration = 0.2f;                                //몇초 안에 완료할지    
    Vector3 m_toScale = new Vector3(1.2f, 1.2f, 1.2f);      //어디까지 커질지
    Vector3 m_fromScale = Vector3.one;                      //원본 스케일
    

    private void Start()
    {
        //cEventListner.INSTANCE.Register(gameObject.name, TargetSetting);
    }

    private void OnDisable()
    {
        //cEventListner.INSTANCE.Remove(gameObject.name);
    }


    void OnHover(bool _isIn)
    {
        if (_isIn)
        {
            //Debug.Log("인");
            TweenScale.Begin(gameObject, m_duration, m_toScale);            
        }
        else
        {
            //Debug.Log("아웃");
            TweenScale.Begin(gameObject, m_duration, m_fromScale);                        
        }        
    }
    
    void OnPress(bool _isPressed)
    {
        if (_isPressed)
        {
            TweenColor.Begin(gameObject, m_duration, Color.gray);            
        }
        else
        {
            TweenColor.Begin(gameObject, m_duration, Color.white);
        }
    }

    
        
}
