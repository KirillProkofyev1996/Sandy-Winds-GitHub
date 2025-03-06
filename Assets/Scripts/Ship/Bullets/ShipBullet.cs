using Unity.VisualScripting;
using UnityEngine;

public class ShipBullet : MonoBehaviour
{
    // Значение урона берется из ShipShooter,
    // чтобы можно было его увеличивать или уменьшать
    // не затрагивая префаб
    private float damage;
    private bool isCanSlowdownEnemy; 

    private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealth>().TakeDamage(damage);

            if (isCanSlowdownEnemy)
            {
                other.GetComponent<EnemyMovement>().SlowdownByCrossbow();
            }
        }

        Destroy(gameObject);
    }

    public void SetDamage(float value)
    {
        damage = value;
    }

    public void CanSlowdownEnemy(bool value)
    {
        isCanSlowdownEnemy = value;
    }
}
