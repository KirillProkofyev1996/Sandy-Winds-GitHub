using Unity.VisualScripting;
using UnityEngine;

public class ShipBullet : MonoBehaviour
{
    // Значение урона берется из ShipShooter,
    // чтобы можно было его увеличивать или уменьшать
    // не затрагивая префаб
    [SerializeField] private float damage;
    [SerializeField] private bool isCanSlowdownEnemy;
    [SerializeField] private float damageImprovement = 50f; // Используется для улучшения всего оружия в процентах 

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

    // Публичный метод для чертежей прокачки урона оружия (желтый 7)
    public void ImproveProcentDamage()
    {
        damage += damage / 100 * damageImprovement;
    }
}
