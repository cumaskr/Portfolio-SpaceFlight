using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cCoroutineHelper : MonoBehaviour {

    public delegate IEnumerator DelCoroutine();
    public  Coroutine MyStartcoroutine(DelCoroutine _method)
    {
        return StartCoroutine("RunCoroutine", _method);
    }
	
    

}
