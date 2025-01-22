using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Aim2D : MonoBehaviour
{
    [SerializeField] private GameObject aim;
    public Vector3 direction;

    private void Update()
    {
        transform.position = Input.mousePosition;

        Direction();
    }

    private void Direction()
    {
        Vector3 cursorPosition = Input.mousePosition;
        
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f);
        direction = cursorPosition - screenCenter;
        direction.z = direction.y;
        direction.y = 0;

        direction.Normalize();
    }

    public Vector3 GetDirection()
    {
        return direction;
    }

    public void SetAimImage(bool IsAimButton)
    {
        if (IsAimButton == true)
        {
            aim.SetActive(true);
        }
        else
        {
            aim.SetActive(false);
        }
    }
}
