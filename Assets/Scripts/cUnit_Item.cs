using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cUnit_Item : cUnit {

    cItem m_item;
    float m_rotSpeed = 30.0f;
    float m_graity = 0.0f;        
    float m_radNormalVec;
    float m_radNormalVec2;
    public int m_direction; //화면 양옆 반대로 튕길때 사용하는 변수    
    public bool m_isMagnet;

    public void Setting(string _itemName)
    {
        Init(100.0f, 2.0f, 25.0f);        
        m_camera = new cCheckOutCamera(gameObject);        

        m_item = cItem_Factory.MakeItem(_itemName);        
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(cItem_Factory.MakePath(_itemName));
        transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);                

        m_radNormalVec = (float)UnityEngine.Random.Range(-1, 2);
        m_radNormalVec2 = (float)UnityEngine.Random.Range(0.5f, 2);
        
        m_direction = 1;
        m_graity = 0.0f;
        m_isMagnet = false;
    }

	// Update is called once per frame
	void Update () {

        transform.Rotate(Vector3.forward * m_rotSpeed * Time.deltaTime);        
        m_camera.CheckOutCamera("Prefab_Unit_Item");        
        m_graity -= 0.1f;

        if(!m_isMagnet) transform.Translate(((new Vector3(m_direction * m_radNormalVec, m_radNormalVec2, 0) * m_speed) + (new Vector3(0, m_graity, 0))) * Time.deltaTime, Space.World);
        else transform.Translate((DirectionMove(cStopWatch.INSTANCE.m_player)* m_speed - DirectionMove(cStopWatch.INSTANCE.m_player) * m_graity) *  Time.deltaTime, Space.World);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag.Equals("TAG_POWER")) return;        
        m_item.Use(cStopWatch.INSTANCE.m_player.GetComponent<cUnit_Player>());
        Die();      
    }

    Vector3 DirectionMove(GameObject _object)
    {
        if (_object == null) return Vector3.zero;     
           
        return (_object.transform.localPosition - transform.localPosition).normalized;
    }

    public override void Init(float _hp, float _speed, float _damage)
    {
        m_hp = _hp;
        m_speed = _speed;
        m_damage = _damage;
        int tmpRnd = UnityEngine.Random.Range(-1, 1);
        if (tmpRnd == 0) m_rotSpeed *= 1;
        else m_rotSpeed *= -1;
    }

    public override void Die()
    {
        GameObject tmpEffect = cObjectPool.INSTANCE.GetObject(cPrefabManager.INSTANCE.FindPrefab("Prerfab_Effect_Item").name, cPrefabManager.INSTANCE.FindPrefab("Prerfab_Effect_Item"));
        tmpEffect.GetComponent<cObjectPool_Effect>().Setting(transform.localPosition, "Prerfab_Effect_Item");
        Destroy(gameObject);
    }
    
}
