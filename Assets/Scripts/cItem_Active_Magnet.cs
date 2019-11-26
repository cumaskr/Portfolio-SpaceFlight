using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cItem_Active_Magnet : cItem {

    int m_time = 10;

    public override void Use(cUnit_Player _player = null)
    {
        _player.StartCoroutine("CoActiveMagnet", m_time);                                   
    }

    public override int ItemDataOut()
    {
        return m_time;
    }
}
