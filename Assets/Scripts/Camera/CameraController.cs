using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private CinemachineVirtualCamera aimCamera;
    [SerializeField] private CinemachineVirtualCamera shipCamera;
    private bool IsAimButtonPressed;

    [Header("Components")]
    [SerializeField] private Aim aim;
    [SerializeField] private ShipInput shipInput;
    [SerializeField] private ShipShooter shipShooter;

    private void Update()
    {
        CameraSwitcher();
        
        // Метод появления прицела в пространстве в цвете
        aim.ShowColoredAim(IsAimButtonPressed, shipShooter.GetDistance(), shipShooter.GetWeaponDistance());
    }

    // Метод переключения основной камеры и камеры
    // для прицеливания по нажатию правой кнопки мыши
    private void CameraSwitcher()
    {
        if (shipInput.GetAimButton())
        {
            IsAimButtonPressed =! IsAimButtonPressed;
        }
        if (IsAimButtonPressed == true)
        {
            shipCamera.gameObject.SetActive(false);
            aimCamera.gameObject.SetActive(true);
        }
        if (IsAimButtonPressed == false)
        {
            shipCamera.gameObject.SetActive(true);
            aimCamera.gameObject.SetActive(false);
        }
    }
}
