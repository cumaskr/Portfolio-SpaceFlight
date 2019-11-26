using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//딕셔너리를 썻을경우 확인이 불가능
public class cObjectPool : MonoBehaviour {
   
    static cObjectPool m_instance;

    public static cObjectPool INSTANCE
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<cObjectPool>() as cObjectPool;
                if (m_instance == null)
                {
                    Debug.LogError("오브젝트풀 매니져 싱글톤 객체 생성이 안되었습니다.");
                }
            }
            return m_instance;
        }
    }
      
    public Dictionary<string, List<GameObject>> m_dic = new Dictionary<string, List<GameObject>>();
    
    public void Setting(string _name, int _count, GameObject _prefab)
    {        
        if (!m_dic.ContainsKey(_name))
        {
            m_dic.Add(_name, new List<GameObject>());
        }
        if (m_dic[_name].Count != 0)
        {
            m_dic[_name] = new List<GameObject>();            
        }

        for (int i = 0; i < _count; i++)
        {
            GameObject tmpObj = Object.Instantiate(_prefab) as GameObject;
            tmpObj.name = _name;
            tmpObj.SetActive(false);                        
            m_dic[_name].Add(tmpObj);
        }
    }
    public void ReturnObject(string _name, GameObject _obj)
    {
        _obj.SetActive(false);
        m_dic[_name].Add(_obj);
    }
    public GameObject GetObject(string _name, GameObject _prefab)
    {        
        if (m_dic[_name].Count == 0)
        {
            GameObject tmpObj = Object.Instantiate(_prefab) as GameObject;
            tmpObj.name = _name;
            tmpObj.SetActive(false);
            m_dic[_name].Add(tmpObj);
        } 

        GameObject outObj = m_dic[_name][0];        
        m_dic[_name].RemoveAt(0);
        outObj.SetActive(true);
        return outObj;
    }   
}
