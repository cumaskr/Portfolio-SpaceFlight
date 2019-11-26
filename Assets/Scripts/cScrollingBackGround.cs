using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cScrollingBackGround : MonoBehaviour {

    UITexture m_texture;
    float m_speed;

	// Use this for initialization
	void Start () {
        m_texture = GetComponent<UITexture>();
        m_speed = 0.05f;
	}
	
	// Update is called once per frame
	void Update () {
        m_texture.uvRect = new Rect(m_texture.uvRect.position - new Vector2(m_speed * Time.deltaTime ,0), Vector2.one);  
	}
}
