using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cUiPopUp_Shop : cUiPopUp
{
    public UILabel m_playerGameMoneyLabel;
    public UILabel m_playerCashLabel;

    public UILabel m_playerHPLabel;
    public UILabel m_playerAttackLabel;    

    private void Awake()
    {
        cDataManager.INSTANCE.PLAYER.m_event += PlayerDataObserver;
        cDataManager.INSTANCE.PLAYER.Notify();
    }

    public void Exit()
    {
        cDataManager.INSTANCE.PLAYER.SaveXML();
        cDataManager.INSTANCE.INVENTORY.SaveXML();
        FadeOut();
    }

    public void PlayerDataObserver()
    {
        m_playerGameMoneyLabel.text = cDataManager.INSTANCE.PLAYER.m_GameMoney.ToString();
        m_playerCashLabel.text = cDataManager.INSTANCE.PLAYER.m_Cash.ToString();

        m_playerHPLabel.text = "[FF0000] 체력 - " + cDataManager.INSTANCE.PLAYER.m_hp.ToString();
        m_playerAttackLabel.text = "[FF0000] 공격력 - " + cDataManager.INSTANCE.PLAYER.m_Attack.ToString();
    }

}
