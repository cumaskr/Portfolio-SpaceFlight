using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cUnit_Boss : cUnit {

    public float   m_gravity;
    public Vector3 m_direction;
    public bool m_isOn;
    public bool m_isDownAttack;
    public int m_DownAttackCount = 0;
    public cGameManager m_GameManager;
    public bool m_isCoMake360Playing = false;
    public UISprite[] m_SpriteBossHP;
    public UILabel m_LabelBossHP;


    private void OnEnable()
    {
        Init(20000.0f, 2.0f, 100.0f);
        m_camera = new cCheckOutCamera(gameObject);
        m_isOn = false;
        m_isDownAttack = false;
        m_direction = new Vector3(1, 1, 0);
        StartCoroutine("CoMakeHP");
}
    	
	// Update is called once per frame
	void Update () {

        if (m_isOn)
        {
            m_gravity -= 0.1f;
            m_camera.CheckOutCamera("Prefab_Unit_Item", this);
            transform.Translate(((new Vector3(-1 * m_direction.x, 1 * m_direction.y, 0) * m_speed) + (new Vector3(0, m_gravity, 0))) * Time.deltaTime, Space.World);
            m_SpriteBossHP[1].fillAmount = 1 - (m_hp / 20000.0f);
            m_LabelBossHP.text = ((m_hp / 20000.0f) * 100.0f).ToString("N1") + "%";
        }        

    }
    
    IEnumerator Make360MinionOnce(float _speed)
    {
        string prefab_Name = "Prefab_Unit_Minion";        
        float _angle = 0.0f;
        Quaternion _Direction = Quaternion.Euler(0, 0, _angle);
        m_isCoMake360Playing = false;

        while (_angle <= 360.0f)
        {
            m_isCoMake360Playing = true;
            if (gameObject == null) yield break;            
            yield return new WaitForSeconds(0.01f);            
            GameObject tmpObj = cObjectPool.INSTANCE.GetObject(cPrefabManager.INSTANCE.FindPrefab(prefab_Name).name, cPrefabManager.INSTANCE.FindPrefab(prefab_Name));            
            tmpObj.transform.localPosition = transform.localPosition;
            tmpObj.transform.localScale = Vector3.one;
            tmpObj.GetComponent<cUnit_Minion>().m_speed = _speed;
            tmpObj.GetComponent<cUnit_Minion>().m_direction = _Direction * Vector3.up ;                   
            tmpObj.SetActive(true);
            _angle += 15.0f;
            _Direction = Quaternion.Euler(0, 0, _angle);
        }
        m_isCoMake360Playing = false;
        yield break;
    }

    private void OnTriggerStay(Collider col)
    {                
        if (m_gravity >= 0) return;
        if (col.tag.Equals("TAG_PLAYER"))
        {
            col.gameObject.GetComponent<cUnit>().GetDamage(m_damage, 5.0f);
        }                   
    }

    public override void Die()
    {
        if (!m_isDie)
        {
            m_LabelBossHP.text = "0.0%";
            //코루틴 2번 들어오는거 방지!
            m_isDie = true;
            GameObject tmpEffect = cObjectPool.INSTANCE.GetObject(cPrefabManager.INSTANCE.FindPrefab("Prerfab_Effect_Explosion").name, cPrefabManager.INSTANCE.FindPrefab("Prerfab_Effect_Explosion"));
            tmpEffect.GetComponent<cObjectPool_Effect>().Setting(transform.localPosition, "Prerfab_Effect_Explosion");
            m_GameManager.ResultClear();
            Destroy(gameObject);
        }        
    }    

    public override void Init(float _hp, float _speed, float _damage)
    {
        m_hp = _hp;
        m_speed = _speed;
        m_damage = _damage;
                
        m_firstColor = new Color(1, 1, 1);
        GetComponent<SpriteRenderer>().color = m_firstColor;
    }

    public IEnumerator CoMakeHP()
    {
        for (int i = 0; i < m_SpriteBossHP.Length; i++)
        {
            m_SpriteBossHP[i].gameObject.SetActive(true);
            m_SpriteBossHP[i].transform.localScale = Vector3.zero;
        }        
        while (m_SpriteBossHP[0].transform.localScale.x < 1.0f)
        {
            yield return new WaitForSeconds(0.01f);
            for (int i = 0; i < m_SpriteBossHP.Length; i++)
            {
                m_SpriteBossHP[i].transform.localScale += new Vector3(0.03f, 0.03f, 0.03f);
            }
        }        
        yield break;
    }
}
