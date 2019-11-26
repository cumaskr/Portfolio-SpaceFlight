using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cDataManager : MonoBehaviour {

    static cDataManager m_instance;

    public static cDataManager INSTANCE
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<cDataManager>() as cDataManager;
                if (m_instance == null)
                {
                    Debug.LogError("데이터 매니져 싱글톤 객체 생성이 안되었습니다.");
                }
            }
            return m_instance;
        }
    }


    cDataPlayer m_player;
    public cDataPlayer PLAYER
    {
        get
        {
            if (m_player == null)
            {
                Debug.LogError("플레이어 데이터가 없습니다.");
                return null;
            }             
            return m_player;
        }
    }

    cDataMap m_map;
    public cDataMap MAP
    {        
        get
        {
            if (m_map == null)
            {
                Debug.LogError("맵 데이터가 없습니다.");
                return null;
            }
            return m_map;
        }
    }

    cDataInventory m_inventory;
    public cDataInventory INVENTORY
    {
        get
        {
            if (m_inventory == null)
            {
                Debug.LogError("인벤토리 데이터가 없습니다.");
                return null;
            }
            return m_inventory;
        }
    }

    private void Awake()
    {        
        m_player = new cDataPlayer();        
        m_map = new cDataMap();
        m_inventory = new cDataInventory();

        DontDestroyOnLoad(gameObject);
    }    

}
