using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class ShipShooter : MonoBehaviour
{
    [Header("Cannon settings")]
    [SerializeField] private Rigidbody cannon;
    [SerializeField] private Transform cannonShootPoint;
    [SerializeField] private float cannonSpeed;
    [SerializeField] private float cannonRate;
    private string cannonWeapon = "Cannon";


    [Header("Crossbow settings")]
    [SerializeField] private Rigidbody crossbow;
    [SerializeField] private Transform crossbowShootPoint;
    [SerializeField] private float crossbowSpeed;
    [SerializeField] private float crossbowRate;
    private string crossbowWeapon = "Crossbow";


    [Header("Machinegun settings")]
    [SerializeField] private Rigidbody machinegun;
    [SerializeField] private Transform machinegunShootPoint;
    [SerializeField] private float machinegunSpeed;
    [SerializeField] private float machinegunRate;
    private string machinegunWeapon = "Machinegun";


    [Header("Gun settings")]
    [SerializeField] private Rigidbody gun;
    [SerializeField] private Transform gunShootPoint;
    [SerializeField] private float gunSpeed;
    [SerializeField] private float gunRate;
    private string gunWeapon = "Gun";


    [Header("Settings")]
    //[SerializeField] private Rigidbody bullet;
    //[SerializeField] private Transform shootPoint;
    //[SerializeField] private float speed;
    //[SerializeField] private float fireRate;
    [SerializeField] private float maxDistance;
    [SerializeField] private Transform shootPoint;
    private string currentWeapon;
    private float currentSpeed;
    private float currentRate;
    private float distance;
    private Vector3 direction;

    
    [Header("Components")]
    [SerializeField] private Aim aim;
    private ShipInput shipInput;

    private void Start()
    {
        shipInput = GetComponent<ShipInput>();
    }

    private void Update()
    {
        SwitchWeapon();
        Shoot();
        aim.ShowRedAim(distance, maxDistance);
    }

    private void Shoot()
    {
        distance = Vector3.Distance(aim.GetAimPosition(), shootPoint.position);
        direction = (aim.GetAimPosition() - shootPoint.position).normalized;

        if (currentWeapon == cannonWeapon)
        {
            if (shipInput.GetShootButton() && Time.time >= currentRate && distance <= maxDistance)
            {
                Rigidbody currentCannon = Instantiate(cannon, cannonShootPoint.position, Quaternion.identity);
                currentCannon.velocity = direction * cannonSpeed;
                currentCannon.transform.LookAt(currentCannon.transform.position + currentCannon.velocity);
                currentRate = Time.time + cannonRate;
            }
        }
        if (currentWeapon == crossbowWeapon)
        {
            if (shipInput.GetShootButton() && Time.time >= currentRate && distance <= maxDistance)
            {
                Rigidbody currentCrossbow = Instantiate(crossbow, crossbowShootPoint.position, Quaternion.identity);
                currentCrossbow.velocity = direction * crossbowSpeed;
                currentCrossbow.transform.LookAt(currentCrossbow.transform.position + currentCrossbow.velocity);
                currentRate = Time.time + crossbowRate;
            }
        }
        if (currentWeapon == machinegunWeapon)
        {
            if (shipInput.GetShootButton() && Time.time >= currentRate && distance <= maxDistance)
            {
                Rigidbody currentMachinegun = Instantiate(machinegun, machinegunShootPoint.position, Quaternion.identity);
                currentMachinegun.velocity = direction * machinegunSpeed;
                currentMachinegun.transform.LookAt(currentMachinegun.transform.position + currentMachinegun.velocity);
                currentRate = Time.time + machinegunRate;
            }
        }
        if (currentWeapon == gunWeapon)
        {
            if (shipInput.GetShootButton() && Time.time >= currentRate && distance <= maxDistance)
            {
                Rigidbody currentGun = Instantiate(gun, gunShootPoint.position, Quaternion.identity);
                currentGun.velocity = direction * gunSpeed;
                currentGun.transform.LookAt(currentGun.transform.position + currentGun.velocity);
                currentRate = Time.time + gunRate;
            }
        }
    }

    private void SwitchWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = cannonWeapon;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = crossbowWeapon;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentWeapon = machinegunWeapon;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentWeapon = gunWeapon;
        }
    }
}
