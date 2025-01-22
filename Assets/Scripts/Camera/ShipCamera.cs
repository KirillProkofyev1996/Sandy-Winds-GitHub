using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ShipCamera : MonoBehaviour
{
    [SerializeField] private Transform ship;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private float zoomSpeed, maxDistance, minDistance;
    private float initialDistance;

    private CinemachineVirtualCamera vm;
    private CinemachineFramingTransposer transposer;
    
    private void Start()
    {
        vm = GetComponent<CinemachineVirtualCamera>();
        transposer = vm.GetCinemachineComponent<CinemachineFramingTransposer>();

        initialDistance = transposer.m_CameraDistance;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            transform.RotateAround(ship.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.E))
        {
            transform.RotateAround(ship.position, Vector3.up, -rotationSpeed * Time.deltaTime);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0f)
        {
            float newDistance = transposer.m_CameraDistance - scroll * zoomSpeed;
            newDistance = Mathf.Clamp(newDistance, minDistance, maxDistance);
            transposer.m_CameraDistance = newDistance;
        }
    }
}
