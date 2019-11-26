using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cUiMatoolButton : MonoBehaviour {

    public GameObject   m_onOff;
    public GameObject[] m_score;
    public int          m_SceneNumber = -1;
    public UILabel      m_label;

    private void Start()
    {
        m_label.text = (m_SceneNumber + 1).ToString();        
    }

    void OnClick()
    {
        cEventListner.INSTANCE.Execute(cEventListner.EVENTKEY.cUiMaptoolButton_Click, m_SceneNumber);
    }

    public void SetOnOFf(bool _isOn)
    {
        //자물쇠 이미지는 반대로!!
        if (_isOn)
        {
            gameObject.GetComponent<BoxCollider>().enabled = true;
            m_onOff.SetActive(false);            
        }
        else
        {
            m_onOff.SetActive(true);
            gameObject.GetComponent<BoxCollider>().enabled = false;
        } 
    }
    public void SetScore(int _score)
    {
        int tmpIndex = 0;
        for (int i = 0; i < _score; i++)
        {
            m_score[tmpIndex++].SetActive(true);
        }        
    }    
}
