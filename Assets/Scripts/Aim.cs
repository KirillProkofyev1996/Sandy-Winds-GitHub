using UnityEngine;

public class Aim : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject greenAim, redAim;

    private void Update()
    { 
        TrackAimPosition();
    }

    // Метод отслеживания прицела лучем на поверхность,
    // где высота прицела равна высоте точке выстрела
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
