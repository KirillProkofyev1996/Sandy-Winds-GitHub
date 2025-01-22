using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    [Header("Movement vars")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float procentMovementSpeed;    
    [SerializeField] private float boostSpeed;
    [SerializeField] private float startStoppingPower, breakStoppingPower;
    [SerializeField] private float curMovementSpeed, curBoostSpeed, curStartStoppingPower, curBreakStoppingPower;
    private float currentSpeed, currentRotationSpeed;

    
    [Header("Jump settings")]
    [SerializeField] private float jumpForce;
    public float gravity = -9.81f;
    public float gravityMultiplier;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float checkerOffset;
    private bool isGrounded;


    [Header("Components")]
    [SerializeField] Transform tr;
    [SerializeField] Rigidbody rb;
    private ShipInput shipInput;

    [Header("Tilt rotation settings")]
    [SerializeField] private ShipSurfaceCollision shipSurfaceCollision;
    [SerializeField] private float tiltSpeed;

    private void Start()
    {
        shipInput = GetComponent<ShipInput>();
    }

    private void Update()
    {   
        isGrounded = Physics.CheckSphere(groundChecker.position, checkerOffset, groundMask);
        
        curMovementSpeed = movementSpeed + (movementSpeed/100 * procentMovementSpeed);
        curBoostSpeed = curMovementSpeed + (curMovementSpeed/100 * boostSpeed);
        curStartStoppingPower = movementSpeed/100 * startStoppingPower;
        curBreakStoppingPower = movementSpeed/100 * breakStoppingPower;
        
        Movement(curMovementSpeed, curBoostSpeed);
        Rotation();
        Jump();
        Gravity();

        TiltRotation();
    }

    private void TiltRotation()
    {
        Quaternion targetRotation = Quaternion.FromToRotation(transform.up, shipSurfaceCollision.GetNormal());
        tr.rotation = Quaternion.Slerp(tr.rotation, targetRotation, Time.deltaTime * tiltSpeed);
    }

    private void Movement(float speed, float boost)
    {    
        if (shipInput.GetVerticalDirection() > 0)
        {
            if (shipInput.GetBoostButton())
            {
                currentSpeed = Mathf.Lerp(currentSpeed, speed + boost, Time.deltaTime/curStartStoppingPower);
            }
            else
            {
                currentSpeed = Mathf.Lerp(currentSpeed, speed, Time.deltaTime/curStartStoppingPower);
            }
        }
        if (shipInput.GetVerticalDirection() < 0)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, -speed, Time.deltaTime/curStartStoppingPower);
        }
        if (shipInput.GetVerticalDirection() == 0)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, Time.deltaTime/curBreakStoppingPower);
        }

        tr.position += tr.forward * currentSpeed * Time.deltaTime;
    }

    private void Rotation()
    {
        if (shipInput.GetHorizontalDirection() > 0)
        {
            currentRotationSpeed = Mathf.Lerp(currentRotationSpeed, rotationSpeed, Time.fixedDeltaTime/curStartStoppingPower);
        }
        if (shipInput.GetHorizontalDirection() < 0)
        {
            currentRotationSpeed = Mathf.Lerp(currentRotationSpeed, -rotationSpeed, Time.fixedDeltaTime/curStartStoppingPower);
        }
        if (shipInput.GetHorizontalDirection() == 0)
        {
            currentRotationSpeed = Mathf.Lerp(currentRotationSpeed, 0, Time.fixedDeltaTime/curBreakStoppingPower);
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
            rb.velocity += new Vector3(0, Physics.gravity.y * gravityMultiplier * Time.deltaTime, 0);
        }
    }
}
