using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cBullet_Default : cBullet
{
    GameObject m_target;
    string m_prefabName;

    private void Awake()
    {
        m_camera = new cCheckOutCamera(gameObject);
    }

    public override void Setting(Transform _startPos, GameObject _target, float _speed, float _damage, string _prfabName = null)
    {
        m_target = _target;
        m_speed = _speed;
        m_damage = _damage;
        transform.localPosition = _startPos.position;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;
        m_prefabName = _prfabName;
    }

    //총알이 발사될때 방향
    private void OnEnable()
    {
        if (m_target == null)
        {
            m_DirectionVec = Vector3.up;
        }
        else
        {
            ResetDirection();
        }
    }

    void ResetDirection()
    {
        m_DirectionVec = (m_target.transform.position - transform.position).normalized;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        m_camera.CheckOutCamera(m_prefabName);        
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag.Equals("TAG_ITEM") || col.gameObject.tag.Equals("TAG_LIGHT")) return;
        if (col.gameObject.tag.Equals("TAG_ENEMY_BOSS"))
        {
            if (col.gameObject.GetComponent<cUnit_Boss>().m_isOn == false) return;            
        }
        //부딪힌 유닛에게 대미지를 준다.
        col.gameObject.GetComponent<cUnit>().GetDamage(m_damage,0.0f);
        //이펙트 생성
        GameObject tmpEffect = cObjectPool.INSTANCE.GetObject(cPrefabManager.INSTANCE.FindPrefab("Prerfab_Effect_Blood").name, cPrefabManager.INSTANCE.FindPrefab("Prerfab_Effect_Blood"));
        tmpEffect.GetComponent<cObjectPool_Effect>().Setting(col.contacts[0].point, "Prerfab_Effect_Blood");
        //총알 반납
        cObjectPool.INSTANCE.ReturnObject(m_prefabName, gameObject);
    }    
}
