using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float health, maxHealth;
    [SerializeField] private float timeToDestroy;
    [SerializeField] private bool isBoss;

    private void Start()
    {
        health = maxHealth;
    }

    // Противник жив, если здоровье больше нуля
    private bool IsAlive => health > 0;

    private void CheckIsAlive()
    {
        if (IsAlive == false)
        {
            Destroy(gameObject, timeToDestroy);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        CheckIsAlive();
    }

    public float GetHealth()
    {
        return health;
    }
}
