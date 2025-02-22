using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ShipShooter : MonoBehaviour
{
    [Header("Cannon settings")]
    [SerializeField] private float cannonDamage;
    [SerializeField] private Rigidbody cannon;
    [SerializeField] private Transform cannonShootPoint;
    [SerializeField] private float cannonSpeed;
    [SerializeField] private float cannonReload;
    [SerializeField] private float cannonDistance;
    [SerializeField] private float cannonShootAngle; // Если угол = 50
    [SerializeField] private float cannonAngleMultiplier; // То множитель = 1.5
    private string cannonWeapon = "Cannon";


    [Header("Crossbow settings")]
    [SerializeField] private float crossbowDamage;
    [SerializeField] private Rigidbody crossbow;
    [SerializeField] private Transform crossbowFrontShootPoint;
    [SerializeField] private Transform crossbowBackShootPoint;
    [SerializeField] private float crossbowSpeed;
    [SerializeField] private float crossbowReload;
    [SerializeField] private float crossbowDistance;
    private string crossbowWeapon = "Crossbow";


    [Header("Machinegun settings")]
    [SerializeField] private float machinegunDamage;
    [SerializeField] private Rigidbody machinegun;
    [SerializeField] private Transform machinegunShootPoint;
    [SerializeField] private float machinegunSpeed;
    [SerializeField] private float machinegunReload;
    [SerializeField] private float machinegunDistance;
    [SerializeField] private int machinegunCounts;
    [SerializeField] private float machinegunAngle;
    private string machinegunWeapon = "Machinegun";


    [Header("Gun settings")]
    [SerializeField] private float gunDamage;
    [SerializeField] private Rigidbody gun;
    [SerializeField] private Transform gunShootPoint;
    [SerializeField] private float gunSpeed;
    [SerializeField] private float gunReload;
    [SerializeField] private float gunDistance;
    private string gunWeapon = "Gun";


    [Header("Cannon draw trajectory")]
    [SerializeField] private int resolution;
    [SerializeField] private float correctionFactor; // При угле = 50, множителе 1.5, фактор = -0.28
    private Vector3 launchDirection;
    private float gravityAcceleration;


    [Header("Settings")]
    [SerializeField] private float currentDamage;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float aimOffsetY;
    [SerializeField] private int maxWeaponCount;
    private bool isCanShoot;
    private float weaponDistance;
    private string currentWeapon;
    private float currentReload;
    private float distance;
    private Vector3 direction;
    private Vector3 aimPosition;
    private int weaponCount;

    
    [Header("Components")]
    [SerializeField] private Aim aim;
    [SerializeField] private LineRenderer lineRenderer;
    private ShipInput shipInput;

    private void Start()
    {
        shipInput = GetComponent<ShipInput>();
        isCanShoot = true;
    }

    private void Update()
    {
        AimPosition();
        Direction();
        Distance();

        SwitchWeapon();
        Shoot();
    }

    private void AimPosition()
    {
        aimPosition = new Vector3(aim.GetAimPosition().x, aim.GetAimPosition().y + aimOffsetY, aim.GetAimPosition().z);
    }

    private void Direction()
    {
        direction = aimPosition - shootPoint.position;
        direction.Normalize();
    }

    private void Distance()
    {
        distance = Vector3.Distance(aimPosition, shootPoint.position);
    }

    // Метод стрельбы из разных видов оружия
    private void Shoot()
    {
        if (isCanShoot)
        {
            // Стрельба из пушки
            if (currentWeapon == cannonWeapon)
            {
                lineRenderer.enabled = true;
                CannonShootTarget();
                CannonShowTrajectory();

                if (shipInput.GetShootButton() && Time.time >= currentReload && distance <= cannonDistance)
                {
                    Rigidbody currentCannon = Instantiate(cannon, cannonShootPoint.position, cannonShootPoint.rotation);
                    currentCannon.velocity = launchDirection * cannonSpeed;
                    currentCannon.transform.LookAt(currentCannon.transform.position + currentCannon.velocity);

                    ShipBullet bullet = currentCannon.GetComponent<ShipBullet>();
                    bullet.SetDamage(cannonDamage);

                    currentReload = Time.time + cannonReload;
                }
            }
            else
            {
                lineRenderer.enabled = false;
            }

            // Стрельба из арбалета
            if (currentWeapon == crossbowWeapon)
            {
                if (shipInput.GetShootButton() && Time.time >= currentReload && distance <= crossbowDistance)
                {
                    Rigidbody currentFrontCrossbow = Instantiate(crossbow, crossbowFrontShootPoint.position, crossbowFrontShootPoint.rotation);
                    currentFrontCrossbow.velocity = direction * crossbowSpeed;
                    currentFrontCrossbow.transform.LookAt(currentFrontCrossbow.transform.position + currentFrontCrossbow.velocity);

                    Rigidbody currentBackCrossbow = Instantiate(crossbow, crossbowBackShootPoint.position, crossbowBackShootPoint.rotation);
                    currentBackCrossbow.velocity = direction * crossbowSpeed;
                    currentBackCrossbow.transform.LookAt(currentBackCrossbow.transform.position + currentBackCrossbow.velocity);

                    ShipBullet bullet1 = currentFrontCrossbow.GetComponent<ShipBullet>();
                    bullet1.SetDamage(crossbowDamage);

                    ShipBullet bullet2 = currentBackCrossbow.GetComponent<ShipBullet>();
                    bullet2.SetDamage(crossbowDamage);

                    currentReload = Time.time + crossbowReload;
                }
            }

            // Стрельба из пулемета
            if (currentWeapon == machinegunWeapon)
            {
                if (shipInput.GetShootButton() && Time.time >= currentReload && distance <= machinegunDistance)
                {
                    float halfSpread = machinegunAngle / 2f;
                    float angleIncrement = machinegunAngle / (machinegunCounts - 1);

                    for (int i = 0; i < machinegunCounts; i++)
                    {
                        float angle = -halfSpread + i * angleIncrement;
                        Quaternion machinegunRotation = Quaternion.Euler(0, angle, 0);
                        Rigidbody currentMachinegun = Instantiate(machinegun, machinegunShootPoint.position, machinegunShootPoint.rotation);
                        currentMachinegun.velocity = machinegunRotation * direction * machinegunSpeed;

                        ShipBullet bullet = currentMachinegun.GetComponent<ShipBullet>();
                        bullet.SetDamage(machinegunDamage);
                    }

                    currentReload = Time.time + machinegunReload;
                }
            }

            // Стрельба из автомата
            if (currentWeapon == gunWeapon)
            {
                if (shipInput.GetShootButton() && Time.time >= currentReload && distance <= gunDistance)
                {
                    Rigidbody currentGun = Instantiate(gun, gunShootPoint.position, gunShootPoint.rotation);
                    currentGun.velocity = direction * gunSpeed;
                    currentGun.transform.LookAt(currentGun.transform.position + currentGun.velocity);

                    ShipBullet bullet = currentGun.GetComponent<ShipBullet>();
                    bullet.SetDamage(gunDamage);

                    currentReload = Time.time + gunReload;
                }
            }
        }
    }

    // Метод смены оружия по нажатию цифр от 1 до 4
    private void SwitchWeapon()
    {
        if (shipInput.GetWeapon())
        {
            weaponCount ++;

            if (weaponCount == 1)
            {
                currentWeapon = cannonWeapon;
                weaponDistance = cannonDistance;
                currentDamage = cannonDamage;
                currentReload = cannonReload;
                isCanShoot = true;
            }
            if (weaponCount == 2)
            {
                currentWeapon = crossbowWeapon;
                weaponDistance = crossbowDistance;
                currentDamage = crossbowDamage;
                currentReload = crossbowReload;
                isCanShoot = true;
            }
            if (weaponCount == 3)
            {
                currentWeapon = machinegunWeapon;
                weaponDistance = machinegunDistance;
                currentDamage = machinegunDamage;
                currentReload = machinegunReload;
                isCanShoot = true;
            }
            if (weaponCount == 4)
            {
                currentWeapon = gunWeapon;
                weaponDistance = gunDistance;
                currentDamage = gunDamage;
                currentReload = gunReload;
                isCanShoot = true;
            }
            if (weaponCount > maxWeaponCount)
            {
                weaponCount = 0;
                isCanShoot = false;
            }
        }
    }

    // Метод балистического полета ядра пушки
    private void CannonShootTarget()
    {
        Vector3 directionToTarget = aimPosition - shootPoint.position;
        float heightDifference = directionToTarget.y;
        directionToTarget.y = 0;
        float horizontalDistance = directionToTarget.magnitude;

        float angleRad = cannonShootAngle * Mathf.Deg2Rad;

        gravityAcceleration = Physics.gravity.y;
        float verticalSpeed = cannonSpeed * Mathf.Sin(angleRad);
        float requiredLaunchSpeed = Mathf.Sqrt(horizontalDistance * gravityAcceleration /
                                        (horizontalDistance * Mathf.Tan(angleRad) - heightDifference));

        if (cannonSpeed < requiredLaunchSpeed)
        {
            cannonSpeed = requiredLaunchSpeed;
        }

        launchDirection = directionToTarget / cannonAngleMultiplier;
        launchDirection.y = verticalSpeed;
    }

    // Метод отрисовки траектории ядра пушки
    private void CannonShowTrajectory()
    {
        for (int i = 0; i <= resolution; i++)
        {
            float t = i / (float)resolution;
            float x = launchDirection.x * cannonSpeed * t;
            float y = launchDirection.y * t - (correctionFactor * gravityAcceleration * t * t);
            float z = launchDirection.z * cannonSpeed * t;

            if (i < lineRenderer.positionCount)
            {
                lineRenderer.SetPosition(i, shootPoint.position + new Vector3(x, y, z));
            }
        }
    }

    // Метод включения возможности стрельбы
    private void CanShoot()
    {
        isCanShoot = true;
    }

    // Метод получения переменной возможности стрельбы
    public bool GetCanShoot()
    {
        return isCanShoot;
    }

    // Публичные методы получения дистанций
    public float GetDistance()
    {
        return distance;
    }
    public float GetWeaponDistance()
    {
        return weaponDistance;
    }

    // Публичный метод отключения стрельбы на время
    public void ShootOff(float time)
    {
        isCanShoot = false;
        Invoke("CanShoot", time);
    }

    // Публичный метод получения урона текущим оружием
    public float GetDamage()
    {
        return currentDamage;
    }

    // Публичный метод получения времени перезарядки
    public float GetReload()
    {
        return currentReload;
    }

    // Публичный метод для чертежей прокачки урона оружия
    public void ImproveProcentDamage(float value)
    {
        cannonDamage += cannonDamage / 100 * value;
        crossbowDamage += crossbowDamage / 100 * value;
        machinegunDamage += machinegunDamage / 100 * value;
        gunDamage += gunDamage / 100 * value;
    }

    // Публичный метод для чертежей прокачки времени перезарядки
    public void ImproveProcentReload(float value)
    {
        cannonReload += cannonReload / 100 * value;
        crossbowReload += crossbowReload / 100 * value;
        machinegunReload += machinegunReload / 100 * value;
        gunReload += gunReload / 100 * value;
    }
}
