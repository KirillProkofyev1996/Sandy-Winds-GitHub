using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] public float currentHealth;
    [SerializeField] private float strength;
    [SerializeField] private float timeToDestroy;

    // Корабль жив, если здоровье больше нуля
    private bool IsAlive => currentHealth > 0;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    // Проверка на то, мертв ли корабль
    private void CheckIsAlive()
    {   
        if (IsAlive == false)
        {
            Destroy(gameObject, timeToDestroy);
        }
    }

    // Публичный метод для нанесения урона кораблю
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        CheckIsAlive();
    }

    // Публичный метод увеличения здоровья для чертежей 
    public void ImproveHealth(float health)
    {
        maxHealth += health;
        currentHealth = maxHealth;
    }

    public float GetHealth()
    {
        return currentHealth;
    }

    public float GetStrength()
    {
        return strength;
    }
}
