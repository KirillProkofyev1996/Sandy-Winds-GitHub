using System;
using Unity.VisualScripting;
using UnityEngine;

public class ShipShooter : MonoBehaviour
{
    [Header("Cannon settings")]
    [SerializeField] private Rigidbody cannon;
    [SerializeField] private Transform cannonShootPoint;
    [SerializeField] private float cannonSpeed;
    [SerializeField] private float cannonRate;
    [SerializeField] private float cannonDistance;
    [SerializeField] private float cannonShootAngle; // если угол = 50
    [SerializeField] private float cannonAngleMultiplier; // то multiplier = 1.5
    private string cannonWeapon = "Cannon";


    [Header("Crossbow settings")]
    [SerializeField] private Rigidbody crossbow;
    [SerializeField] private Transform crossbowFrontShootPoint;
    [SerializeField] private Transform crossbowBackShootPoint;
    [SerializeField] private float crossbowSpeed;
    [SerializeField] private float crossbowRate;
    [SerializeField] private float crossbowDistance;
    private string crossbowWeapon = "Crossbow";


    [Header("Machinegun settings")]
    [SerializeField] private Rigidbody machinegun;
    [SerializeField] private Transform machinegunShootPoint;
    [SerializeField] private float machinegunSpeed;
    [SerializeField] private float machinegunRate;
    [SerializeField] private float machinegunDistance;
    [SerializeField] private int machinegunCounts;
    [SerializeField] private float machinegunAngle;
    private string machinegunWeapon = "Machinegun";


    [Header("Gun settings")]
    [SerializeField] private Rigidbody gun;
    [SerializeField] private Transform gunShootPoint;
    [SerializeField] private float gunSpeed;
    [SerializeField] private float gunRate;
    [SerializeField] private float gunDistance;
    private string gunWeapon = "Gun";


    [Header("Settings")]
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float aimOffsetY;
    private float weaponDistance;
    private string currentWeapon;
    private float currentRate;
    private float distance;
    private Vector3 direction;
    private Vector3 aimPosition;

    
    [Header("Components")]
    [SerializeField] private Aim aim;
    private ShipInput shipInput;

    private void Start()
    {
        shipInput = GetComponent<ShipInput>();
    }

    private void FixedUpdate()
    {
        Shoot();
    }

    private void Update()
    {
        AimPosition();
        Direction();
        Distance();
        SwitchWeapon();
    }

    private void AimPosition()
    {
        aimPosition = new Vector3(aim.GetAimPosition().x, aim.GetAimPosition().y + aimOffsetY, aim.GetAimPosition().z);
    }

    private void Direction()
    {
        direction = aimPosition - shootPoint.position;
        direction = direction.normalized;
    }

    private void Distance()
    {
        distance = Vector3.Distance(aimPosition, shootPoint.position);
    }

