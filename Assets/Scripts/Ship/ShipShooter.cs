using UnityEngine;

public class ShipShooter : MonoBehaviour
{
    [Header("Cannon settings")]
    [SerializeField] private Rigidbody cannon;
    [SerializeField] private Transform cannonShootPoint;
    [SerializeField] private float cannonSpeed;
    [SerializeField] private float cannonRate;
    [SerializeField] private float cannonDistance;
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
    private float weaponDistance;
    private string currentWeapon;
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
    }

    // Метод стрельбы из разных видов оружия
    private void Shoot()
    {
        distance = Vector3.Distance(aim.GetAimPosition(), shootPoint.position);
        direction = (aim.GetAimPosition() - shootPoint.position).normalized;

        if (currentWeapon == cannonWeapon)
        {
            if (shipInput.GetShootButton() && Time.time >= currentRate && distance <= cannonDistance)
            {
                Rigidbody currentCannon = Instantiate(cannon, cannonShootPoint.position, Quaternion.identity);
                currentCannon.velocity = direction * cannonSpeed;
                currentCannon.transform.LookAt(currentCannon.transform.position + currentCannon.velocity);
                currentRate = Time.time + cannonRate;
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
