using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class cGameManager : MonoBehaviour {

    [System.Serializable]
    public class cCorutineData
    {
        public string m_funcName;
        public int m_parameter;
    }
    [System.Serializable]
    public class cData
    {
        public float      m_time;
        public cCorutineData[] m_funcList;        
    }

    public Camera       m_camera;
    public cData[]      m_MakeTimeList;
    public GameObject   m_boss;
    public GameObject   m_bossEffect;
    public UILabel      m_moneyLabel;
    public UILabel      m_cashLabel;
    public UILabel      m_ReadyLabel;
    public UITexture[]  m_inGameSkillList;

    public UITexture m_TextureHP;
    public UILabel   m_LabelHP;
    
    cUnit_Player m_player;

    int                 m_InGameCount = 0;
    GameObject          m_PauseObejct = null;
    Vector3             m_cameraViewPort;

    public float m_FirstMoneyInGame;
    public float m_FirstSCashInGame;
    public bool m_isClear = false;
    public GameObject m_resultWindow;
    public UILabel m_testLabel;
    
    float m_firstCash;
    float m_firstGameMoney;

    // Use this for initialization
    void Start () {
        
        cSoundManager.INSTANCE.Play(2);

        m_firstCash = cDataManager.INSTANCE.PLAYER.m_Cash;
        m_firstGameMoney = cDataManager.INSTANCE.PLAYER.m_GameMoney;

        cObjectPool.INSTANCE.Setting(cPrefabManager.INSTANCE.FindPrefab("Prefab_Bullet_Default").name, 5, cPrefabManager.INSTANCE.FindPrefab("Prefab_Bullet_Default"));
        cObjectPool.INSTANCE.Setting(cPrefabManager.INSTANCE.FindPrefab("Prerfab_Effect_Blood").name, 5, cPrefabManager.INSTANCE.FindPrefab("Prerfab_Effect_Blood"));
        cObjectPool.INSTANCE.Setting(cPrefabManager.INSTANCE.FindPrefab("Prerfab_Effect_Rock").name, 5, cPrefabManager.INSTANCE.FindPrefab("Prerfab_Effect_Rock"));
        cObjectPool.INSTANCE.Setting(cPrefabManager.INSTANCE.FindPrefab("Prefab_Unit_Minion").name, 5, cPrefabManager.INSTANCE.FindPrefab("Prefab_Unit_Minion"));
        cObjectPool.INSTANCE.Setting(cPrefabManager.INSTANCE.FindPrefab("Prerfab_Effect_Explosion").name, 1, cPrefabManager.INSTANCE.FindPrefab("Prerfab_Effect_Explosion"));
        cObjectPool.INSTANCE.Setting(cPrefabManager.INSTANCE.FindPrefab("Prefab_Unit_Minion_Interporate").name, 5, cPrefabManager.INSTANCE.FindPrefab("Prefab_Unit_Minion_Interporate"));
        cObjectPool.INSTANCE.Setting(cPrefabManager.INSTANCE.FindPrefab("Prefab_Unit_Item").name, 5, cPrefabManager.INSTANCE.FindPrefab("Prefab_Unit_Item"));
        cObjectPool.INSTANCE.Setting(cPrefabManager.INSTANCE.FindPrefab("Prerfab_Effect_Item").name, 5, cPrefabManager.INSTANCE.FindPrefab("Prerfab_Effect_Item"));
        cObjectPool.INSTANCE.Setting(cPrefabManager.INSTANCE.FindPrefab("Prefab_Unit_LightMinion").name, 1, cPrefabManager.INSTANCE.FindPrefab("Prefab_Unit_LightMinion"));
        cObjectPool.INSTANCE.Setting(cPrefabManager.INSTANCE.FindPrefab("Prefab_Unit_LightMinionAttack").name, 1, cPrefabManager.INSTANCE.FindPrefab("Prefab_Unit_LightMinionAttack"));
        cObjectPool.INSTANCE.Setting(cPrefabManager.INSTANCE.FindPrefab("Prerfab_Effect_Clear").name, 1, cPrefabManager.INSTANCE.FindPrefab("Prerfab_Effect_Clear"));

        cDataManager.INSTANCE.PLAYER.m_event += PlayerDataObserver;
        cDataManager.INSTANCE.PLAYER.Notify();

        m_player = cStopWatch.INSTANCE.m_player.GetComponent<cUnit_Player>();

        StartCoroutine("GameStartAfterCountDown");
        StartCoroutine("InGameHPAnimation",cDataManager.INSTANCE.PLAYER.m_hp);
        UIInGameSkillUpdate();

        m_FirstMoneyInGame = cDataManager.INSTANCE.PLAYER.m_GameMoney;
        m_FirstSCashInGame = cDataManager.INSTANCE.PLAYER.m_Cash;
    }
    
    // Update is called once per frame
    void Update () {

        //if (m_testLabel.gameObject.activeSelf == false) m_testLabel.gameObject.SetActive(true);        
        //m_testLabel.text = cObjectPool.INSTANCE.m_dic["Prefab_Bullet_Default"].Count.ToString() +"/"+ cObjectPool.INSTANCE.m_dic["Prefab_Unit_Minion"].Count.ToString() + "/"+ cObjectPool.INSTANCE.m_dic["Prefab_Unit_Minion_Interporate"].Count.ToString();

        InGameMinionSetting();               
    }

    void InGameMinionSetting()
    {
        if (m_InGameCount >= m_MakeTimeList.Length) return;
        if (cStopWatch.INSTANCE.m_timer >= m_MakeTimeList[m_InGameCount].m_time)
        {
            for (int i = 0; i < m_MakeTimeList[m_InGameCount].m_funcList.Length; i++)
            {
                StartCoroutine(m_MakeTimeList[m_InGameCount].m_funcList[i].m_funcName, m_MakeTimeList[m_InGameCount].m_funcList[i].m_parameter);
            }
            m_InGameCount++;
        }
    }

    public void UIInGameSkillUpdate()
    {
        for (int i = 0; i < cDataManager.INSTANCE.PLAYER.m_itemList.Length; i++)
        {
            if (cDataManager.INSTANCE.PLAYER.m_itemList[i] != null)
            {
                m_inGameSkillList[i].mainTexture = Resources.Load(cItem_Factory.MakePath(cDataManager.INSTANCE.PLAYER.m_itemList[i].m_name)) as Texture;                    
            }
            else
            {
                m_inGameSkillList[i].mainTexture = null;
            }       
        }        
    }

    public void UIInGameSkillOne()
    {
        if (m_inGameSkillList[0].mainTexture == null) return;
        if (m_isClear || m_player == null) return;
        cDataManager.INSTANCE.PLAYER.m_itemList[0].Use(m_player);
        //cDataManager.INSTANCE.PLAYER.m_itemList[0] = null;
        m_inGameSkillList[0].mainTexture = null;
    }

    public void UIInGameSkillTwo()
    {
        if (m_inGameSkillList[1].mainTexture == null) return;
        if (m_isClear || m_player == null) return;
        cDataManager.INSTANCE.PLAYER.m_itemList[1].Use(m_player);
        //cDataManager.INSTANCE.PLAYER.m_itemList[1] = null;
        m_inGameSkillList[1].mainTexture = null;
    }

    public void UIInGameSkillThree()
    {
        if (m_inGameSkillList[2].mainTexture == null) return;
        if (m_isClear || m_player == null) return;
        cDataManager.INSTANCE.PLAYER.m_itemList[2].Use(m_player);
        //cDataManager.INSTANCE.PLAYER.m_itemList[2] = null;
        m_inGameSkillList[2].mainTexture = null;
    }
    
    public void PlayerDataObserver()
    {        
        m_moneyLabel.text = cDataManager.INSTANCE.PLAYER.m_GameMoney.ToString();
        m_cashLabel.text = cDataManager.INSTANCE.PLAYER.m_Cash.ToString();
    }

    public void MakePrefabPause()
    {
        if (m_PauseObejct == null)
        {
            m_PauseObejct = cPrefabManager.INSTANCE.MakePrefab("Prefab_Popup_Pause", "UI Front");
            m_PauseObejct.GetComponent<cUiPopUp>().FadeIn();
        }
        else
        {
            m_PauseObejct.GetComponent<cUiPopUp>().FadeIn();
        }
        Time.timeScale = 0.0f;
    }

    public int ResultScroeMapCalculate()
    {
        if (((int)(m_player.m_hp / cDataManager.INSTANCE.PLAYER.m_hp * 100)) >= 70)
        {
            return 3;   
        }
        else if (((int)(m_player.m_hp / cDataManager.INSTANCE.PLAYER.m_hp * 100)) >= 50)
        {
            return 2;
        }
        else if (((int)(m_player.m_hp / cDataManager.INSTANCE.PLAYER.m_hp * 100)) < 50)
        {
            return 1;
        }
        return 0;
    }

    public void ResultClear()
    {        
        int _sceneIndex = 0;
        _sceneIndex = System.Int32.Parse(SceneManager.GetActiveScene().name.Remove(0, 8));
        cDataManager.INSTANCE.MAP.m_mapInfoList[_sceneIndex].m_score = ResultScroeMapCalculate();

        //2스테이지 까지 밖에 안만듬.
        if (_sceneIndex < 1) cDataManager.INSTANCE.MAP.m_mapInfoList[++_sceneIndex].m_isOn = true;

        for (int i = 0; i < m_inGameSkillList.Length; i++)
        {
            if (m_inGameSkillList[i].mainTexture == null) cDataManager.INSTANCE.PLAYER.m_itemList[i] = null;            
        }

        cDataManager.INSTANCE.PLAYER.SaveXML();
        cDataManager.INSTANCE.MAP.SaveXML();
        
        StartCoroutine("CoClearEffect");

        m_isClear = true;
        m_resultWindow.SetActive(true);
    }

    public void ResultFail()
    {
        ReturnToPlayerByFail();
        m_isClear = false;        
        m_resultWindow.SetActive(true);        
    }

    void ReturnToPlayerByFail()
    {
        cDataManager.INSTANCE.PLAYER.m_Cash = m_firstCash;
        cDataManager.INSTANCE.PLAYER.m_GameMoney = m_firstGameMoney;
        cDataManager.INSTANCE.PLAYER.Notify();
    }

    //뒤로가기 버튼
    public void ChaneScenetoMapSelectNow()
    {
        cDataManager.INSTANCE.PLAYER.m_Cash = m_firstCash;
        cDataManager.INSTANCE.PLAYER.m_GameMoney = m_firstGameMoney;
        cDataManager.INSTANCE.PLAYER.Notify();
        cSceneManager.INSTANCE.StartCoroutine("ChangeScene", "scMapSelect");
    }
    //결과창 돌아가기 버튼
    public void ChangeSceneToMapSelect()
    {        
        cSceneManager.INSTANCE.StartCoroutine("ChangeScene", "scMapSelect");
    }
    
    IEnumerator GameStartAfterCountDown()
    {
        yield return new WaitForSeconds(0.3f);
        int nCount = 2;
        m_ReadyLabel.gameObject.SetActive(true);
        while (nCount > 0)
        {
            nCount--;
            if (nCount == 1) m_ReadyLabel.text = "Ready !";
            else if (nCount == 0) m_ReadyLabel.text = "Go !";
            yield return new WaitForSeconds(1.0f);
        }
        m_ReadyLabel.gameObject.SetActive(false);
        cStopWatch.INSTANCE.m_isStart = true;
        m_player.StartCoroutine("FireMain");
        yield break;
    }

    IEnumerator CoMakeMinion5(int _nCount)
    {
        string prefab_Name = "Prefab_Unit_Minion";
        int nCount = 0;
        while (nCount < _nCount)
        {
            yield return new WaitForSeconds(1.5f / cStopWatch.INSTANCE.m_speed);
            for (int i = 0; i < 5; i++)
            {
                GameObject tmpObj = cObjectPool.INSTANCE.GetObject(cPrefabManager.INSTANCE.FindPrefab(prefab_Name).name, cPrefabManager.INSTANCE.FindPrefab(prefab_Name));
                tmpObj.transform.localPosition = m_camera.ViewportToWorldPoint(new Vector3(0.2f * i + 0.1f, 0, 0));
                tmpObj.transform.localPosition = new Vector3(tmpObj.transform.localPosition.x, 6.0f, 0.0f);
                tmpObj.transform.localRotation = Quaternion.identity;
                tmpObj.transform.localScale = Vector3.one * 1.5f;
                tmpObj.SetActive(true);
            }
            nCount++;
        }
    }

    IEnumerator CoMakeMinionInterPorate(int _startIndex)
    {
        string prefab_Name = "Prefab_Unit_Minion_Interporate";
        int nCount = 0;
        while (nCount < 15)
        {
            yield return new WaitForSeconds(0.2f / cStopWatch.INSTANCE.m_speed);
            GameObject tmpObj = cObjectPool.INSTANCE.GetObject(cPrefabManager.INSTANCE.FindPrefab(prefab_Name).name, cPrefabManager.INSTANCE.FindPrefab(prefab_Name));
            tmpObj.GetComponent<cUnit_Minion_Interporate>().Setting(_startIndex, prefab_Name, false);
            tmpObj.SetActive(true);
            nCount++;
        }
        yield break;
    }

    IEnumerator CoMakeMinionInterPorateDouble(int _startIndex)
    {
        string prefab_Name = "Prefab_Unit_Minion_Interporate";
        int nCount = 0;
        while (nCount < 20)
        {
            yield return new WaitForSeconds(0.2f / cStopWatch.INSTANCE.m_speed);
            GameObject tmpObj = cObjectPool.INSTANCE.GetObject(cPrefabManager.INSTANCE.FindPrefab(prefab_Name).name, cPrefabManager.INSTANCE.FindPrefab(prefab_Name));
            tmpObj.GetComponent<cUnit_Minion_Interporate>().Setting(_startIndex, prefab_Name, true);
            tmpObj.SetActive(true);
            nCount++;
        }
        yield break;
    }

    IEnumerator CoBossIntroMove()
    {
        m_boss.SetActive(true);
        m_boss.GetComponent<cInterPorate>().StartCoroutine("InterpolateStart");
        yield break;
    }

    IEnumerator CoMakeBossEffect()
    {
        m_bossEffect.SetActive(true);
        int nCount = 0;
        while (nCount < 10)
        {
            yield return new WaitForSeconds(0.5f);
            nCount++;
        }
        m_bossEffect.SetActive(false);

        yield break;
    }

    IEnumerator CoMakeBoss()
    {
        m_boss.transform.localScale = Vector3.zero;
        StartCoroutine("CoBossIntroMove");

        while (m_boss.transform.localScale.x <= 4.5f)
        {
            yield return new WaitForSeconds(0.01f);
            m_boss.transform.localScale += new Vector3(0.02f, 0.02f, 0.02f);
        }

        m_boss.transform.localScale = new Vector3(4.5f, 4.5f, 1.0f);                
        m_boss.GetComponent<cUnit_Boss>().m_isOn = true;        
        yield break;
    }

    IEnumerator InGameHPAnimation(float _prevHp)
    {
        float tmpHp = _prevHp;
        yield return new WaitForEndOfFrame();
        while (tmpHp >= m_player.m_hp)
        {
            yield return new WaitForSeconds(0.01f);
            m_TextureHP.fillAmount = tmpHp / cDataManager.INSTANCE.PLAYER.m_hp;
            m_LabelHP.text = ((int)(tmpHp / cDataManager.INSTANCE.PLAYER.m_hp * 100)).ToString() + "%";
            tmpHp--;
        }
        m_TextureHP.fillAmount = m_player.m_hp / cDataManager.INSTANCE.PLAYER.m_hp;
        m_LabelHP.text = ((int)(m_player.m_hp / cDataManager.INSTANCE.PLAYER.m_hp * 100)).ToString() + "%";
        yield break;
    }

    IEnumerator CoMakeLightMinion()
    {
        GameObject tmpObj = cObjectPool.INSTANCE.GetObject(cPrefabManager.INSTANCE.FindPrefab("Prefab_Unit_LightMinion").name, cPrefabManager.INSTANCE.FindPrefab("Prefab_Unit_LightMinion"));
        tmpObj.transform.localPosition = new Vector3(0, 0, 0);
        tmpObj.SetActive(true);
        yield break;
    }

    IEnumerator CoClearEffect()
    {
        int nCount = 0;
        while (nCount < 8)
        {
            yield return new WaitForSeconds(0.5f);
            GameObject tmpClearEffect = cObjectPool.INSTANCE.GetObject(cPrefabManager.INSTANCE.FindPrefab("Prerfab_Effect_Clear").name, cPrefabManager.INSTANCE.FindPrefab("Prerfab_Effect_Clear"));
            tmpClearEffect.GetComponent<cObjectPool_Effect>().Setting(new Vector3(UnityEngine.Random.Range(-2.0f, 3.0f), UnityEngine.Random.Range(-1.0f, 4.0f), 0), "Prerfab_Effect_Clear");            
            nCount++;
        }
        yield break;
    }
           
}
