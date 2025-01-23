using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private CinemachineVirtualCamera aimCamera;
    [SerializeField] private CinemachineVirtualCamera shipCamera;
    private bool IsAimButtonPressed;

    [Header("Components")]
    [SerializeField] private Aim aim;

    private void Update()
    {
        CameraSwitcher();
        aim.ShowWhiteAim(IsAimButtonPressed);
    }

    private void CameraSwitcher()
    {
        if (Input.GetMouseButtonDown(1))
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
