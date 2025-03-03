using UnityEngine;
using UnityEngine.InputSystem;

public class InputSchemeSwitcher : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    private InputActionMap shipInputMap;
    private InputActionMap uiInputMap;

    private void Awake()
    {
        shipInputMap = inputActions.FindActionMap("Ship");
        uiInputMap = inputActions.FindActionMap("UI");

        SetShipInput();
    }

    public void SetShipInput()
    {
        uiInputMap.Disable();
        shipInputMap.Enable();
    }

    public void SetUiInput()
    {
        shipInputMap.Disable();
        uiInputMap.Enable();
    }
}
