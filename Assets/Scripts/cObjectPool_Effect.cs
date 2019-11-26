using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cObjectPool_Effect : MonoBehaviour {


    public void Setting(Vector3 _startPos, string _name)
    {
        gameObject.name = _name;
        transform.localPosition = _startPos;        
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }
    	
	// Update is called once per frame
	void Update () {


        if (Time.timeScale < 0.01f)
        {
            GetComponent<ParticleSystem>().Simulate(Time.unscaledDeltaTime, true, false);
        }


        if (GetComponent<ParticleSystem>().isPlaying == false)
        {
            cObjectPool.INSTANCE.ReturnObject(cPrefabManager.INSTANCE.FindPrefab(gameObject.name).name, gameObject);
        }

    }
}
