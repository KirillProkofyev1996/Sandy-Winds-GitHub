using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quests_accept : MonoBehaviour
{
    public GameObject dialog;
    public dialog M_dialog;
    public GameObject pressE;
    void OnCollisionStay(Collision collision) {
        // Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "Ship") {
            pressE.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E)) {
                pressE.SetActive(false);
                dialog.SetActive(true);
                M_dialog.say();
                Destroy(this.gameObject);
            }
        }   
    }

    void OnCollisionExit(Collision collision) {
        if (collision.gameObject.tag == "Ship") {
            pressE.SetActive(false);
        }
    }
}
