using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cPowerCollider : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("TAG_ENEMY"))
        {
            other.GetComponent<cUnit>().Die();
        }
    }    
}
