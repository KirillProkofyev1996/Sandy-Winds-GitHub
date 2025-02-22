using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quests_accept : MonoBehaviour
{
    public ShipInput input;
    public GameObject dialog;
    public dialog M_dialog;
    public GameObject accept_notify;
    void OnCollisionStay(Collision collision) {
        // Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "Ship") {
            accept_notify.SetActive(true);
            if (input.GetInteractButton()) {
                accept_notify.SetActive(false);
                dialog.SetActive(true);
                M_dialog.say();
                Destroy(this.gameObject);
            }
        }   
    }

    void OnCollisionExit(Collision collision) {
        if (collision.gameObject.tag == "Ship") {
            accept_notify.SetActive(false);
        }
    }
}
