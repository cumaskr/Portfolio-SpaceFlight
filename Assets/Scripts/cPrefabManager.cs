using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cPrefabManager : MonoBehaviour {

    static cPrefabManager m_instance;

    public static cPrefabManager INSTANCE
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<cPrefabManager>() as cPrefabManager;
                if (m_instance == null)
                {
                    Debug.LogError("프리팹 매니져 싱글톤 객체 생성이 안되었습니다.");
                }
            }
            return m_instance;
        }
    }

    public GameObject[] m_prefabList;

    public GameObject FindPrefab(string _name)
    {
        for (int i = 0; i < m_prefabList.Length; i++)
        {
            if (m_prefabList[i].name.Equals(_name))
            {
                return m_prefabList[i];
            }
        }
        return null;
    }

   public GameObject MakePrefab(string _name, string _parent)
    {
        GameObject findObj = FindPrefab(_name);
        if (findObj == null) return null;
        GameObject outObj = Instantiate(findObj, Vector3.zero, Quaternion.identity) as GameObject;
        outObj.transform.SetParent(GameObject.Find(_parent).transform);
        outObj.transform.localScale = Vector3.one;
        return outObj;
    }    



}
