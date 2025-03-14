using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quests_accept : MonoBehaviour
{
    [SerializeField] private GameObject dialogText;
    [SerializeField] private GameObject acceptNotify;
    
    [SerializeField] private ShipInput input;
    [SerializeField] private GameObject dialog;
    [SerializeField] private GameObject buttons;
    [SerializeField] private dialog M_dialog;
    [SerializeField] private GameObject accept_notify;
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ship") 
        {
            acceptNotify.SetActive(true);

            if (input.GetInteractButton()) 
            {
                accept_notify.SetActive(false);
                dialog.SetActive(true);
                buttons.SetActive(false);
                M_dialog.say();
                Destroy(this.gameObject);
            }
        }   
    }

    void OnCollisionExit(Collision collision) 
    {
        if (collision.gameObject.tag == "Ship") 
        {
            acceptNotify.SetActive(false);
        }
    }
}
