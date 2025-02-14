using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBullet : MonoBehaviour
{
    // Значение урона берется из ShipShooter,
    // чтобы можно было его увеличивать или уменьшать
    // не затрагивая префаб
    public float damage; 

    private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealth>().TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
