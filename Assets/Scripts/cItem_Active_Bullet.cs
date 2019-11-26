using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cItem_Active_Bullet : cItem {
        
    public override void Use(cUnit_Player _player = null)
    {        
        _player.m_FireLevel++;
        if (_player.m_FireLevel > 2)
        {
            _player.m_FireLevel = 2;
            return;
        }
        if (_player.m_FireLevel == 1)
        {
            _player.m_leftFirePos.gameObject.SetActive(true);
            _player.StartCoroutine("FireLeft");
        }
        else if (_player.m_FireLevel == 2)
        {
            _player.m_rightFirePos.gameObject.SetActive(true);
            _player.StartCoroutine("FireRight");
        }        
    }

}
