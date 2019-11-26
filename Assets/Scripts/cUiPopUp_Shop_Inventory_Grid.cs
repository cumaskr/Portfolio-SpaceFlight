using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class cUiPopUp_Shop_Inventory_Grid : MonoBehaviour
{    
    public UISprite m_sprite;
    public UITexture    m_texture;
    public UILabel      m_label;
    public int          m_index;
                
    public void SetData(cItem _itemInfo, int _index)
    {
        m_index = _index;

        string tmpName = _itemInfo.m_name.Remove(0, 4);        
        m_label.text = "[" + tmpName + "]";        
        m_texture.mainTexture = Resources.Load(cItem_Factory.MakePath(_itemInfo.m_name)) as Texture;        
    }

    void OnClick()
    {        
        cEventListner.INSTANCE.Execute(cEventListner.EVENTKEY.cUiPopUp_Shop_Inventory_Grid_Click, m_index);        
    }    
}
