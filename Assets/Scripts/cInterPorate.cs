using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cInterPorate : MonoBehaviour {

    public Vector3[] m_VectorList;
    float m_t = 0.0f;

    private void OnEnable()
    {
        transform.localPosition = m_VectorList[0];
    }

    public Vector3 InterPolate(Vector3 _v1, Vector3 _v2, Vector3 _v3, float _t)
    {
        return Mathf.Pow((1 - _t), 2) * _v1 + 2 * _t * (1 - _t) * _v2 + Mathf.Pow(_t, 2) * _v3;
    }

    IEnumerator InterpolateStart()
    {
        while (m_t <= 1.0f)
        {
            transform.localPosition = InterPolate(m_VectorList[0], m_VectorList[1], m_VectorList[2], m_t);
            m_t += 0.003f;
            yield return new WaitForSeconds(0.01f);
        }
        yield break;        
    }    
}
