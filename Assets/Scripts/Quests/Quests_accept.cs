using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quests_accept : MonoBehaviour
{
    [SerializeField] private ShipInput input;
    [SerializeField] private GameObject dialog;
    [SerializeField] private dialog M_dialog;
    [SerializeField] private GameObject accept_notify;
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ship") 
        {
            accept_notify.SetActive(true);

            if (input.GetInteractButton()) 
            {
                accept_notify.SetActive(false);
                dialog.SetActive(true);
                M_dialog.say();
                Destroy(gameObject);
            }
        }   
    }

    void OnCollisionExit(Collision collision) 
    {
        if (collision.gameObject.tag == "Ship") 
        {
            accept_notify.SetActive(false);
        }
    }
}
