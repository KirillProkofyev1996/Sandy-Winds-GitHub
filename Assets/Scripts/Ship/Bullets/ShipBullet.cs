using Unity.VisualScripting;
using UnityEngine;

public class ShipBullet : MonoBehaviour
{
    // Значение урона берется из ShipShooter,
    // чтобы можно было его увеличивать или уменьшать
    // не затрагивая префаб
    [SerializeField] private float damage;
    [SerializeField] private bool isCanSlowdownEnemyByCrossbow; // Для арбалета с возможностью замедлять противника
    [SerializeField] private bool isCanSlowdownEnemyBySidesGun; // Для боковых автоматов с возможностью замедлять противника
    [SerializeField] private bool isCanDestroyEnemyWeapon; // Для дроби пулемета с возможностью уничтожить оружие противника
    [SerializeField] private int destroyEnemyWeaponProcent; // Вероятность уничтожения оружия противника
    [SerializeField] private float damageImprovement = 50f; // Используется для улучшения всего оружия в процентах
    [SerializeField] private bool isSelfDestruct; // Для боковых автоматов, которые уничтожаются через определенное время
    [SerializeField] private float selfDestructTime; // Для боковых автоматов, которые уничтожаются через определенное время

    private void Start()
    {
        SelfDestruction();        
    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealth>().TakeDamage(damage);

            if (isCanSlowdownEnemyByCrossbow)
            {
                other.GetComponent<EnemyMovement>().SlowdownByCrossbow();
            }
            if (isCanSlowdownEnemyBySidesGun)
            {
                other.GetComponent<EnemyMovement>().SlowdownBySidesGun();
            }
            if (isCanDestroyEnemyWeapon)
            {
                if (other.GetComponent<BalloonMarauderAttack>() != null)
                {
                    other.GetComponent<BalloonMarauderAttack>().DestroyWeapon(destroyEnemyWeaponProcent);
                }
                if (other.GetComponent<SloopMarauderAttack>() != null)
                {
                    other.GetComponent<SloopMarauderAttack>().DestroyWeapon(destroyEnemyWeaponProcent);
                }
            }
        }

        Destroy(gameObject);
    }

    private void SelfDestruction()
    {
        if (isSelfDestruct)
        {
            Destroy(gameObject, selfDestructTime);
        }
    }

    // Публичный метод для чертежей прокачки урона оружия (желтый 7)
    public void ImproveProcentDamage()
    {
        damage += damage / 100 * damageImprovement;
    }
}
