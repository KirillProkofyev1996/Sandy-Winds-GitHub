using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] Transform ship;
    [SerializeField] float rayLength;
    [SerializeField] LayerMask groundLayer;
    private float yPosition;

    private void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(ship.position, Vector3.down, out hit, rayLength, groundLayer))
        {
            yPosition = hit.point.y;
        }
        else
        { 
            yPosition = ship.position.y; 
        }

        transform.position = new Vector3(ship.position.x, yPosition, ship.position.z);
    }
}
