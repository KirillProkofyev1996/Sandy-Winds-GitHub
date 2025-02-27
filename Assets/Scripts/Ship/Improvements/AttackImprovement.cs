using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackImprovement : MonoBehaviour
{
    [Header("Statistic")]
    [SerializeField] private Text health;
    [SerializeField] private Text strength;
    [SerializeField] private Text speed;
    [SerializeField] private Text turbo;
    [SerializeField] private Text damage;
    
    [Header("Yellow")]
    [SerializeField] private InputField yellow1_value1;

    /*[Header("Blue")]
    [SerializeField] private InputField blue1_value1;

    [Header("Orange")]
    [SerializeField] private InputField orange1_value1;

    [Header("Red")]
    [SerializeField] private InputField red1_value1;*/

    [Header("Components")]
    private ShipHealth shipHealth;
    private ShipMovement shipMovement;
    private ShipShooter shipShooter;

    private void Start()
    {
        shipHealth = GetComponent<ShipHealth>();
        shipMovement = GetComponent<ShipMovement>();
        shipShooter = GetComponent<ShipShooter>();
    }

    private void Update()
    {
        health.text = shipHealth.GetHealth().ToString();
        strength.text = shipHealth.GetStrength().ToString();
        speed.text = shipMovement.GetSpeed().ToString();
        turbo.text = shipMovement.GetTurbo().ToString();
        damage.text = shipShooter.GetDamage().ToString();
    }

    public void Yellow_1()
    {
        
    }
}
