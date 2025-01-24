using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeffenceImprovement : MonoBehaviour
{
    [Header("Statistic")]
    [SerializeField] private Text health, strength, speed, turbo, damage;
    
    [Header("Yellow")]
    [SerializeField] private InputField yellow1_value1;

    [Header("Blue")]
    [SerializeField] private InputField blue1_value1;

    [Header("Orange")]
    [SerializeField] private InputField orange1_value1;

    [Header("Red")]
    [SerializeField] private InputField red1_value1;

    [Header("Components")]
    private ShipHealth shipHealth;
    private ShipMovement shipMovement;

    private void Start()
    {
        shipHealth = GetComponent<ShipHealth>();
        shipMovement = GetComponent<ShipMovement>();

        //health.text = ;
        //strength.text = ;
        //speed.text = ;
        //turbo.text = ;
    }

    public void Yellow_1()
    {
        shipHealth.ImproveHealth(float.Parse(yellow1_value1.text));
    }
}
