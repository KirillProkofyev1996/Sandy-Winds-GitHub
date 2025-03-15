using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quests_accept : MonoBehaviour
{
    [SerializeField] private InputSchemeSwitcher inputSchemeSwitcher;
    [SerializeField] private GameObject dialogText;
    [SerializeField] private GameObject acceptNotify;
    
    [SerializeField] private ShipInput input;
    [SerializeField] private GameObject dialog;
    [SerializeField] private GameObject buttons;
    [SerializeField] private Dialog M_dialog;
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ship") 
        {
            acceptNotify.SetActive(true);

            if (input.GetInteractButton()) 
            {
                acceptNotify.SetActive(false);
                dialog.SetActive(true);
                inputSchemeSwitcher.SetUiInput();
                M_dialog.Say();
                Destroy(this.gameObject);
            }
        }   
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ship") 
        {
            acceptNotify.SetActive(false);
        }
    }
}
