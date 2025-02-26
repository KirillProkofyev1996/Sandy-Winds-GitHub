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
    [SerializeField] private float stabilizationPower;
    [SerializeField] private float currentMoveSpeedInAir;
    [SerializeField] private float currentDelayPowerInAir;


    [Header("Jump settings")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float improvedJumpForce;
    [SerializeField] private float gravityMultiplier;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float checkerOffset;
    [SerializeField] private float speedJumpLimit;
    private bool isGrounded;
    

    [Header("Components")]
    private Transform tr;
    private Rigidbody rb;
    private ShipInput shipInput;

    // Дополнительные переменные для просчета скорости и поворота корабля
    private float newCurrentMoveSpeed;
    private float newCurrentRotationSpeed;
    
    private void Start()
    {
        shipInput = GetComponent<ShipInput>();
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
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
        
        Movement(currentMoveSpeed, currentBoostSpeed);
        MovementInAir(currentMoveSpeedInAir);
        Rotation();
        Gravity();
        Stabilization();
    }

    // Метод движения корабля на земле и с ускорением
    private void Movement(float speed, float boost)
    {
        if (isGrounded)
        {
            if (shipInput.GetVerticalDirection().y > 0)
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
            if (shipInput.GetVerticalDirection().y < 0)
            {
                newCurrentMoveSpeed = Mathf.Lerp(newCurrentMoveSpeed, -speed, Time.deltaTime / currentStartMovePower);
            }
            if (shipInput.GetVerticalDirection().y == 0)
            {
                newCurrentMoveSpeed = Mathf.Lerp(newCurrentMoveSpeed, 0, Time.deltaTime / currentBreakMovePower);
            }

            tr.position += tr.forward * newCurrentMoveSpeed * Time.deltaTime;
        }
    }
    
    // Метод поворота корабля по оси У на земле и в воздухе
    private void Rotation()
    {
        if (isGrounded)
        {
            if (shipInput.GetHorizontalDirection().x > 0)
            {
                newCurrentRotationSpeed = Mathf.Lerp(newCurrentRotationSpeed, rotationSpeed, Time.deltaTime / currentStartMovePower);
            }
            if (shipInput.GetHorizontalDirection().x < 0)
            {
                newCurrentRotationSpeed = Mathf.Lerp(newCurrentRotationSpeed, -rotationSpeed, Time.deltaTime / currentStartMovePower);
            }
            if (shipInput.GetHorizontalDirection().x == 0)
            {
                newCurrentRotationSpeed = Mathf.Lerp(newCurrentRotationSpeed, 0, Time.deltaTime / currentBreakMovePower);
            }
        }
        if (!isGrounded)
        {
            if (shipInput.GetHorizontalDirection().x > 0)
            {
                newCurrentRotationSpeed = Mathf.Lerp(newCurrentRotationSpeed, rotationSpeedInAir, Time.deltaTime / currentDelayPowerInAir);
            }
            if (shipInput.GetHorizontalDirection().x < 0)
            {
                newCurrentRotationSpeed = Mathf.Lerp(newCurrentRotationSpeed, -rotationSpeedInAir, Time.deltaTime / currentDelayPowerInAir);
            }
            if (shipInput.GetHorizontalDirection().x == 0)
            {
                newCurrentRotationSpeed = Mathf.Lerp(newCurrentRotationSpeed, 0, Time.deltaTime / currentDelayPowerInAir);
            }
        }

        tr.Rotate(new Vector3(0, newCurrentRotationSpeed, 0));
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
    private void MovementInAir(float moveSpeed)
    {
        // Отслеживание скорости движения корабля перед прыжком
        // и делать прыжок назад, вперед или на месте
        if (newCurrentMoveSpeed < speedJumpLimit && newCurrentMoveSpeed > -speedJumpLimit)
        {
            if (!isGrounded)
            {
                tr.position += Vector3.zero;
            }
        }
        if (newCurrentMoveSpeed >= speedJumpLimit)
        {
            if (!isGrounded)
            {
                tr.position += tr.forward * moveSpeed * Time.deltaTime;
            }
        }
        if (newCurrentMoveSpeed <= -speedJumpLimit)
        {
            if (!isGrounded)
            {
                tr.position -= tr.forward * moveSpeed * Time.deltaTime;
            }
        }
    }

    // Метод выравнивания корабля по осям х и z,
    // если корабль в воздухе
    private void Stabilization()
    {
        if (!isGrounded)
        {
            Quaternion targetRotation = Quaternion.Euler(0, tr.eulerAngles.y, 0);
            tr.rotation = Quaternion.Slerp(tr.rotation, targetRotation, Time.deltaTime * stabilizationPower);
        }
    }

    public float GetSpeed()
    {
        return moveSpeed;
    }

    public float GetTurbo()
    {
        return boostSpeed;
    }

    // Публичный метод увеличения скорости в процентах для чертежей 
    public void ImproveProcentSpeed(float value)
    {
        moveSpeed += moveSpeed / 100 * value;
    }

    // Публичный метод увеличения силы прыжка для чертежа (планирование)
    public void ImproveJumpForce()
    {
        jumpForce = improvedJumpForce;
    }
}