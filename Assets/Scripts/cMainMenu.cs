using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cMainMenu : MonoBehaviour {

    public GameObject m_buttons;
    GameObject[] m_buttonList;
    public GameObject[] m_prefabList;

    public UILabel m_playerGameMoneyLabel;
    public UILabel m_playerCashLabel;

    private void Start()
    {        
        m_buttonList = new GameObject[m_buttons.transform.childCount];

        cDataManager.INSTANCE.PLAYER.m_event += PlayerDataObserver;
        cDataManager.INSTANCE.PLAYER.Notify();
        cSoundManager.INSTANCE.Play(0);        
    }
        
    public void ButtonOption()
    {        
        if (m_buttonList[0] == null)
        {
            m_buttonList[0] = cPrefabManager.INSTANCE.MakePrefab("prefab_Popup_Option", "UI Front");
            m_buttonList[0].GetComponent<cUiPopUp>().FadeIn();
        }
        else
        {
            m_buttonList[0].GetComponent<cUiPopUp>().FadeIn();
        }        
    }
    public void ButtonShop()
    {
        if (m_buttonList[1] == null)
        {
            m_buttonList[1] = cPrefabManager.INSTANCE.MakePrefab("prefab_Popup_Shop", "UI Front");
            m_buttonList[1].GetComponent<cUiPopUp>().FadeIn();
        }
        else
        {
            m_buttonList[1].GetComponent<cUiPopUp>().FadeIn();
        }        
    }
    public void ButtonFriendList()
    {
        if (m_buttonList[2] == null)
        {
            m_buttonList[2] = cPrefabManager.INSTANCE.MakePrefab("Prefab_Popup_FriendList", "UI Front");            
            m_buttonList[2].GetComponent<cUiPopUp>().FadeIn();
        }
        else
        {
            m_buttonList[2].GetComponent<cUiPopUp>().FadeIn();
        }
    }    
    public void ButtonQuit()
    {     
        if (m_buttonList[3] == null)
        {                
            m_buttonList[3] = cPrefabManager.INSTANCE.MakePrefab("Prefab_Popup_Quit", "UI Front");            
            m_buttonList[3].GetComponent<cUiPopUp>().FadeIn();
        }
        else
        {                        
            m_buttonList[3].GetComponent<cUiPopUp>().FadeIn();
        }        
    }
    public void ButtonInGame()
    {
        cSceneManager.INSTANCE.StartCoroutine("ChangeScene", "scMapSelect");
    }

    public void PlayerDataObserver()
    {        
            m_playerGameMoneyLabel.text = cDataManager.INSTANCE.PLAYER.m_GameMoney.ToString();                
            m_playerCashLabel.text = cDataManager.INSTANCE.PLAYER.m_Cash.ToString();        
    }     
}
