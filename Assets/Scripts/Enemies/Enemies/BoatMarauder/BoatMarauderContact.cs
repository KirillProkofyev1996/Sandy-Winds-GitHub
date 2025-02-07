using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BoatMarauderContact : MonoBehaviour
{
    [SerializeField] private PlayableDirector cameraShakeTimeline;
    [SerializeField] private float damage;
    private bool isContact;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ship"))
        {
            cameraShakeTimeline.Play();

            isContact = true;

            other.GetComponent<ShipHealth>().TakeDamage(damage);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ship"))
        {
            isContact = false;
        }
    }

    public bool GetIsContact()
    {
        return isContact;
    }
}