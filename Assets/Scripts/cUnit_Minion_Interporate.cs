using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cUnit_Minion_Interporate : cUnit
{
    
    public Vector3[] m_VectorList;          //3개씩 1패턴
    public Vector3[] m_VectorDoubleList;    //5개씩 1패턴
    public int       m_startNumber = 0;
    public string    m_prefabName;

    float m_rotSpeed = 10.0f;    
    float m_t = 0.0f;
    public bool m_isDouble = true;

    

    private void Awake()
    {
        Init(200.0f, 3.0f, 25.0f);
        m_camera = new cCheckOutCamera(gameObject);
    }
    
    public void Setting(int _startNumber, string _prefab_Name, bool _isDouble)
    {
        m_startNumber = _startNumber;
        m_prefabName = _prefab_Name;
        m_isDouble = _isDouble;
        m_t = 0.0f;
        if (m_isDouble == true)
        {
            transform.localPosition = m_VectorDoubleList[m_startNumber];
            StartCoroutine("InterpolateStartDouble");
        }
        if (m_isDouble == false)
        {
            transform.localPosition = m_VectorList[m_startNumber];
            StartCoroutine("InterpolateStart");
        }
    }


    public Vector3 InterPolate(Vector3 _v1, Vector3 _v2, Vector3 _v3,float _t)
    {
        return Mathf.Pow((1 - _t), 2) * _v1 + 2 * _t * (1 - _t) * _v2 + Mathf.Pow(_t, 2) * _v3;        
    }

    IEnumerator InterpolateStart()
    {            
        while (m_t <= 1.0f)
        {
            transform.localPosition = InterPolate(m_VectorList[m_startNumber], m_VectorList[m_startNumber + 1], m_VectorList[m_startNumber + 2],m_t);
            m_t += 0.01f * cStopWatch.INSTANCE.m_speed;
            yield return new WaitForSeconds(0.01f);            
        }                
    }

    IEnumerator InterpolateStartDouble()
    {
        while (m_t <= 1.0f)
        {
            transform.localPosition = InterPolate(m_VectorDoubleList[m_startNumber], m_VectorDoubleList[m_startNumber + 1], m_VectorDoubleList[m_startNumber + 2], m_t);
            m_t += 0.02f * cStopWatch.INSTANCE.m_speed;
            yield return new WaitForSeconds(0.01f);
        }
        m_t = 0.0f;

        if (cStopWatch.INSTANCE.m_player != null)
        {
            m_VectorDoubleList[m_startNumber + 4] = cStopWatch.INSTANCE.m_player.transform.localPosition + new Vector3(0, -3, 0);
        }                
        while (m_t <= 1.0f)
        {
            transform.localPosition = InterPolate(m_VectorDoubleList[m_startNumber + 2], m_VectorDoubleList[m_startNumber + 3], m_VectorDoubleList[m_startNumber + 4], m_t);
            m_t += 0.02f * cStopWatch.INSTANCE.m_speed;
            yield return new WaitForSeconds(0.01f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * m_rotSpeed * Time.deltaTime);        
        m_camera.CheckOutCamera(m_prefabName);        
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.tag.Equals("TAG_POWER")) return;
        col.gameObject.GetComponent<cUnit>().GetDamage(m_damage, 1.0f);
    }

    public override void Init(float _hp, float _speed, float _damage)
    {
        m_hp = _hp;
        m_speed = _speed;
        m_damage = _damage;
        int tmpRnd = UnityEngine.Random.Range(-1, 1);
        if (tmpRnd == 0) m_rotSpeed *= 1;
        else m_rotSpeed *= -1;
        m_firstColor = new Color(Random.Range(0, 1.0f), Random.Range(0, 1.0f), Random.Range(0, 1.0f));
        GetComponent<SpriteRenderer>().color = m_firstColor;
    }

    public override void Die()
    {
        if (!m_isDie)
        {
            m_isDie = true;
            GameObject tmpEffect = cObjectPool.INSTANCE.GetObject(cPrefabManager.INSTANCE.FindPrefab("Prerfab_Effect_Rock").name, cPrefabManager.INSTANCE.FindPrefab("Prerfab_Effect_Rock"));
            tmpEffect.GetComponent<cObjectPool_Effect>().Setting(transform.localPosition, "Prerfab_Effect_Rock");
            Destroy(gameObject);

            int nRnd = Random.Range(0, 5);
            if (nRnd == 0)
            {
                GameObject tmpObj = cObjectPool.INSTANCE.GetObject(cPrefabManager.INSTANCE.FindPrefab("Prefab_Unit_Item").name, cPrefabManager.INSTANCE.FindPrefab("Prefab_Unit_Item"));
                tmpObj.GetComponent<cUnit_Item>().Setting("아이템_캐쉬");
                tmpObj.transform.localPosition = transform.localPosition;
                tmpObj.SetActive(true);
            }
        }        
    }
}
