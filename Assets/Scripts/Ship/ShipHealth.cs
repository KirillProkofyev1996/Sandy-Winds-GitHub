using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHealth : MonoBehaviour
{
    [SerializeField] public float health, maxHealth;
    [SerializeField] private float strength, maxStrength;
    [SerializeField] private float timeToDestroy;

    [Header("Berserk mechanics")]
    [SerializeField] private float healthProcentLimit;
    [SerializeField] private float lessDamageMultiplier;
    [SerializeField] private bool isBerserker;

    // Корабль жив, если здоровье больше нуля
    private bool IsAlive => health > 0;

    private void Start()
    {
        health = maxHealth;
        strength = maxStrength;
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
        if (!isBerserker)
        {
            health -= damage;
            CheckIsAlive();
        }
        if (isBerserker)
        {
            if (health < health/100 * healthProcentLimit)
            {
                health -= damage/lessDamageMultiplier;
                CheckIsAlive();
            }
            else
            {
                health -= damage;
                CheckIsAlive();
            }
        }
    }

    // Публичный метод включения возможности БЕРСЕРКЕР для чертежа
    public void EnableBerserker()
    {
        isBerserker = true;
    }

    // Публичный метод увеличения максимального здоровья для чертежей 
    public void ImproveMaxHealth(float value)
    {
        maxHealth += value;
        health = maxHealth;
    }

    // Публичный метод увеличения максимального здоровья в процентах для чертежей
    public void ImproveProcentHealth(float value)
    {
        maxHealth += maxHealth / 100 * value;
        health = maxHealth;
    }

    // Публичный метод увеличения максимальной стойкости в процентах для чертежей
    public void ImproveProcentStrength(float value)
    {
        maxStrength += maxStrength / 100 * value;
        strength = maxStrength;
    }

    public float GetHealth()
    {
        return health;
    }

    public float GetStrength()
    {
        return strength;
    }
}