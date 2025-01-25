using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    [Header("Movement vars")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float procentMovementSpeed; // Сила воздействия на скорость в процентах
    [SerializeField] private float boostSpeed; // Сила воздействия на скорость в процентах
    [SerializeField] private float startStoppingPower, breakStoppingPower; // Сила воздействия на скорость в процентах

    [Header("Current vars")]
    [SerializeField] private float currentMovementSpeed;
    [SerializeField] private float currentBoostSpeed;
    [SerializeField] private float currentStartStoppingPower;
    [SerializeField] private float currentBreakStoppingPower;

    private float currentSpeed, currentRotationSpeed;

    
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

    private void Update()
    {   
        // Проверка на нахождение корабля на земле
        isGrounded = Physics.CheckSphere(groundChecker.position, checkerOffset, groundMask);
        
        // Просчет скорости, ускорения, иннерции остановки
        // и начала движения с учетом процентов
        currentMovementSpeed = movementSpeed + (movementSpeed/100 * procentMovementSpeed);
        currentBoostSpeed = currentMovementSpeed + (currentMovementSpeed/100 * boostSpeed);
        currentStartStoppingPower = movementSpeed/100 * startStoppingPower;
        currentBreakStoppingPower = movementSpeed/100 * breakStoppingPower;
        
        Movement(currentMovementSpeed, currentBoostSpeed);
        Rotation();
        Jump();
        Gravity();
    }

    private void Movement(float speed, float boost)
    {    
        if (shipInput.GetVerticalDirection() > 0)
        {
            if (shipInput.GetBoostButton())
            {
                currentSpeed = Mathf.Lerp(currentSpeed, speed + boost, Time.deltaTime/currentStartStoppingPower);
            }
            else
            {
                currentSpeed = Mathf.Lerp(currentSpeed, speed, Time.deltaTime/currentStartStoppingPower);
            }
        }
        if (shipInput.GetVerticalDirection() < 0)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, -speed, Time.deltaTime/currentStartStoppingPower);
        }
        if (shipInput.GetVerticalDirection() == 0)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, Time.deltaTime/currentBreakStoppingPower);
        }

        tr.position += tr.forward * currentSpeed * Time.deltaTime;
    }

    private void Rotation()
    {
        if (shipInput.GetHorizontalDirection() > 0)
        {
            currentRotationSpeed = Mathf.Lerp(currentRotationSpeed, rotationSpeed, Time.deltaTime/currentStartStoppingPower);
        }
        if (shipInput.GetHorizontalDirection() < 0)
        {
            currentRotationSpeed = Mathf.Lerp(currentRotationSpeed, -rotationSpeed, Time.deltaTime/currentStartStoppingPower);
        }
        if (shipInput.GetHorizontalDirection() == 0)
        {
            currentRotationSpeed = Mathf.Lerp(currentRotationSpeed, 0, Time.deltaTime/currentBreakStoppingPower);
        }

        tr.Rotate(new Vector3(0, currentRotationSpeed, 0));
    }
    
    private void Jump()
    {
        if (shipInput.GetJumpButton() && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }
    }

    private void Gravity()
    {
        if (!isGrounded)
        {
            //rb.velocity += new Vector3(0, Physics.gravity.y * gravityMultiplier * Time.deltaTime, 0);
            //rb.velocity = new Vector3(0, Physics.gravity.y * gravityMultiplier * Time.deltaTime, 0); 
            tr.position -= tr.up * gravityMultiplier * Time.deltaTime;
        }
    }
}
