using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject whiteAim, redAim;

    private void Update()
    { 
        TrackAimPosition();
    }

    private void TrackAimPosition()
    {
        Vector3 mousePosition = Input.mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 hitPosition = hit.point;
            hitPosition.y = shootPoint.position.y;

            transform.position = hitPosition;
        }
    }

    public void ShowWhiteAim(bool IsAimButton)
    {
        if (IsAimButton == true)
        {
            whiteAim.SetActive(true);
        }
        else
        {
            whiteAim.SetActive(false);
            redAim.SetActive(false);
        }
    }

    public void ShowRedAim(float distance, float weaponDistance)
    {
        if (distance >= weaponDistance)
        {
            redAim.SetActive(true);
            whiteAim.SetActive(false);
        }
        else
        {
            redAim.SetActive(false);
            whiteAim.SetActive(true);
        }
    }

    public Vector3 GetAimPosition()
    {
        return transform.position;
    }
}
