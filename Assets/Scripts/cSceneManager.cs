using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class cSceneManager : MonoBehaviour {

    static cSceneManager m_instance;

    public static cSceneManager INSTANCE
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<cSceneManager>() as cSceneManager;
                if (m_instance == null)
                {
                    Debug.LogError("씬 매니져 싱글톤 객체 생성이 안되었습니다.");
                }
            }
            return m_instance;
        }
    }
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (SceneManager.GetActiveScene().buildIndex == 0) StartCoroutine("FirstScene");
    }

    public IEnumerator ChangeScene(string _sceneName)
    {
        cFadeInOut.INSTANCE.StartCoroutine("FadeStart", _sceneName);
        yield break;
        //if (_sceneName.Equals("scMainMenu")) yield return new WaitForSeconds(2.0f);

        //switch (_sceneName)
        //{
        //    case "scMainMenu":
        //        cFadeInOut.INSTANCE.StartCoroutine("FadeStart", "scMainMenu");
        //        yield break;
        //    case "scMapSelect":
        //        cFadeInOut.INSTANCE.StartCoroutine("FadeStart", "scMapSelect");
        //        yield break;
        //    case "scInGame0":
        //        cFadeInOut.INSTANCE.StartCoroutine("FadeStart", "scInGame0");
        //        yield break;
        //    case "scInGame1":
        //        cFadeInOut.INSTANCE.StartCoroutine("FadeStart", "scInGame1");
        //        yield break;
        //    case "scInGame2":
        //        cFadeInOut.INSTANCE.StartCoroutine("FadeStart", "scInGame2");
        //        yield break;
        //}
    }

    IEnumerator FirstScene()
    {
        yield return new WaitForSeconds(2.0f);
        StartCoroutine("ChangeScene", "scMainMenu");
        yield break;
    }    
}
