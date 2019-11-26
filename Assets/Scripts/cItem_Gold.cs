using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cItem_Gold : cItem {

    public override void Use(cUnit_Player _player = null)
    {
        cDataManager.INSTANCE.PLAYER.m_GameMoney += cItem_Factory.MakeItem(m_name).m_goldPrice;
        cDataManager.INSTANCE.PLAYER.Notify();        
    }
}
