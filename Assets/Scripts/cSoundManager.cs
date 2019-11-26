using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cSoundManager : MonoBehaviour {


    static cSoundManager m_instance;

    public static cSoundManager INSTANCE
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<cSoundManager>() as cSoundManager;
                if (m_instance == null)
                {
                    Debug.LogError("사운드 매니져 싱글톤 객체 생성이 안되었습니다.");
                }
            }
            return m_instance;
        }
    }

    public AudioSource m_audioSource;
    public AudioClip[] m_list;

    private void Awake()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    public void Play(int _index)
    {
        m_audioSource.clip = m_list[_index];
        m_audioSource.Play();
    }    
}
