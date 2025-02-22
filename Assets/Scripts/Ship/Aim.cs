using Unity.VisualScripting;
using UnityEngine;

public class Aim : MonoBehaviour
{
    [SerializeField] private GameObject greenAim, redAim;
    [SerializeField] private LayerMask ground;
    [SerializeField] private ShipInput shipInput;

    private void Update()
    { 
        TrackAimPosition();
    }

    // Метод отслеживания прицела лучем на слой поверхности
    private void TrackAimPosition()
    {
        Vector3 mousePosition = shipInput.GetAimPosition();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 50, ground))
        {
            Vector3 hitPosition = hit.point;
            transform.position = hitPosition;
        }
    }

    // Публичный метод скрытия и показа прицела 
    // в цвете при смене на камеру прицеливания
    public void ShowColoredAim(bool IsAimButtonPressed, float distance, float weaponDistance)
    {
        if (IsAimButtonPressed == true)
        {
            if (distance <= weaponDistance)
            {
                greenAim.SetActive(true);
                redAim.SetActive(false);
            }
            else
            {
                greenAim.SetActive(false);
                redAim.SetActive(true);
            }
        }
        if (IsAimButtonPressed == false)
        {
            greenAim.SetActive(false);
            redAim.SetActive(false);
        }
    }

    public Vector3 GetAimPosition()
    {
        return transform.position;
    }
}
