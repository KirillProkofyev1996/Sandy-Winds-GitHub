using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackImprovement : MonoBehaviour
{
    [Header("Statistic")]
    [SerializeField] private Text cannon;
    [SerializeField] private Text crossbow;
    [SerializeField] private Text machinegun;
    [SerializeField] private Text gun;
    
    [Header("Yellow")]
    [SerializeField] private InputField yellow1_value1;
    [SerializeField] private InputField yellow2_value1;

    [Header("Blue")]
    [SerializeField] private InputField blue1_value1;
    [SerializeField] private InputField blue2_value1;

    [Header("Orange")]
    [SerializeField] private InputField orange1_value1;

    [Header("Red")]
    [SerializeField] private InputField red1_value1;

    [Header("Components")]
    private ShipShooter shipShooter;

    private void Start()
    {
        shipShooter = GetComponent<ShipShooter>();
    }

    private void Update()
    {
        cannon.text = shipShooter.GetCannonDamage().ToString();
        crossbow.text = shipShooter.GetCrossbowDamage().ToString();
        machinegun.text = shipShooter.GetMachinegunDamage().ToString();
        gun.text = shipShooter.GetGunDamage().ToString();
    }

    public void Yellow_1()
    {
        shipShooter.ImproveCannonWeapon(float.Parse(yellow1_value1.text));
    }

    public void Yellow_2()
    {
        shipShooter.ImproveCannonWeapon(float.Parse(yellow2_value1.text));
    }


    public void Blue_1()
    {
        shipShooter.ImproveCrossbowWeapon(float.Parse(blue1_value1.text));
    }

    public void Blue_2()
    {
        shipShooter.ImproveCrossbowWeapon(float.Parse(blue2_value1.text));
        shipShooter.SetSlowdownEnemyByCrossbow();
    }


    public void Orange_1()
    {
        shipShooter.ImproveMachinegunWeapon(float.Parse(orange1_value1.text));
    }
    

    public void Red_1()
    {
        shipShooter.ImproveGunWeapon(float.Parse(red1_value1.text));
    }
}
