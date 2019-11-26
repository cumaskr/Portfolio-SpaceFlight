using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cMapSelect : MonoBehaviour {

    public UIGrid m_grid;

    private void Start()
    {
        cSoundManager.INSTANCE.Play(1);
        cObjectPool.INSTANCE.Setting(cPrefabManager.INSTANCE.FindPrefab("prefab_Maptool_Button").name, 10, cPrefabManager.INSTANCE.FindPrefab("prefab_Maptool_Button"));

        for (int i = 0; i < cDataManager.INSTANCE.MAP.m_mapInfoList.Count; i++)
        {
            GameObject tmpObj = cObjectPool.INSTANCE.GetObject(cPrefabManager.INSTANCE.FindPrefab("prefab_Maptool_Button").name, cPrefabManager.INSTANCE.FindPrefab("prefab_Maptool_Button"));
            tmpObj.transform.SetParent(m_grid.transform);
            m_grid.transform.localPosition = new Vector3(-366, 172, 0);
            tmpObj.transform.localRotation = Quaternion.identity;
            tmpObj.transform.localScale = Vector3.one;
            tmpObj.GetComponent<cUiMatoolButton>().SetOnOFf(cDataManager.INSTANCE.MAP.m_mapInfoList[i].m_isOn);
            tmpObj.GetComponent<cUiMatoolButton>().SetScore(cDataManager.INSTANCE.MAP.m_mapInfoList[i].m_score);
            tmpObj.GetComponent<cUiMatoolButton>().m_SceneNumber = i;
        }

        cEventListner.INSTANCE.Register(cEventListner.EVENTKEY.cUiMaptoolButton_Click, SceneToGameEvent);

    }


    void SceneToGameEvent(object index, System.EventArgs e)
    {
        if ((int)index == -1) return;
        string strStage = "scInGame" + ((int)index).ToString();
        cSceneManager.INSTANCE.StartCoroutine("ChangeScene", strStage);
    }


    void SceneToMainMenu()
    {
        cSceneManager.INSTANCE.StartCoroutine("ChangeScene", "scMainMenu");
    }
  





}
