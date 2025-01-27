using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    [Header("Movement vars")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private float procentMoveSpeed; // Сила воздействия на скорость в процентах
    [SerializeField] private float boostSpeed; // Сила воздействия на скорость в процентах
    [SerializeField] private float startMovePower, breakMovePower; // Сила воздействия на скорость в процентах

    [SerializeField] private float moveSpeedInAir;
    [SerializeField] private float delayMoveSpeedInAir; // Сила воздействия на скорость в воздухе в процентах

    [Header("Current vars")]
    [SerializeField] private float currentMoveSpeed;
    [SerializeField] private float currentBoostSpeed;
    [SerializeField] private float currentStartMovePower;
    [SerializeField] private float currentBreakMovePower;
    [SerializeField] private float currentMoveSpeedInAir;
    [SerializeField] private float currentDelayMoveSpeedInAir;

    private float newCurrentMoveSpeed, newCurrentRotationSpeed;
    private float newCurrentMoveSpeedInAirVertical, newCurrentMoveSpeedInAirHorizontal;

    
    [Header("Jump settings")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float gravityMultiplier;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float checkerOffset;
    private bool isGrounded;


    [Header("Components")]
    [SerializeField] Transform tr;
    [SerializeField] Rigidbody rb;
    private ShipInput shipInput;
    
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

        // Просчет скорости и инерции движения в воздухе
        currentMoveSpeedInAir = moveSpeedInAir;
        currentDelayMoveSpeedInAir = moveSpeedInAir/100 * delayMoveSpeedInAir;
        
        Movement(currentMoveSpeed, currentBoostSpeed);
        MovementInAir(currentMoveSpeedInAir);
        Rotation();
        Gravity();
    }

    private void Movement(float speed, float boost)
    {
        // Движение корабля на земле и с ускорением
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
    private void MovementInAir(float speed)
    {
        // Движение корабля в воздухе
        if (isGrounded == false)
        {
            // Движение вперед и назад
            if (shipInput.GetVerticalDirection() > 0)
            {
                newCurrentMoveSpeedInAirVertical = Mathf.Lerp(newCurrentMoveSpeedInAirVertical, speed, Time.deltaTime / currentDelayMoveSpeedInAir);
            }
            if (shipInput.GetVerticalDirection() < 0)
            {
                newCurrentMoveSpeedInAirVertical = Mathf.Lerp(newCurrentMoveSpeedInAirVertical, -speed, Time.deltaTime / currentDelayMoveSpeedInAir);
            }
            if (shipInput.GetVerticalDirection() == 0)
            {
                newCurrentMoveSpeedInAirVertical = Mathf.Lerp(newCurrentMoveSpeedInAirVertical, 0, Time.deltaTime / currentDelayMoveSpeedInAir);
            }

            // Движение в стороны
            if (shipInput.GetHorizontalDirection() > 0)
            {
                newCurrentMoveSpeedInAirHorizontal = Mathf.Lerp(newCurrentMoveSpeedInAirHorizontal, speed, Time.deltaTime / currentDelayMoveSpeedInAir);
            }
            if (shipInput.GetHorizontalDirection() < 0)
            {
                newCurrentMoveSpeedInAirHorizontal = Mathf.Lerp(newCurrentMoveSpeedInAirHorizontal, -speed, Time.deltaTime / currentDelayMoveSpeedInAir);
            }
            if (shipInput.GetHorizontalDirection() == 0)
            {
                newCurrentMoveSpeedInAirHorizontal = Mathf.Lerp(newCurrentMoveSpeedInAirHorizontal, 0, Time.deltaTime / currentDelayMoveSpeedInAir);
            }

            tr.position += tr.right * newCurrentMoveSpeedInAirHorizontal * Time.deltaTime;
            tr.position += tr.forward * newCurrentMoveSpeedInAirVertical * Time.deltaTime;
        }
    }

    private void Rotation()
    {
        // Поворот корабля по оси У на земле
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
    
    private void Jump()
    {
        // Прыжок
        if (shipInput.GetJumpButton() && isGrounded)
        {
            rb.velocity = new Vector3(0, jumpForce, 0);
            // Обнуление скорости корабля на цемле, чтобы при приземлении
            // корабль не сохранял ускорение, если оно было нажато перед прыжком
            newCurrentMoveSpeed = 0;
        }
    }

    private void Gravity()
    {
        // Действие увеличения гравитации,
        // когда корабль в воздухе
        if (!isGrounded)
        {
            tr.position -= tr.up * gravityMultiplier * Time.deltaTime;
        }
    }
}
