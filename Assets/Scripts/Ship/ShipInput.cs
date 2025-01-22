using UnityEngine;

public class ShipInput : MonoBehaviour
{
    private float horizontalDirection, verticalDirection;
    private bool isShootButton, isAimButton, isJumpButton, isBoostButton;

    public void Update()
    {
        horizontalDirection = Input.GetAxis("Horizontal");
        verticalDirection = Input.GetAxis("Vertical");
        isShootButton = Input.GetButton("Fire1");
        isAimButton = Input.GetButton("Fire2");
        isJumpButton = Input.GetButton("Jump");
        isBoostButton = Input.GetKey(KeyCode.LeftShift);
    }

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
}
