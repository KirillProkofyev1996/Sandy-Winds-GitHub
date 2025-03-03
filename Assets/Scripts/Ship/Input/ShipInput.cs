using UnityEngine;
using UnityEngine.InputSystem;

public class ShipInput : MonoBehaviour
{
    [Header("Variables")]
    private bool isShootButton;
    private bool isCameraButton;
    private bool isJumpButton;
    private bool isBoostButton;
    private bool isInteractButton;
    private bool isWeaponButton;
    private bool isExtraWeaponButton;
    private bool isEnterUIButton;

    private Vector2 horizontalDirection, verticalDirection, lookDirection, zoom;
    private Vector3 aimPosition;


    [Header("Components")]
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private InputSchemeSwitcher inputSchemeSwitcher;
    private InputActionMap shipInputMap;

    private void Awake()
    {
        shipInputMap = inputActions.FindActionMap("Ship");
    }

    private void Update()
    {
        verticalDirection = shipInputMap.FindAction("Move").ReadValue<Vector2>();
        horizontalDirection = shipInputMap.FindAction("Rotate").ReadValue<Vector2>();
        lookDirection = shipInputMap.FindAction("Look").ReadValue<Vector2>();
        aimPosition = shipInputMap.FindAction("Aim").ReadValue<Vector2>();
        zoom = shipInputMap.FindAction("Zoom").ReadValue<Vector2>();

        isCameraButton = shipInputMap.FindAction("Camera").triggered;
        isWeaponButton = shipInputMap.FindAction("Weapon").triggered;
        isExtraWeaponButton = shipInputMap.FindAction("ExtraWeapon").triggered;
        isEnterUIButton = shipInputMap.FindAction("EnterUI").triggered;
        
        isJumpButton = shipInputMap.FindAction("Jump").IsInProgress();
        isInteractButton = shipInputMap.FindAction("Interact").IsInProgress();
        isBoostButton = shipInputMap.FindAction("Boost").IsInProgress();
        isShootButton = shipInputMap.FindAction("Shoot").IsPressed();

        SwitchToUI();
    }

    private void SwitchToUI()
    {
        if (isEnterUIButton)
        {
            inputSchemeSwitcher.SetUiInput();
        }
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
    public bool GetExtraWeapon()
    {
        return isExtraWeaponButton;
    }
    public bool GetEnterUIButton()
    {
        return isEnterUIButton;
    }
}
