using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cInGame_BackGround : MonoBehaviour {
    
    public GameObject[]     m_backList;
    public GameObject[]     m_midddlebackList;
    public GameObject[]     m_frontbackList;
    
    float m_backSpeed   = 30.0f;
    float m_middleSpeed = 60.0f;
    float m_bigSpeed    = 120.0f;
    float m_size        = 0.0f;
    int   m_activeSkillSpeed = 10;
   

	// Use this for initialization
	void Start () {
        m_size = m_backList[0].GetComponent<UITexture>().localSize.y;        
    }

    public void BackGroundScrolling(GameObject[] _objList, float _size, float _speed)
    {        
        if (_objList[0].transform.localPosition.y <= -_size)
        {
            StartCoroutine("ChangeBakGround", _objList);                        
        }
        for (int i = 0; i < _objList.Length; i++)
        {
            int nCalcSpeed = cStopWatch.INSTANCE.m_speed * m_activeSkillSpeed;
            if (cStopWatch.INSTANCE.m_speed == 1) nCalcSpeed = 1;            
            _objList[i].gameObject.transform.localPosition -= new Vector3(0, _speed * nCalcSpeed * Time.deltaTime, 0);
        }
    }

    IEnumerator ChangeBakGround(GameObject[] _objList)
    {
        yield return new WaitForEndOfFrame();
        _objList[0].transform.localPosition = _objList[1].transform.localPosition + new Vector3(0, m_size - 1, 0);
        GameObject tmpObj = _objList[1];
        _objList[1] = _objList[0];
        _objList[0] = tmpObj;
        yield break; 
    }

	// Update is called once per frame
	void Update () {
        if (cStopWatch.INSTANCE.m_isStart)
        {
            BackGroundScrolling(m_backList, m_size, m_backSpeed);
            BackGroundScrolling(m_midddlebackList, m_size, m_middleSpeed);
            BackGroundScrolling(m_frontbackList, m_size, m_bigSpeed);
        }
    }
}
