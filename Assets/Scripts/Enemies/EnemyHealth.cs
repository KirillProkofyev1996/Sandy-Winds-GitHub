using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float timeToDestroy;
    [SerializeField] private float currentHealth;

    // Противник жив, если здоровье больше нуля
    private bool IsAlive => currentHealth > 0;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void CheckIsAlive()
    {
        if (IsAlive == false)
        {
            Destroy(gameObject, timeToDestroy);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        CheckIsAlive();
    }

    public float GetHealth()
    {
        return currentHealth;
    }
}
