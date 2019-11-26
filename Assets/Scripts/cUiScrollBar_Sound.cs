using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cUiScrollBar_Sound : cUiScrollBar {

    public void SoundApplication()
    {
        m_label.text = m_scroll.value.ToString("N1");
        cSoundManager.INSTANCE.m_audioSource.volume = System.Single.Parse(m_scroll.value.ToString("N1"));
    }

    // Use this for initialization
    void Start()
    {
        m_scroll.value = cSoundManager.INSTANCE.m_audioSource.volume;
        m_scroll.onChange.Add(new EventDelegate(this, "SoundApplication"));
    }
}
