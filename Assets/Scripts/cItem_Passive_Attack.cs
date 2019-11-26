using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cItem_Passive_Attack : cItem {

    int m_itemData = 25;
    public override void Use(cUnit_Player _player = null)
    {
        cDataManager.INSTANCE.PLAYER.m_Attack += m_itemData;
        cDataManager.INSTANCE.PLAYER.Notify();
    }

    public override int ItemDataOut()
    {
        return m_itemData;
    }
}
