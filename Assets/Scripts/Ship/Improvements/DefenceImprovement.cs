using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefenceImprovement : MonoBehaviour
{
    [Header("Statistic")]
    [SerializeField] private Text health;
    [SerializeField] private Text strength;
    [SerializeField] private Text speed;
    [SerializeField] private Text turbo;
    [SerializeField] private Text damage;
    [SerializeField] private Text crew;
    [SerializeField] private Text reload;
    
    [Header("Yellow")]

    // Чертеж 1 (желтый)
    [SerializeField] private InputField yellow1_value1;

    // Чертеж 2 (желтый)
    [SerializeField] private InputField yellow2_value1;

    // Чертеж 3 (желтый)
    [SerializeField] private InputField yellow3_value1;

    // Чертеж 4 (желтый)
    [SerializeField] private InputField yellow4_value1;

    // Чертеж 5 (желтый)
    [SerializeField] private InputField yellow5_value1;

    // Чертеж 6 (желтый)
    [SerializeField] private InputField yellow6_value1;

    // Чертеж 7 (желтый)
    [SerializeField] private InputField yellow7_value1;
    [SerializeField] private InputField yellow7_value2;

    // Чертеж 8 (желтый)
    [SerializeField] private InputField yellow8_value1;

    // Чертеж 10 (желтый)
    [SerializeField] private InputField yellow10_value1;
    [SerializeField] private InputField yellow10_value2;

    // Чертеж 11 (желтый)
    [SerializeField] private InputField yellow11_value1;

    [Header("Blue")]

    // Чертеж 1 (синий)
    [SerializeField] private InputField blue1_value1;
    [SerializeField] private InputField blue1_value2;

    // Чертеж 2 (синий)
    [SerializeField] private InputField blue2_value1;

    // Чертеж 4 (синий)
    [SerializeField] private InputField blue4_value1;
    [SerializeField] private InputField blue4_value2;

    // Чертеж 5 (синий)
    [SerializeField] private InputField blue5_value1;

    // Чертеж 6 (синий)
    [SerializeField] private InputField blue6_value1;

    // Чертеж 7 (синий)
    [SerializeField] private InputField blue7_value1;
    [SerializeField] private InputField blue7_value2;

    /*[Header("Orange")]
    [SerializeField] private InputField orange1_value1;

    [Header("Red")]
    [SerializeField] private InputField red1_value1;*/

    [Header("Components")]
    private ShipHealth shipHealth;
    private ShipMovement shipMovement;
    private ShipShooter shipShooter;
    private ShipCrew shipCrew;

    private void Start()
    {
        shipHealth = GetComponent<ShipHealth>();
        shipMovement = GetComponent<ShipMovement>();
        shipShooter = GetComponent<ShipShooter>();
        shipCrew = GetComponent<ShipCrew>();
    }

    private void Update()
    {
        health.text = shipHealth.GetHealth().ToString();
        strength.text = shipHealth.GetStrength().ToString();
        speed.text = shipMovement.GetSpeed().ToString();
        turbo.text = shipMovement.GetTurbo().ToString();
        damage.text = shipShooter.GetDamage().ToString();
        reload.text = shipShooter.GetReload().ToString();
        crew.text = shipCrew.GetCrew().ToString();
    }

    // Метод для чертежа 1 (желтый)
    public void Yellow_1()
    {
        shipHealth.ImproveMaxHealth(float.Parse(yellow1_value1.text));
    }

    // Метод для чертежа 2 (желтый)
    public void Yellow_2()
    {
        shipHealth.ImproveProcentStrength(float.Parse(yellow2_value1.text));
    }

    // Метод для чертежа 3 (желтый)
    public void Yellow_3()
    {
        shipMovement.ImproveProcentSpeed(float.Parse(yellow3_value1.text));
    }

    // Метод для чертежа 4 (желтый)
    public void Yellow_4()
    {
        shipHealth.ImproveMaxHealth(float.Parse(yellow4_value1.text));
    }

    // Метод для чертежа 5 (желтый)
    public void Yellow_5()
    {
        shipHealth.ImproveMaxHealth(float.Parse(yellow5_value1.text));
    }

    // Метод для чертежа 6 (желтый)
    public void Yellow_6()
    {
        shipHealth.ImproveMaxHealth(float.Parse(yellow6_value1.text));
    }

    // Метод для чертежа 7 (желтый)
    public void Yellow_7()
    {
        shipShooter.ImproveProcentDamage(float.Parse(yellow7_value1.text));
        shipMovement.ImproveProcentSpeed(float.Parse(yellow7_value2.text));
    }

    // Метод для чертежа 8 (желтый)
    public void Yellow_8()
    {
        // Защита от ракет противника
        shipMovement.ImproveProcentSpeed(float.Parse(yellow8_value1.text));
    }

    // Метод для чертежа 9 (желтый)
    public void Yellow_9()
    {
        // ПВО на корабле
    }

    // Метод для чертежа 10 (желтый)
    public void Yellow_10()
    {
        shipHealth.ImproveProcentStrength(float.Parse(yellow10_value1.text));
        shipMovement.ImproveProcentSpeed(float.Parse(yellow10_value2.text));
    }

    // Метод для чертежа 11 (желтый)
    public void Yellow_11()
    {
        shipHealth.ImproveMaxHealth(float.Parse(yellow11_value1.text));
    }

    // Метод для чертежа 1 (синий)
    public void Blue_1()
    {
        shipHealth.ImproveProcentHealth(float.Parse(blue1_value1.text));
        shipMovement.ImproveProcentSpeed(float.Parse(blue1_value2.text));
    }

    // Метод для чертежа 2 (синий)
    public void Blue_2()
    {
        shipMovement.ImproveProcentSpeed(float.Parse(blue2_value1.text));
    }

    // Метод для чертежа 3 (синий)
    public void Blue_3()
    {
        // Возможность планировать. Боковые крылья.
    }

    // Метод для чертежа 4 (синий)
    public void Blue_4()
    {
        shipShooter.ImproveProcentReload(float.Parse(blue4_value1.text));
        shipMovement.ImproveProcentSpeed(float.Parse(blue4_value2.text));
    }

    // Метод для чертежа 5 (синий)
    public void Blue_5()
    {
        shipHealth.ImproveProcentStrength(float.Parse(blue5_value1.text));
    }

    // Метод для чертежа 6 (синий)
    public void Blue_6()
    {
        shipMovement.ImproveProcentSpeed(float.Parse(blue6_value1.text));
    }

    // Метод для чертежа 7 (синий)
    public void Blue_7()
    {
        shipHealth.ImproveMaxHealth(float.Parse(blue7_value1.text));
        shipMovement.ImproveProcentSpeed(float.Parse(blue7_value2.text));
    }

    // Метод для чертежа 8 (синий)
    public void Blue_8()
    {
        // ...
    }

    // Метод для чертежа 9 (синий)
    public void Blue_9()
    {
        // Воздушные шары. Недолгий полет катера.
    }
}
