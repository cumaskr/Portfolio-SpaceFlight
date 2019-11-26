using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cItem_Cash : cItem {
    
    public override void Use(cUnit_Player _player = null)
    {
        cDataManager.INSTANCE.PLAYER.m_Cash += cItem_Factory.MakeItem(m_name).m_cashPrice;
        cDataManager.INSTANCE.PLAYER.Notify();
    }  
}
