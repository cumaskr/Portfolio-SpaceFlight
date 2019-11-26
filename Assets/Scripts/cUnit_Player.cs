using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cUnit_Player : cUnit {
    
    public Transform  m_mainFirePos  = null;
    public Transform  m_leftFirePos  = null;
    public Transform  m_rightFirePos = null;
    public int        m_FireLevel = 0;
    public GameObject m_ColliderPower = null;
    public GameObject m_ColliderMagnet = null;
    public GameObject m_EffectMagnet;
    public cGameManager m_GameManager = null; 

    private void Start()
    {
        Init(cDataManager.INSTANCE.PLAYER.m_hp,  6.0f, cDataManager.INSTANCE.PLAYER.m_Attack);
        m_camera = new cCheckOutCamera(gameObject);
        m_ColliderPower.SetActive(false);
        m_ColliderMagnet.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        //안드로이드 테스트용
        //if (Input.GetTouch(0).phase == TouchPhase.Moved)
        //{
        //    float movedeltaX = Input.GetTouch(0).deltaPosition.x;
        //    this.transform.Translate(-Vector3.right * movedeltaX * cDataManager.INSTANCE.PLAYER.m_deltaToch * Time.deltaTime);
        //    Vector3 minmaxPos = m_camera.CAMERA.WorldToViewportPoint(transform.localPosition);

        //    if (minmaxPos.x < 0.0f) minmaxPos.x = 0.0f;
        //    else if (minmaxPos.x > 1.0f) minmaxPos.x = 1.0f;
        //    transform.localPosition = m_camera.CAMERA.ViewportToWorldPoint(minmaxPos);
        //}

        //컴퓨터 테스트용
        if (Input.GetMouseButton(0))
        {
            float movedeltaX = Input.GetAxis("Mouse X");
            this.transform.Translate(-Vector3.right * movedeltaX * 13.0f * Time.deltaTime);
            Vector3 minmaxPos = m_camera.CAMERA.WorldToViewportPoint(transform.localPosition);
            if (minmaxPos.x < 0.0f) minmaxPos.x = 0.0f;
            else if (minmaxPos.x > 1.0f) minmaxPos.x = 1.0f;
            transform.localPosition = m_camera.CAMERA.ViewportToWorldPoint(minmaxPos);
        }
    }

    public override void AnimationHP(float _preVHP)
    {
        m_GameManager.StartCoroutine("InGameHPAnimation", _preVHP);
    }

    IEnumerator FireMain()
    {        
        while (this != null)
        {
            yield return new WaitForSeconds(0.05f);
            GameObject tmpObj = cObjectPool.INSTANCE.GetObject(cPrefabManager.INSTANCE.FindPrefab("Prefab_Bullet_Default").name, cPrefabManager.INSTANCE.FindPrefab("Prefab_Bullet_Default"));
            tmpObj.GetComponent<cBullet_Default>().Setting(m_mainFirePos, null, 10.0f, m_damage, "Prefab_Bullet_Default");
        }        
    }

    //cItem_Active_Bullet에서 호출
    IEnumerator FireLeft()
    {
        while (this != null)
        {
            yield return new WaitForSeconds(0.05f);
            GameObject tmpObj = cObjectPool.INSTANCE.GetObject(cPrefabManager.INSTANCE.FindPrefab("Prefab_Bullet_Default").name, cPrefabManager.INSTANCE.FindPrefab("Prefab_Bullet_Default"));
            tmpObj.GetComponent<cBullet_Default>().Setting(m_leftFirePos, null, 10.0f, m_damage, "Prefab_Bullet_Default");
        }
    }
    //cItem_Active_Bullet에서 호출
    IEnumerator FireRight()
    {
        while (this != null)
        {
            yield return new WaitForSeconds(0.05f);
            GameObject tmpObj = cObjectPool.INSTANCE.GetObject(cPrefabManager.INSTANCE.FindPrefab("Prefab_Bullet_Default").name, cPrefabManager.INSTANCE.FindPrefab("Prefab_Bullet_Default"));
            tmpObj.GetComponent<cBullet_Default>().Setting(m_rightFirePos, null, 10.0f, m_damage, "Prefab_Bullet_Default");
        }
    }
    //cItem_Active_Speed에서 호출
    public IEnumerator CoActiveSpeed(int _time)
    {
        m_ColliderPower.SetActive(true);
        int nCount = 0;
        cStopWatch.INSTANCE.m_speed = 2;
        while (nCount < _time)
        {
            yield return new WaitForSeconds(1.0f);
            nCount++;
        }
        cStopWatch.INSTANCE.m_speed = 1;
        m_ColliderPower.SetActive(false);
    }
    //cItem_Active_Magnet에서 호출
    public IEnumerator CoActiveMagnet(int _time)
    {
        m_ColliderMagnet.SetActive(true);
        m_EffectMagnet.SetActive(true);
        int nCount = 0;
        while (nCount < _time)
        {
            yield return new WaitForSeconds(1.0f);
            nCount++;
        }
        m_ColliderMagnet.SetActive(false);
        m_EffectMagnet.SetActive(false);
    }

    public override void Init(float _hp, float _speed, float _damage)
    {        
        m_speed = _speed;
        m_hp = _hp;
        m_damage = _damage;
        m_firstColor = new Color(1, 1, 1);
        GetComponent<SpriteRenderer>().color = m_firstColor;
    }

    public override void Die()
    {
        if (!m_isDie)
        {
            m_isDie = true;
            GameObject tmpEffect = cObjectPool.INSTANCE.GetObject(cPrefabManager.INSTANCE.FindPrefab("Prerfab_Effect_Explosion").name, cPrefabManager.INSTANCE.FindPrefab("Prerfab_Effect_Explosion"));
            tmpEffect.GetComponent<cObjectPool_Effect>().Setting(transform.localPosition, "Prerfab_Effect_Explosion");
            m_GameManager.ResultFail();
            Destroy(gameObject);
        }        
    }
    
}
