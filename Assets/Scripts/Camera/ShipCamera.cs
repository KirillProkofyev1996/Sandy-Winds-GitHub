using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ShipCamera : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Transform ship;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float zoomSpeed, maxDistance, minDistance;
    private float initialDistance;

    [Header("Components")]
    private CinemachineVirtualCamera vm;
    private CinemachineFramingTransposer transposer;
    
    private void Start()
    {
        // Получение компонента transposer и его значения из virtual camera
        vm = GetComponent<CinemachineVirtualCamera>();
        transposer = vm.GetCinemachineComponent<CinemachineFramingTransposer>();
        initialDistance = transposer.m_CameraDistance;
    }

    void Update()
    {
        CameraRotation();
        CameraZoom();
    }

    private void CameraRotation()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            transform.RotateAround(ship.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.RotateAround(ship.position, Vector3.up, -rotationSpeed * Time.deltaTime);
        }
    }

    private void CameraZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0f)
        {
            float newDistance = transposer.m_CameraDistance - scroll * zoomSpeed;
            newDistance = Mathf.Clamp(newDistance, minDistance, maxDistance);
            transposer.m_CameraDistance = newDistance;
        }
    }
}
