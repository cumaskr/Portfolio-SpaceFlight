using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cItem_Active_Speed : cItem {

    int m_time = 3;

    public override void Use(cUnit_Player _player = null)
    {
        _player.StartCoroutine("CoActiveSpeed", m_time);        
    }

    public override int ItemDataOut()
    {
        return m_time;
    }
}
