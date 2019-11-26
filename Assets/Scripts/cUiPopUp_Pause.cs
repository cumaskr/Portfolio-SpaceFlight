using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cUiPopUp_Pause : cUiPopUp
{
    GameObject m_OptionObject = null;



    public void ReturnMapSelect()
    {
        Time.timeScale = 1.0f;
        cSceneManager.INSTANCE.StartCoroutine("ChangeScene", "scMapSelect");
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        FadeOut();
    }

    public void MakeOption()
    {
        FadeOut();
        if (m_OptionObject == null)
        {
            m_OptionObject = cPrefabManager.INSTANCE.MakePrefab("prefab_Popup_Option", "UI Front");
            m_OptionObject.GetComponent<cUiPopUp>().FadeIn();
        }
        else
        {
            m_OptionObject.GetComponent<cUiPopUp>().FadeIn();
        }
    }
}
