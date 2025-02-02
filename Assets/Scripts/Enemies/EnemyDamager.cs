using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ship"))
        {
            other.GetComponent<ShipHealth>().TakeDamage(damage);
        }
    }
}
