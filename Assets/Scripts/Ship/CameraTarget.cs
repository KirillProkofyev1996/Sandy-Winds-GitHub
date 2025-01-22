using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] Transform ship;
    private Transform tr;

    private void Start()
    {
        tr = transform;
    }
    private void Update()
    {
        tr.position = new Vector3(ship.position.x, 0, ship.position.z);
    }
}
