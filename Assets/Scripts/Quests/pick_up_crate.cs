using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pick_up_crate : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ship")
        {
            Destroy(this.gameObject);
            //call end quest
        }
    }
}
