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

        if (shipShooter.GetCanShoot())
        {
            // Метод появления прицела в пространстве в цвете
            aim.ShowColoredAim(IsAimButtonPressed, shipShooter.GetDistance(), shipShooter.GetWeaponDistance());
        }
        if (!shipShooter.GetCanShoot())
        {
            // Метод показывает только красный прицел
            aim.ShowRedAim();
        }
    }

    // Метод переключения основной камеры и камеры
    // для прицеливания по нажатию правой кнопки мыши
    private void CameraSwitcher()
    {
        if (shipInput.GetCameraButton() == true)
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

    public bool GetIsAimButtonPressed()
    {
        return IsAimButtonPressed;
    }
}
