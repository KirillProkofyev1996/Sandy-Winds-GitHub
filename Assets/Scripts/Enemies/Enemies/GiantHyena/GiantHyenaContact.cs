using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantHyenaContact : MonoBehaviour
{
    [SerializeField] private float firstPawDamage, secondPawDamage, headDamage, biteDamage;
    [SerializeField] private GiantHyenaAttack giantHyenaAttack;
    private int counts;

    private void Start()
    {
        counts = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ship"))
        {
            counts++;

            if (giantHyenaAttack.GetIsBite() == false)
            {
                if (counts == 1)
                {
                    other.GetComponent<ShipHealth>().TakeDamage(firstPawDamage);
                }
                if (counts == 2)
                {
                    other.GetComponent<ShipHealth>().TakeDamage(secondPawDamage);
                }
                if (counts == 3)
                {
                    other.GetComponent<ShipHealth>().TakeDamage(headDamage);
                    counts = 0;
                }
            }
            if (giantHyenaAttack.GetIsBite() == true)
            {
                other.GetComponent<ShipHealth>().TakeDamage(biteDamage);
                counts = 0;
            }
        }
    }
}
