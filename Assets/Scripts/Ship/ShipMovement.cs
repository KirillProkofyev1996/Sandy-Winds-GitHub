using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    [Header("Movement vars on ground")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float procentMoveSpeed; // Сила воздействия на скорость в процентах
    [SerializeField] private float boostSpeed; // Сила воздействия на скорость в процентах
    [SerializeField] private float startMovePower, breakMovePower; // Сила воздействия на скорость в процентах

    [Header("Current vars on ground")]
    [SerializeField] private float currentMoveSpeed;
    [SerializeField] private float currentBoostSpeed;
    [SerializeField] private float currentStartMovePower;
    [SerializeField] private float currentBreakMovePower;

    [Header("Movement vars in air")]
    [SerializeField] private float moveSpeedInAir;
    [SerializeField] private float rotationSpeedInAir;
    [SerializeField] private float delayPowerInAir; // Сила воздействия на повороты в воздухе в процентах

    [Header("Current vars in air")]
    [SerializeField] private float currentMoveSpeedInAir;
    [SerializeField] private float currentDelayPowerInAir;
    [SerializeField] private float currentRotationSpeedInAir;


    [Header("Jump settings")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float gravityMultiplier;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float checkerOffset;
    [SerializeField] private float speedLimit;
    private bool isGrounded;
    

    [Header("Components")]
    [SerializeField] Transform tr;
    [SerializeField] Rigidbody rb;
    private ShipInput shipInput;

    // Дополнительные переменные для просчета скорости и поворота корабля
    private float newCurrentMoveSpeed;
    private float newCurrentRotationSpeed;
    private float newCurrentRotationSpeedInAir;
    
    private void Start()
    {
        shipInput = GetComponent<ShipInput>();
    }

    private void FixedUpdate()
    {
        Jump();
    }

    private void Update()
    {   
        // Проверка на нахождение корабля на земле
        isGrounded = Physics.CheckSphere(groundChecker.position, checkerOffset, groundMask);
        
        // Просчет скорости, ускорения, иннерции остановки
        // и начала движения с учетом процентов
        currentMoveSpeed = moveSpeed + (moveSpeed/100 * procentMoveSpeed);
        currentBoostSpeed = currentMoveSpeed + (currentMoveSpeed/100 * boostSpeed);
        currentStartMovePower = moveSpeed/100 * startMovePower;
        currentBreakMovePower = moveSpeed/100 * breakMovePower;

        // Просчет движения и поворота в воздухе
        currentMoveSpeedInAir = moveSpeedInAir;
        currentDelayPowerInAir = moveSpeedInAir/100 * delayPowerInAir;
        currentRotationSpeedInAir = rotationSpeedInAir;
        
        Movement(currentMoveSpeed, currentBoostSpeed);
        MovementInAir(currentMoveSpeedInAir, currentRotationSpeedInAir);
        Rotation();
        Gravity();
    }

    // Метод движения корабля на земле и с ускорением
    private void Movement(float speed, float boost)
    {
        if (isGrounded)
        {
            if (shipInput.GetVerticalDirection() > 0)
            {
                if (shipInput.GetBoostButton())
                {
                    newCurrentMoveSpeed = Mathf.Lerp(newCurrentMoveSpeed, speed + boost, Time.deltaTime / currentStartMovePower);
                }
                else
                {
                    newCurrentMoveSpeed = Mathf.Lerp(newCurrentMoveSpeed, speed, Time.deltaTime / currentStartMovePower);
                }
            }
            if (shipInput.GetVerticalDirection() < 0)
            {
                newCurrentMoveSpeed = Mathf.Lerp(newCurrentMoveSpeed, -speed, Time.deltaTime / currentStartMovePower);
            }
            if (shipInput.GetVerticalDirection() == 0)
            {
                newCurrentMoveSpeed = Mathf.Lerp(newCurrentMoveSpeed, 0, Time.deltaTime / currentBreakMovePower);
            }

            tr.position += tr.forward * newCurrentMoveSpeed * Time.deltaTime;
        }
    }
    
    // Метод поворота корабля по оси У на земле
    private void Rotation()
    {
        if (isGrounded)
        {
            if (shipInput.GetHorizontalDirection() > 0)
            {
                newCurrentRotationSpeed = Mathf.Lerp(newCurrentRotationSpeed, rotationSpeed, Time.deltaTime / currentStartMovePower);
            }
            if (shipInput.GetHorizontalDirection() < 0)
            {
                newCurrentRotationSpeed = Mathf.Lerp(newCurrentRotationSpeed, -rotationSpeed, Time.deltaTime / currentStartMovePower);
            }
            if (shipInput.GetHorizontalDirection() == 0)
            {
                newCurrentRotationSpeed = Mathf.Lerp(newCurrentRotationSpeed, 0, Time.deltaTime / currentBreakMovePower);
            }

            tr.Rotate(new Vector3(0, newCurrentRotationSpeed, 0));
        }
    }

    // Метод прыжка
    private void Jump()
    {
        if (shipInput.GetJumpButton() && isGrounded)
        {
            rb.velocity = new Vector3(0, jumpForce, 0);
        }
    }

    // Метод уменьшения гравитации, когда корабль в воздухе
    private void Gravity()
    {
        if (!isGrounded)
        {
            tr.position -= tr.up * gravityMultiplier * Time.deltaTime;
        }
    }
    
    // Метод движения и поворот корабля в воздухе
    private void MovementInAir(float moveSpeed, float rotationSpeed)
    {
        // Отслеживание скорости движения корабля перед прыжком
        if (newCurrentMoveSpeed < speedLimit && newCurrentMoveSpeed > -speedLimit)
        {
            if (!isGrounded)
            {
                tr.position += Vector3.zero;
            }
        }
        if (newCurrentMoveSpeed >= speedLimit)
        {
            if (!isGrounded)
            {
                tr.position += tr.forward * moveSpeed * Time.deltaTime;
            }
        }
        if (newCurrentMoveSpeed <= -speedLimit)
        {
            if (!isGrounded)
            {
                tr.position -= tr.forward * moveSpeed * Time.deltaTime;
            }
        }

        // Поворот корабля в воздухе
        if (!isGrounded)
        {
            if (shipInput.GetHorizontalDirection() > 0)
            {
                newCurrentRotationSpeedInAir = Mathf.Lerp(newCurrentRotationSpeedInAir, rotationSpeed, Time.deltaTime / currentDelayPowerInAir);
            }
            if (shipInput.GetHorizontalDirection() < 0)
            {
                newCurrentRotationSpeedInAir = Mathf.Lerp(newCurrentRotationSpeedInAir, -rotationSpeed, Time.deltaTime / currentDelayPowerInAir);
            }
            if (shipInput.GetHorizontalDirection() == 0)
            {
                newCurrentRotationSpeedInAir = Mathf.Lerp(newCurrentRotationSpeedInAir, 0, Time.deltaTime / currentDelayPowerInAir);
            }

            tr.Rotate(new Vector3(0, newCurrentRotationSpeedInAir, 0));
        }
    }
}
