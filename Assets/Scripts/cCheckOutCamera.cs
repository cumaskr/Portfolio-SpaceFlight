using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cCheckOutCamera {

    Camera m_camera;
    GameObject m_gameObject;    

    public Camera CAMERA
    {
        get
        {
            return m_camera;
        }       
    }
    
    public cCheckOutCamera(GameObject _gameObject)
    {
        m_camera = cStopWatch.INSTANCE.m_3Dcamera.GetComponent<Camera>();
        m_gameObject = _gameObject;
        
    }
        
    public void CheckOutCamera(string _prefabName, cUnit_Boss _boss = null)
    {
        //줌인일때는 잠시 보류
        if (m_camera.orthographicSize != 5) return;

        Vector3 minmaxPos = m_camera.WorldToViewportPoint(m_gameObject.transform.localPosition);

        if (m_gameObject.tag.Equals("TAG_ENEMY_BOSS"))
        {
            if (minmaxPos.x < 0.0f || minmaxPos.x > 1.0f)
            {
                if (!_boss.m_isDownAttack)
                {
                    if (!_boss.m_isCoMake360Playing)
                    {
                        _boss.StartCoroutine("Make360MinionOnce", 7.0f);
                    }                    
                } 

                _boss.m_DownAttackCount++;
                if (_boss.m_DownAttackCount % 5 == 0)
                {
                    _boss.m_isDownAttack = true;
                }

                _boss.m_direction.x *= -1;                                
            }  
                      
            if (_boss.m_isDownAttack == true && minmaxPos.y > 1.0f)
            {
                _boss.m_isDownAttack = false;
                _boss.m_gravity = 0.0f;
            }

            //번지점프라면
            if (_boss.m_isDownAttack)
            {
                if (minmaxPos.y < 0.0f)
                {
                    _boss.m_gravity = 20.0f;
                    if (_boss.m_gravity == 20.0f) _boss.m_GameManager.StartCoroutine("CoMakeLightMinion");
                }
            }
            //번지점프가 아니라면
            else
            {
                if (minmaxPos.y < 0.8f)
                {
                    _boss.m_gravity = 0.0f;                    
                }
            }                        
        }            
        else if (m_gameObject.tag.Equals("TAG_ITEM"))
        {
            if (minmaxPos.x < 0.0f)
            {                
                m_gameObject.GetComponent<cUnit_Item>().m_direction = -1;
            }
            else if (minmaxPos.x > 1.0f)
            {
                m_gameObject.GetComponent<cUnit_Item>().m_direction = -1;                
            }
            else if (minmaxPos.y < 0.0f)
            {
                cObjectPool.INSTANCE.ReturnObject(cPrefabManager.INSTANCE.FindPrefab(_prefabName).name, m_gameObject);
            }            
        }
        else
        {
            if (m_gameObject.tag.Equals("TAG_BULLET") && minmaxPos.y > 1.0f)
            {
                cObjectPool.INSTANCE.ReturnObject(cPrefabManager.INSTANCE.FindPrefab(_prefabName).name, m_gameObject);
            }
            if (m_gameObject.tag.Equals("TAG_ENEMY") && minmaxPos.y > 1.5f)
            {
                cObjectPool.INSTANCE.ReturnObject(cPrefabManager.INSTANCE.FindPrefab(_prefabName).name, m_gameObject);
            }
            if (minmaxPos.x < 0.0f)
            {
                cObjectPool.INSTANCE.ReturnObject(cPrefabManager.INSTANCE.FindPrefab(_prefabName).name, m_gameObject);
            }
            else if (minmaxPos.x > 1.0f)
            {
                cObjectPool.INSTANCE.ReturnObject(cPrefabManager.INSTANCE.FindPrefab(_prefabName).name, m_gameObject);
            }
            else if (minmaxPos.y < 0.0f)
            {
                cObjectPool.INSTANCE.ReturnObject(cPrefabManager.INSTANCE.FindPrefab(_prefabName).name, m_gameObject);
            }           
        }        
    }

}
