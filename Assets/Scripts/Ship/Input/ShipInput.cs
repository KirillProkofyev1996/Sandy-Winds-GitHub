using UnityEngine;

public class ShipInput : MonoBehaviour
{
    [Header("Variables")]
    private bool isShootButton, isCameraButton, isJumpButton, isBoostButton, isInteractButton, isWeaponButton;
    private Vector2 horizontalDirection, verticalDirection, lookDirection, zoom;
    private Vector3 aimPosition;

    [Header("Components")]
    private InputActions inputActions;

    private void Awake()
    {
        inputActions = new InputActions();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Update()
    {
        verticalDirection = inputActions.Ship.Move.ReadValue<Vector2>();
        horizontalDirection = inputActions.Ship.Rotate.ReadValue<Vector2>();
        lookDirection = inputActions.Ship.Look.ReadValue<Vector2>();
        aimPosition = inputActions.Ship.Aim.ReadValue<Vector2>();
        zoom = inputActions.Ship.Zoom.ReadValue<Vector2>();

        isCameraButton = inputActions.Ship.Camera.triggered;
        isWeaponButton = inputActions.Ship.Weapon.triggered;
        isJumpButton = inputActions.Ship.Jump.IsInProgress();
        isInteractButton = inputActions.Ship.Interact.IsInProgress();
        isBoostButton = inputActions.Ship.Boost.IsInProgress();
        isShootButton = inputActions.Ship.Shoot.IsPressed();
    }

    // Публичные методы получения переменных для
    // управления кораблем, стрельбой и прицеливанием
    public Vector2 GetHorizontalDirection()
    {
        return horizontalDirection;
    }
    public Vector2 GetVerticalDirection()
    {
        return verticalDirection;
    }
    public Vector2 GetLookDirection()
    {
        return lookDirection;
    }
    public Vector3 GetAimPosition()
    {
        return aimPosition;
    }
    public bool GetShootButton()
    {
        return isShootButton;
    }
    public bool GetCameraButton()
    {
        return isCameraButton;
    }
    public bool GetJumpButton()
    {
        return isJumpButton;
    }
    public bool GetBoostButton()
    {
        return isBoostButton;
    }
    public bool GetInteractButton()
    {
        return isInteractButton;
    }
    public Vector2 GetZoom()
    {
        return zoom;
    }
    public bool GetWeapon()
    {
        return isWeaponButton;
    }
}