    // Метод стрельбы из разных видов оружия
    private void Shoot()
    {
        if (currentWeapon == cannonWeapon)
        {
            if (shipInput.GetShootButton() && Time.time >= currentRate && distance <= cannonDistance)
            {
                // Создаем снаряд
                Rigidbody currentCannon = Instantiate(cannon, cannonShootPoint.position, Quaternion.identity);

                // Рассчитываем направление и высоту
                Vector3 directionToTarget = aimPosition - shootPoint.position;
                float heightDifference = directionToTarget.y; // Разница в высоте
                directionToTarget.y = 0; // Игнорируем высоту для горизонтального расчета
                float horizontalDistance = directionToTarget.magnitude; // Горизонтальное расстояние до цели

                // Рассчитываем угол в радианах
                float angleRad = cannonShootAngle * Mathf.Deg2Rad;

                // Рассчитываем необходимую скорость
                float gravitationalAcceleration = Physics.gravity.y; // Гравитация
                float verticalSpeed = cannonSpeed * Mathf.Sin(angleRad);
                // Проверяем, может ли снаряд достичь цели с заданной launchSpeed
                float requiredLaunchSpeed = Mathf.Sqrt(horizontalDistance * gravitationalAcceleration / 
                                                (horizontalDistance * Mathf.Tan(angleRad) - heightDifference));

                // Если launchSpeed меньше необходимой скорости, то используем необходимую
                if (cannonSpeed < requiredLaunchSpeed)
                {
                    cannonSpeed = requiredLaunchSpeed;
                }

                // Корректируем вектор направления
                Vector3 launchDirection = directionToTarget / cannonAngleMultiplier; // Нормализуем для получения единичного вектора
                launchDirection.y = verticalSpeed; // Устанавливаем вертикальную скорость

                // Устанавливаем скорость снаряда
                currentCannon.velocity = launchDirection * cannonSpeed;

                // Убедитесь, что снаряд выглядит в направлении движения
                currentCannon.transform.LookAt(currentCannon.transform.position + currentCannon.velocity);

                currentRate = Time.time + cannonRate;

                /*Rigidbody currentCannon = Instantiate(cannon, cannonShootPoint.position, cannonShootPoint.rotation);
                currentCannon.velocity = direction * cannonSpeed;
                currentCannon.transform.LookAt(currentCannon.transform.position + currentCannon.velocity);
                currentRate = Time.time + cannonRate;*/
            }
        }
        if (currentWeapon == crossbowWeapon)
        {
            if (shipInput.GetShootButton() && Time.time >= currentRate && distance <= crossbowDistance)
            {
                Rigidbody currentFrontCrossbow = Instantiate(crossbow, crossbowFrontShootPoint.position, Quaternion.identity);
                currentFrontCrossbow.velocity = direction * crossbowSpeed;
                currentFrontCrossbow.transform.LookAt(currentFrontCrossbow.transform.position + currentFrontCrossbow.velocity);

                Rigidbody currentBackCrossbow = Instantiate(crossbow, crossbowBackShootPoint.position, Quaternion.identity);
                currentBackCrossbow.velocity = direction * crossbowSpeed;
                currentBackCrossbow.transform.LookAt(currentBackCrossbow.transform.position + currentBackCrossbow.velocity);

                currentRate = Time.time + crossbowRate;
            }
        }
        if (currentWeapon == machinegunWeapon)
        {
            if (shipInput.GetShootButton() && Time.time >= currentRate && distance <= machinegunDistance)
            {
                float halfSpread = machinegunAngle / 2f;
                float angleIncrement = machinegunAngle / (machinegunCounts - 1);

                for (int i = 0; i < machinegunCounts; i++)
                {
                    float angle = -halfSpread + i * angleIncrement;
                    Quaternion machinegunRotation = Quaternion.Euler(0, angle, 0);
                    Rigidbody currentMachinegun = Instantiate(machinegun, machinegunShootPoint.position, machinegunShootPoint.rotation);
                    currentMachinegun.velocity = machinegunRotation * direction * machinegunSpeed;
                }

                currentRate = Time.time + machinegunRate;
            }
        }
        if (currentWeapon == gunWeapon)
        {
            if (shipInput.GetShootButton() && Time.time >= currentRate && distance <= gunDistance)
            {
                Rigidbody currentGun = Instantiate(gun, gunShootPoint.position, Quaternion.identity);
                currentGun.velocity = direction * gunSpeed;
                currentGun.transform.LookAt(currentGun.transform.position + currentGun.velocity);
                currentRate = Time.time + gunRate;
            }
        }
    }

    // Метод смены оружия по нажатию цифр от 1 до 4
    private void SwitchWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = cannonWeapon;
            weaponDistance = cannonDistance;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = crossbowWeapon;
            weaponDistance = crossbowDistance;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentWeapon = machinegunWeapon;
            weaponDistance = machinegunDistance;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentWeapon = gunWeapon;
            weaponDistance = gunDistance;
        }
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
}
