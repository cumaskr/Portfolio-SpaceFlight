using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cUnit_Minion : cUnit {

    float   m_rotSpeed = 10.0f;
    public Vector3 m_direction = Vector3.down;

    private void Awake()
    {
        Init(100.0f, 3.3f, 25.0f);
        m_camera = new cCheckOutCamera(gameObject);
    }


    private void OnEnable()
    {
        m_firstColor = new Color(1, 1, 1);
        GetComponent<SpriteRenderer>().color = m_firstColor;
    }

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {                

        transform.Rotate(Vector3.forward* m_rotSpeed * Time.deltaTime);

        transform.Translate(m_direction * m_speed * Time.deltaTime * cStopWatch.INSTANCE.m_speed , Space.World);
        m_camera.CheckOutCamera("Prefab_Unit_Minion");
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.tag.Equals("TAG_POWER")) return;
        col.gameObject.GetComponent<cUnit>().GetDamage(m_damage,1.0f);
    }

    public override void Init(float _hp, float _speed, float _damage)
    {
        m_hp = _hp;
        m_speed = _speed;
        m_damage = _damage;
        int tmpRnd = UnityEngine.Random.Range(-1, 1);
        if (tmpRnd == 0) m_rotSpeed *= 1;
        else m_rotSpeed *= -1;
        m_firstColor = new Color(1,1,1);
        GetComponent<SpriteRenderer>().color = m_firstColor;
    }

    public override void Die()
    {
        if (!m_isDie)
        {
            m_isDie = true;
            GameObject tmpEffect = cObjectPool.INSTANCE.GetObject(cPrefabManager.INSTANCE.FindPrefab("Prerfab_Effect_Rock").name, cPrefabManager.INSTANCE.FindPrefab("Prerfab_Effect_Rock"));
            tmpEffect.GetComponent<cObjectPool_Effect>().Setting(transform.localPosition, "Prerfab_Effect_Rock");


            int nRnd = UnityEngine.Random.Range(0, 2);
            if (nRnd == 0)
            {
                GameObject tmpObj = cObjectPool.INSTANCE.GetObject(cPrefabManager.INSTANCE.FindPrefab("Prefab_Unit_Item").name, cPrefabManager.INSTANCE.FindPrefab("Prefab_Unit_Item"));
                tmpObj.GetComponent<cUnit_Item>().Setting("아이템_골드");
                tmpObj.transform.localPosition = transform.localPosition;
                tmpObj.SetActive(true);
            }

            Destroy(gameObject);
        }        
    }    

    

}
