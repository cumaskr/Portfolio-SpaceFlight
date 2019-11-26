using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cUiItemList {

    UITexture[] m_itemList_UI;
    UILabel[] m_itemList_Label;
    UILabel[] m_SkillList_Label;

    public void SetData(UILabel[] _skillArr,UILabel[] _labelArr, UITexture[] _textureArr = null)
    {
        if (_textureArr == null) Debug.LogError("UIItemList가 연결되어있지 않습니다.");
        if (_labelArr == null) Debug.LogError("LabelItemList가 연결되어있지 않습니다.");

        m_itemList_UI = _textureArr;
        m_itemList_Label = _labelArr;
        m_SkillList_Label = _skillArr;
    }

    public void UI_ItemList_ChangeData()
    {        
        for (int i = 0; i < cDataManager.INSTANCE.PLAYER.m_itemList.Length; i++)
        {
            if (cDataManager.INSTANCE.PLAYER.m_itemList[i] != null)
            {
                m_itemList_UI[i].GetComponent<cUiItemList_Info>().m_index = i;
                m_itemList_UI[i].mainTexture = Resources.Load(cItem_Factory.MakePath(cDataManager.INSTANCE.PLAYER.m_itemList[i].m_name)) as Texture;
                m_itemList_UI[i].gameObject.SetActive(true);
                m_itemList_Label[i].gameObject.SetActive(true);
                m_SkillList_Label[i].gameObject.SetActive(true);
                string tmpName = cDataManager.INSTANCE.PLAYER.m_itemList[i].m_name.Remove(0, 4);
                m_itemList_Label[i].text = "[" + tmpName + "]";
                tmpName = cItem_Factory.GetItemEffect(cDataManager.INSTANCE.PLAYER.m_itemList[i].m_name);
                m_SkillList_Label[i].text = tmpName;
            }
            else
            {
                m_itemList_UI[i].GetComponent<cUiItemList_Info>().m_index = -1;
                m_itemList_UI[i].mainTexture = null;
                m_itemList_UI[i].gameObject.SetActive(false);
                m_itemList_Label[i].gameObject.SetActive(false);
                m_SkillList_Label[i].gameObject.SetActive(false);
            }
        }
    }
}
