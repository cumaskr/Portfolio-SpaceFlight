using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cUnit_Light : cUnit
{
    
    private void OnEnable()
    {
        StartCoroutine("CoTracePlayer");
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

    IEnumerator CoTracePlayer()
    {
        float nCount = 0;
        Vector3 towardVec = transform.localPosition;
        while (nCount < 2.0f)
        {
            yield return new WaitForSeconds(0.01f);
            nCount += 0.01f;
            if (cStopWatch.INSTANCE.m_player != null)
            {
                towardVec = (cStopWatch.INSTANCE.m_player.transform.localPosition - transform.localPosition).normalized;
                towardVec = new Vector3(towardVec.x, 0, 0);
            }
            else
            {
                towardVec = new Vector3(0, 0, 0);
            }
            transform.Translate(towardVec * 15.0f * Time.deltaTime);
        }

        GameObject tmpObj = cObjectPool.INSTANCE.GetObject(cPrefabManager.INSTANCE.FindPrefab("Prefab_Unit_LightMinionAttack").name, cPrefabManager.INSTANCE.FindPrefab("Prefab_Unit_LightMinionAttack"));
        tmpObj.transform.localPosition = new Vector3(transform.localPosition.x, 6.0f, 0.0f);        
        tmpObj.SetActive(true);        

        cObjectPool.INSTANCE.ReturnObject("Prefab_Unit_LightMinion", gameObject);
        yield break;        
    }
}
