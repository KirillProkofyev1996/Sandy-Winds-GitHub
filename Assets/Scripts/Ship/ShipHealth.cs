using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHealth : MonoBehaviour
{
    [SerializeField] private GameObject ship;
    [SerializeField] private float maxHealth;
    [SerializeField] private float timeToDestroy = 0.65f;
    [SerializeField] private float currentHealth;
    private bool IsAlive => currentHealth > 0;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void CheckIsAlive()
    {   
        if (IsAlive == false)
        {
            Destroy(ship, timeToDestroy);
        }
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;
        CheckIsAlive();
    }

    public void ImproveHealth(float health)
    {
        maxHealth += health;
        currentHealth = maxHealth;
    }
}
