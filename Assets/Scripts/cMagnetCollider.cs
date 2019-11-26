using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cMagnetCollider : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("TAG_ITEM"))
        {
            other.GetComponent<cUnit_Item>().m_isMagnet = true;
        }
    }
}
