using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSurfaceCollision : MonoBehaviour
{
    private Vector3 surfaceNormal;

    private void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            surfaceNormal = contact.normal;
        }
    }

    public Vector3 GetNormal()
    {
        return surfaceNormal;
    }
}
