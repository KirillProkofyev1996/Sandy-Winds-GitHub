using UnityEngine;
using UnityEngine.InputSystem;

public class InputSchemeSwitcher : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;

    // В главном меню включить переменную  
    [SerializeField] private bool isStartLevelMainMenu; 

    private InputActionMap shipInputMap;
    private InputActionMap uiInputMap;

    private void Awake()
    {
        shipInputMap = inputActions.FindActionMap("Ship");
        uiInputMap = inputActions.FindActionMap("UI");
    }

    private void Start()
    {
        if (isStartLevelMainMenu)
        {
            SetUiInput();
        }
        if (!isStartLevelMainMenu)
        {
            SetShipInput();
        }
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