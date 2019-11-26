using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cStopWatch : MonoBehaviour {

    static cStopWatch m_instance;

    public static cStopWatch INSTANCE
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<cStopWatch>() as cStopWatch;
                if (m_instance == null)
                {
                    Debug.LogError("스탑와치 매니져 싱글톤 객체 생성이 안되었습니다.");
                }
            }
            return m_instance;
        }
    }
    
    public bool m_isStart = false;
    public float m_timer = 0.0f;
    public GameObject m_player;
    public UILabel m_label;
    public GameObject m_resultWindow;

    public GameObject m_3Dcamera;

    //타이머, 배경움직이는속도
    public int m_speed = 1;
   
    // Update is called once per frame
    void Update()
    {
        if (m_resultWindow.activeSelf == false)
        {
            m_label.GetComponent<UILabel>().text = "[ " + m_timer.ToString("N1") + " ]";

            if (m_isStart)
            {
                m_timer += m_speed * Time.deltaTime;
            }
        }        
    }



}
