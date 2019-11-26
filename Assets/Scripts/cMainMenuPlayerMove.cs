using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cMainMenuPlayerMove : MonoBehaviour {


    public Vector3[] m_positon;
    int m_index;
    float m_posT;
    float m_rotT;
    Vector3 m_forward;

    // Use this for initialization
    void Start () {
        m_index = 0;
        m_posT = 0.0f;
        m_rotT = 0.0f;
        StartCoroutine("ChanePosition");
    }
	
	// Update is called once per frame
	void Update () {


        //Vector3.Lerp()        

    }

    IEnumerator ChanePosition()
    {
        StartCoroutine("ChaneDirection");
        while (true)
        {
            yield return new WaitForSeconds(0.001f);
            m_posT+=0.002f;
            if (m_posT >= 1)
            {
                m_posT = 0;
                m_index++;
                if (m_index >= m_positon.Length - 1) m_index = 0;
                StartCoroutine("ChaneDirection");               
            }
            transform.localPosition = Vector3.Lerp(m_positon[m_index], m_positon[m_index + 1], m_posT);
        }        
    }

    IEnumerator ChaneDirection()
    {
        m_forward = transform.up;
        while (true)
        {
            yield return new WaitForSeconds(0.001f);
            m_rotT +=0.02f;
            if (m_rotT >= 1)
            {
                m_forward = transform.up;
                m_rotT = 0;
                yield break;
            }
            transform.up = Vector3.Lerp(m_forward, (m_positon[m_index] - m_positon[m_index + 1]).normalized, m_rotT);
        }        
    }


}
