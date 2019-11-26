using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cFadeInOut : MonoBehaviour {

    static cFadeInOut m_instance;

    public static cFadeInOut INSTANCE
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<cFadeInOut>() as cFadeInOut;
                if (m_instance == null)
                {
                    Debug.LogError("페이드인 아웃 싱글톤 객체가 생성되지 않았습니다.");
                }
            }
            return m_instance;
        }
    }


    Coroutine m_coroutineIsDone = null;


    public GameObject m_FadeInoutObj;
    public UILabel m_LoadingLabel;
    public UITexture m_Loadingtexture;
    float fTime = 0.0f;

    UISprite m_sprite;

    float m_t;
    Color m_color;
    AsyncOperation m_sceneOperation;
    string m_sceneName = null;


    private void Awake()
    {
        m_sprite = m_FadeInoutObj.GetComponent<UISprite>();
        m_color = m_sprite.color;
        DontDestroyOnLoad(gameObject);
    }
    
    IEnumerator LoadingBar()
    {        
        m_sceneOperation = SceneManager.LoadSceneAsync(m_sceneName);
        m_sceneOperation.allowSceneActivation = false;
        m_LoadingLabel.text = "Loading...\n" + (m_sceneOperation.progress * 100).ToString("N1");

        while (true)
        {                        
            yield return StartCoroutine("FadeCoroutine",true);
            m_LoadingLabel.text = "Loading...\n" + (m_sceneOperation.progress * 100).ToString("N1");
            if (m_sceneOperation.progress >= 0.9f)
            {
                m_LoadingLabel.text = "Loading...\n" + (100.0f).ToString("N1");
                m_sceneOperation.allowSceneActivation = true;
                StartCoroutine("FadeCoroutine", false);
                break;
            }
        }
        yield return null;
    }

    public IEnumerator FadeStart(string _sceneName)
    {
        m_sceneName = _sceneName;
        StartCoroutine("LoadingBar");        
        yield break;            
    }

    public IEnumerator FadeCoroutine(bool _isIn)
    {        
        m_t = 0.0f;
        m_FadeInoutObj.SetActive(true);

        while (true)
        {
            m_LoadingLabel.text = "Loading...\n" + (m_sceneOperation.progress * 100).ToString("N1");
            //t값이 1이 되었다면 타출
            if (m_t >= 1)
            {
                if (_isIn)
                {
                    m_sprite.color = new Color(m_color.r, m_color.g, m_color.b, 1);
                }
                else
                {
                    m_sprite.color = new Color(m_color.r, m_color.g, m_color.b, 0);
                }
                m_FadeInoutObj.SetActive(false);
                yield break;
            }  
                         
            //알파값 증가 감소 코드                         
            if (_isIn) m_sprite.color = new Color(m_color.r, m_color.g, m_color.b, Mathf.Lerp(0, 255, m_t) / 255.0f);
            else m_sprite.color = new Color(m_color.r, m_color.g, m_color.b, Mathf.Lerp(255, 0, m_t) / 255.0f);

            m_t += 0.08f;
            yield return new WaitForSeconds(0.02f);            
        }                
    }
}
