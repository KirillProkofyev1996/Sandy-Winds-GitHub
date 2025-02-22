using UnityEditor;
using UnityEngine;

public class ShipInput : MonoBehaviour
{
    [Header("Variables")]
    private float horizontalDirection, verticalDirection;
    private bool isShootButton, isAimButton, isJumpButton, isBoostButton, isRecoveryButton, isInteractButton;

    private void Update()
    {
        horizontalDirection = Input.GetAxis("Horizontal");
        verticalDirection = Input.GetAxis("Vertical");
        isShootButton = Input.GetButton("Fire1");
        isAimButton = Input.GetMouseButtonDown(1);
        isJumpButton = Input.GetButton("Jump");
        isBoostButton = Input.GetKey(KeyCode.LeftShift);
        isRecoveryButton = Input.GetKey(KeyCode.R);
        isInteractButton = Input.GetButton("Interact");
    }

    // Публичные методы получения переменных для
    // управления кораблем, стрельбой и прицеливанием
    public float GetHorizontalDirection()
    {
        return horizontalDirection;
    }
    public float GetVerticalDirection()
    {
        return verticalDirection;
    }
    public bool GetShootButton()
    {
        return isShootButton;
    }
    public bool GetAimButton()
    {
        return isAimButton;
    }
    public bool GetJumpButton()
    {
        return isJumpButton;
    }
    public bool GetBoostButton()
    {
        return isBoostButton;
    }
    public bool GetRecoveryButton()
    {
        return isRecoveryButton;
    }
    public bool GetInteractButton()
    {
        Debug.Log(isInteractButton);
        return isInteractButton;
    }
}
