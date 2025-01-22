using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHealth : MonoBehaviour
{
    [SerializeField] private GameObject ship;
    [SerializeField] private float maxHealth;
    [SerializeField] private float timeToDestroy = 0.65f;
    private float curHealth;
    private bool IsAlive => curHealth > 0;

    private void Start()
    {
        curHealth = maxHealth;
    }

    private void CheckIsAlive()
    {   
        if (IsAlive == false)
        {
            Destroy(ship, timeToDestroy);
        }
    }

    public void TakeDamage(float damage)
    {
        curHealth -= damage;
        
        CheckIsAlive();
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }
}
