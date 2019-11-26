using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cUnit_LightAttack : cUnit
{

    float m_graity = 0.0f;


    private void Awake()
    {
        m_graity = 0.0f;
        Init(100.0f, 1.5f, 50.0f);
        m_camera = new cCheckOutCamera(gameObject);
    }


    private void OnEnable()
    {
        m_graity = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        m_graity -= 0.2f;
        transform.Translate((Vector3.down * m_speed + new Vector3(0, m_graity, 0)) * Time.deltaTime, Space.World);

        m_camera.CheckOutCamera("Prefab_Unit_LightMinionAttack");
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.tag.Equals("TAG_PLAYER")) col.gameObject.GetComponent<cUnit>().GetDamage(m_damage, 1.0f);

    }

    public override void Init(float _hp, float _speed, float _damage)
    {
        m_hp = _hp;
        m_speed = _speed;
        m_damage = _damage;
    }

    public override void Die()
    {

    }
}
