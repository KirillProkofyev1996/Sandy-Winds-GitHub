using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHiter : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float timeToHit;

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }
    }
}