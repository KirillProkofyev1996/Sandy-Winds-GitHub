using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackImprovement : MonoBehaviour
{
    [Header("Statistic")]

    
    [Header("Yellow")]
    

    [Header("Blue")]
    

    [Header("Orange")]
    

    [Header("Red")]
    

    [Header("Components")]
    private ShipShooter shipShooter;

    private void Start()
    {
        shipShooter = GetComponent<ShipShooter>();
    }

    private void Update()
    {
        
    }

    public void Yellow_1()
    {
        shipShooter.SetCannonAvailable();
    }
    public void Yellow_2()
    {
        shipShooter.SetBigCannonAvailable();
    }
    public void Yellow_3()
    {

    }
    public void Yellow_4()
    {

    }
    public void Yellow_5()
    {

    }
    public void Yellow_6()
    {
        // ...
    }
    public void Yellow_7()
    {
        
    }
    public void Yellow_8()
    {

    }
    public void Yellow_9()
    {
        // ...
    }


    public void Blue_1()
    {
        shipShooter.SetCrossbowAvailable();
    }
    public void Blue_2()
    {
        shipShooter.SetSlowCrossbowAvailable();
    }
    public void Blue_3()
    {
        // ...
    }

    public void Orange_1()
    {
        shipShooter.SetMachinegunAvailable();
    }
    public void Orange_2()
    {
        shipShooter.SetLeadMachinegunAvailable();
    }
    

    public void Red_1()
    {
        shipShooter.SetGunAvailable();
    }
    public void Red_2()
    {
        shipShooter.SetSidesGunAvailable();
    }
}
