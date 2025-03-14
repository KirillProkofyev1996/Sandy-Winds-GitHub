using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ShipShooter : MonoBehaviour
{
    [Header("Cannon settings")]
    [SerializeField] private Rigidbody cannon;
    [SerializeField] private Rigidbody bigCannon;
    [SerializeField] private Transform cannonShootPoint;
    [SerializeField] private float cannonSpeed;
    [SerializeField] private float cannonReload;
    [SerializeField] private float cannonDistance;
    private string cannonWeapon = "Пушка";
    private bool isCannonAvailable = true;
    private bool isBigCannonAvailable; // Для открытия улучшения BigCannon


    [Header("Crossbow settings")]
    [SerializeField] private Rigidbody crossbow;
    [SerializeField] private Rigidbody slowCrossbow;
    [SerializeField] private Transform crossbowShootPoint;
    [SerializeField] private float crossbowSpeed;
    [SerializeField] private float crossbowReload;
    [SerializeField] private float crossbowDistance;
    [SerializeField] private float crossbowShootAngle; // Если угол = 50
    [SerializeField] private float crossbowAngleMultiplier; // То множитель = 1.5
    private string crossbowWeapon = "Арбалет";
    private bool isCrossbowAvailable;
    private bool isSlowCrossbowAvailable; // Для открытия улучшения SlowCrossbow


    [Header("Machinegun settings")]
    [SerializeField] private Rigidbody machinegun;
    [SerializeField] private Rigidbody leadMachinegun;
    [SerializeField] private Transform machinegunShootPoint;
    [SerializeField] private float machinegunSpeed;
    [SerializeField] private float machinegunReload;
    [SerializeField] private float machinegunDistance;
    [SerializeField] private int machinegunCounts;
    [SerializeField] private float machinegunAngle;
    private string machinegunWeapon = "Пулемет";
    private bool isMachinegunAvailable;
    private bool isLeadMachinegunAvailable; // Для открытия улучшения LeadMachinegun


    [Header("Gun settings")]
    [SerializeField] private Rigidbody gun;
    [SerializeField] private Rigidbody sidesGun;
    [SerializeField] private Transform gunShootPoint;
    [SerializeField] private Transform leftGunShootPoint; // Для SidesGun
    [SerializeField] private Transform rightGunShootPoint; // Для SidesGun
    [SerializeField] private float gunSpeed;
    [SerializeField] private float gunReload;
    [SerializeField] private float gunDistance;
    private string gunWeapon = "Автомат";
    private bool isGunAvailable;
    private bool isSidesGunAvailable; // Для открытия улучшения SidesMachinegun


    [Header("Crossbow draw trajectory")]
    [SerializeField] private int resolution;
    [SerializeField] private float correctionFactor; // При угле = 50, множителе 1.5, фактор = -0.28
    private Vector3 launchDirection;
    private float gravityAcceleration;


    [Header("Settings")]
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float aimOffsetY;
    [SerializeField] private int maxWeaponCount; // Для открытия остальный оружий (изначально 1)
    [SerializeField] private bool isImprovedAllDamage; // Улучшает урон всех оружий
    private bool isCanShoot;
    private float weaponDistance;
    private string currentWeapon;
    private float currentReload;
    private float currentDamage;
    private float distance;
    private Vector3 direction;
    private Vector3 gunDirection;
    private Vector3 aimPosition;
    private int weaponCount;

    
    [Header("Components")]
    [SerializeField] private Aim aim;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private CameraController cameraController;
    private ShipInput shipInput;

    private void Start()
    {
        shipInput = GetComponent<ShipInput>();
        currentWeapon = "None";
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

        gunDirection = aimPosition - gunShootPoint.position;
        gunDirection.Normalize();
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
                if (isBigCannonAvailable == false)
                {
                    if (shipInput.GetShootButton() && Time.time >= currentReload && distance <= cannonDistance)
                    {
                        Rigidbody currentCannon = Instantiate(cannon, cannonShootPoint.position, cannonShootPoint.rotation);
                        currentCannon.velocity = direction * cannonSpeed;
                        currentCannon.transform.LookAt(currentCannon.transform.position + currentCannon.velocity);

                        ShipBullet bullet = currentCannon.GetComponent<ShipBullet>();

                        if (isImprovedAllDamage)
                        {
                            bullet.ImproveProcentDamage();
                        }

                        currentReload = Time.time + cannonReload;
                    }
                }
                if (isBigCannonAvailable == true)
                {
                    if (shipInput.GetShootButton() && Time.time >= currentReload && distance <= cannonDistance)
                    {
                        Rigidbody currentCannon = Instantiate(bigCannon, cannonShootPoint.position, cannonShootPoint.rotation);
                        currentCannon.velocity = direction * cannonSpeed;
                        currentCannon.transform.LookAt(currentCannon.transform.position + currentCannon.velocity);

                        ShipBullet bullet = currentCannon.GetComponent<ShipBullet>();

                        if (isImprovedAllDamage)
                        {
                            bullet.ImproveProcentDamage();
                        }

                        currentReload = Time.time + cannonReload;
                    }
                }
            }

            // Стрельба из арбалета
            if (currentWeapon == crossbowWeapon)
            {
                CannonShootTarget();
                CannonShowTrajectory();

                if (shipInput.GetShootButton() && Time.time >= currentReload && distance <= crossbowDistance)
                {
                    if (isSlowCrossbowAvailable == false)
                    {
                        Rigidbody currentCrossbow = Instantiate(crossbow, crossbowShootPoint.position, crossbowShootPoint.rotation);
                        currentCrossbow.velocity = launchDirection * crossbowSpeed;
                        currentCrossbow.transform.LookAt(currentCrossbow.transform.position + currentCrossbow.velocity);

                        ShipBullet bullet = currentCrossbow.GetComponent<ShipBullet>();
                        if (isImprovedAllDamage)
                        {
                            bullet.ImproveProcentDamage();
                        }

                        currentReload = Time.time + crossbowReload;
                    }
                    if (isSlowCrossbowAvailable == true)
                    {
                        Rigidbody currentCrossbow = Instantiate(slowCrossbow, crossbowShootPoint.position, crossbowShootPoint.rotation);
                        currentCrossbow.velocity = launchDirection * crossbowSpeed;
                        currentCrossbow.transform.LookAt(currentCrossbow.transform.position + currentCrossbow.velocity);

                        ShipBullet bullet = currentCrossbow.GetComponent<ShipBullet>();
                        if (isImprovedAllDamage)
                        {
                            bullet.ImproveProcentDamage();
                        }

                        currentReload = Time.time + crossbowReload;
                    }
                }
            }
            else
            {
                lineRenderer.enabled = false;
            }

            // Стрельба из пулемета
            if (currentWeapon == machinegunWeapon)
            {
                if (isLeadMachinegunAvailable == false)
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
                            if (isImprovedAllDamage)
                            {
                                bullet.ImproveProcentDamage();
                            }
                        }

                        currentReload = Time.time + machinegunReload;
                    }
                }
                if (isLeadMachinegunAvailable == true)
                {
                    if (shipInput.GetShootButton() && Time.time >= currentReload && distance <= machinegunDistance)
                    {
                        float halfSpread = machinegunAngle / 2f;
                        float angleIncrement = machinegunAngle / (machinegunCounts - 1);

                        for (int i = 0; i < machinegunCounts; i++)
                        {
                            float angle = -halfSpread + i * angleIncrement;
                            Quaternion machinegunRotation = Quaternion.Euler(0, angle, 0);
                            Rigidbody currentMachinegun = Instantiate(leadMachinegun, machinegunShootPoint.position, machinegunShootPoint.rotation);
                            currentMachinegun.velocity = machinegunRotation * direction * machinegunSpeed;

                            ShipBullet bullet = currentMachinegun.GetComponent<ShipBullet>();
                            if (isImprovedAllDamage)
                            {
                                bullet.ImproveProcentDamage();
                            }
                        }

                        currentReload = Time.time + machinegunReload;
                    }
                }
            }

            // Стрельба из автомата
            if (currentWeapon == gunWeapon)
            {
                if (isSidesGunAvailable == false)
                {
                    if (shipInput.GetShootButton() && Time.time >= currentReload && distance <= gunDistance)
                    {
                        Rigidbody currentGun = Instantiate(gun, gunShootPoint.position, gunShootPoint.rotation);
                        currentGun.velocity = gunDirection * gunSpeed;
                        currentGun.transform.LookAt(currentGun.transform.position + currentGun.velocity);

                        ShipBullet bullet = currentGun.GetComponent<ShipBullet>();
                        if (isImprovedAllDamage)
                        {
                            bullet.ImproveProcentDamage();
                        }

                        currentReload = Time.time + gunReload;
                    }
                }
                if (isSidesGunAvailable == true)
                {
                    if (shipInput.GetShootButton() && Time.time >= currentReload && distance <= gunDistance)
                    {
                        Rigidbody currentLeftSideGun = Instantiate(sidesGun, leftGunShootPoint.position, gunShootPoint.rotation);
                        currentLeftSideGun.velocity = transform.forward * gunSpeed;
                        currentLeftSideGun.transform.LookAt(currentLeftSideGun.transform.position + currentLeftSideGun.velocity);

                        ShipBullet bulletLeft = currentLeftSideGun.GetComponent<ShipBullet>();
                        if (isImprovedAllDamage)
                        {
                            bulletLeft.ImproveProcentDamage();
                        }

                        Rigidbody currentRightSideGun = Instantiate(sidesGun, rightGunShootPoint.position, gunShootPoint.rotation);
                        currentRightSideGun.velocity = transform.forward * gunSpeed;
                        currentRightSideGun.transform.LookAt(currentRightSideGun.transform.position + currentRightSideGun.velocity);

                        ShipBullet bulletRight = currentRightSideGun.GetComponent<ShipBullet>();
                        if (isImprovedAllDamage)
                        {
                            bulletRight.ImproveProcentDamage();
                        }

                        currentReload = Time.time + gunReload;
                    }
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

            if (weaponCount == 1 && isCannonAvailable)
            {
                currentWeapon = cannonWeapon;
                weaponDistance = cannonDistance;
                currentReload = cannonReload;
                isCanShoot = true;
            }
            if (weaponCount == 2 && isCrossbowAvailable)
            {
                currentWeapon = crossbowWeapon;
                weaponDistance = crossbowDistance;
                currentReload = crossbowReload;
                isCanShoot = true;
            }
            if (weaponCount == 3 && isMachinegunAvailable)
            {
                currentWeapon = machinegunWeapon;
                weaponDistance = machinegunDistance;
                currentReload = machinegunReload;
                isCanShoot = true;
            }
            if (weaponCount == 4 && isGunAvailable)
            {
                currentWeapon = gunWeapon;
                weaponDistance = gunDistance;
                currentReload = gunReload;
                isCanShoot = true;
            }
            if (weaponCount > maxWeaponCount)
            {
                weaponCount = 0;
                currentWeapon = "без оружия";
                currentDamage = 0;
                currentReload = 0;
                isCanShoot = true;
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

        float angleRad = crossbowShootAngle * Mathf.Deg2Rad;

        gravityAcceleration = Physics.gravity.y;
        float verticalSpeed = crossbowSpeed * Mathf.Sin(angleRad);
        float requiredLaunchSpeed = Mathf.Sqrt(horizontalDistance * gravityAcceleration /
                                        (horizontalDistance * Mathf.Tan(angleRad) - heightDifference));

        if (crossbowSpeed < requiredLaunchSpeed)
        {
            crossbowSpeed = requiredLaunchSpeed;
        }

        launchDirection = directionToTarget / crossbowAngleMultiplier;
        launchDirection.y = verticalSpeed;
    }

    // Метод отрисовки траектории ядра пушки
    private void CannonShowTrajectory()
    {
        lineRenderer.enabled = true;

        if (cameraController.GetIsAimButtonPressed())
        {
            for (int i = 0; i <= resolution; i++)
            {
                float t = i / (float)resolution;
                float x = launchDirection.x * crossbowSpeed * t;
                float y = launchDirection.y * t - (correctionFactor * gravityAcceleration * t * t);
                float z = launchDirection.z * crossbowSpeed * t;

                if (i < lineRenderer.positionCount)
                {
                    lineRenderer.SetPosition(i, shootPoint.position + new Vector3(x, y, z));
                }
            }
        }
        else
        {
            lineRenderer.enabled = false;
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

    // Публичный метод получения выбранного оружия
    public string GetWeapon()
    {
        return currentWeapon;
    }

    // Публичный метод для чертежей прокачки времени перезарядки
    public void ImproveProcentReload(float value)
    {
        cannonReload += cannonReload / 100 * value;
        crossbowReload += crossbowReload / 100 * value;
        machinegunReload += machinegunReload / 100 * value;
        gunReload += gunReload / 100 * value;
    }

    // Публичный метод для чертежа (желтый 7)
    public void ImproveAllDamage()
    {
        isImprovedAllDamage = true;
    }

    // Методы для открытия стандартных оружий
    public void SetCannonAvailable()
    {
        isCannonAvailable = true;
    }
    public void SetCrossbowAvailable()
    {
        isCrossbowAvailable = true;
    }
    public void SetMachinegunAvailable()
    {
        isMachinegunAvailable = true;
    }
    public void SetGunAvailable()
    {
        isGunAvailable = true;
    }
    public void SetBigCannonAvailable()
    {
        isBigCannonAvailable = true;
    }
    public void SetSlowCrossbowAvailable()
    {
        isSlowCrossbowAvailable = true;
    }
    public void SetLeadMachinegunAvailable()
    {
        isLeadMachinegunAvailable = true;
    }
    public void SetSidesGunAvailable()
    {
        isSidesGunAvailable = true;
    }
}
