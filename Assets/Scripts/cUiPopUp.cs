using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cUiPopUp : MonoBehaviour {

    float       m_duration = 0.2f;                                //몇초 안에 완료할지    
    Vector3     m_toScale = new Vector3(1.0f, 1.0f, 1.0f);        //어디까지 커질지
    Vector3     m_fromScale = Vector3.zero;                       //원본 스케일
    TweenScale  m_ts;
    public GameObject  m_popup;
    public GameObject  m_popupBack;
             
   public void FadeIn()
    {
        if (m_ts)
        {
            m_ts.onFinished.Clear();
        }
        m_popup.SetActive(true);
        m_popupBack.SetActive(true);
        m_popup.transform.localScale = m_fromScale;
        m_ts = TweenScale.Begin(m_popup, m_duration, m_toScale);
        m_ts.SetOnFinished
           (
              delegate ()
              {
                  m_popup.transform.localScale = Vector3.one;
                  m_popup.SetActive(true);
              }
           );
    }

    public void FadeOut()
    {
        m_popupBack.SetActive(false);
        m_ts = TweenScale.Begin(m_popup, m_duration, m_fromScale);
        m_ts.SetOnFinished
            (
               delegate ()
               {
                   m_popup.transform.localScale = Vector3.zero;
                   m_popup.SetActive(false);
               }
            );
    }
}
