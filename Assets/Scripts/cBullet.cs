using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class cBullet : MonoBehaviour {

    public float m_speed = 0.0f;
    public float m_damage = 0.0f;
    public Vector3 m_DirectionVec;
    public cCheckOutCamera m_camera = null;

    private void Awake()
    {        
        m_speed = 15.0f;
    }
    
    public abstract void Setting(Transform _startPos, GameObject _target, float _speed, float _damage, string _prfabName = null);

    public virtual void Move()
    {                
        transform.Translate(m_DirectionVec * m_speed * Time.deltaTime);
    }        
}
