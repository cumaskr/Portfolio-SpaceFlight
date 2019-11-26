using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class cUnit : MonoBehaviour {

    public float m_hp;
    public float m_speed;
    public float m_damage;
    public Color m_firstColor;
    public cCheckOutCamera m_camera = null;
    public bool m_isDie = false; // 총알이 빠르게 돌진하면서 죽을때 이펙트 코루틴이 잠시 멈추다가 Die함수가 2번호출되는 버그 발생 막기위해!

    float m_courutineDamage;
    float m_courutineWaitingTime;

    bool m_isColorEffectPlaying;
    bool m_isGetDamagePlaying;
   

    public abstract void Init(float _hp, float _speed, float _damage);
    public abstract void Die();
    public virtual void AnimationHP(float _preVHP) { }

    public IEnumerator ColorEffect()
    {
        int nTimer = 0;
        m_isColorEffectPlaying = false;        
        while (nTimer < 2)
        {
            if (nTimer % 2 == 0)
            {
                GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f);                
            }
            else
            {
                GetComponent<SpriteRenderer>().color = m_firstColor;                
            }
            m_isColorEffectPlaying = true;
            yield return new WaitForSeconds(0.07f);                        
            nTimer++;
        }
        m_isColorEffectPlaying = false;
    }

    public int m_DropPercentage = 0;


    public void GetDamage(float _damage, float _waitingTime)
    {
        m_courutineDamage = _damage;
        m_courutineWaitingTime = _waitingTime;
        if (!m_isGetDamagePlaying) StartCoroutine("CoroutineDamage");        
    }

    IEnumerator CoroutineDamage()
    {
        AnimationHP(m_hp);
        m_hp -= m_courutineDamage;
        if (!m_isColorEffectPlaying) StartCoroutine("ColorEffect");
        if (m_hp <= 0)
        {
            Die();
            yield break;
        }
        if (gameObject.tag.Equals("TAG_PLAYER")) GetComponent<cZoomInOut>().StartZoomInOut();        
        m_isGetDamagePlaying = true;
        yield return new WaitForSeconds(m_courutineWaitingTime);
        m_isGetDamagePlaying = false;
    }

    //public void GetDamage(float _damage)
    //{        
    //    m_hp -= _damage;
    //    if (!m_isColorEffectPlaying) StartCoroutine("ColorEffect");
    //    if (m_hp <= 0)
    //    {
    //        Die();            
    //    }
    //}
}
