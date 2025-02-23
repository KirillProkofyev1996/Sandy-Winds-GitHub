using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quests_accept : MonoBehaviour
{
    [SerializeField] private GameObject dialogText;
    [SerializeField] private GameObject acceptNotify;
    
    [SerializeField] private ShipInput input;
    [SerializeField] private Dialog dialog;
    
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ship") 
        {
            acceptNotify.SetActive(true);

            if (input.GetInteractButton()) 
            {
                acceptNotify.SetActive(false);
                dialogText.SetActive(true);
                dialog.Say();
                Destroy(gameObject);
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
