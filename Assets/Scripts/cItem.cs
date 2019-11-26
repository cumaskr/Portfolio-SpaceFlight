using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public abstract class cItem  {
        
    public string       m_name;   
    public int          m_goldPrice = 0;
    public int          m_cashPrice = 0;
    
    public abstract void Use(cUnit_Player _player = null);
    public virtual int ItemDataOut()
    {
        return -1;
    }
}
