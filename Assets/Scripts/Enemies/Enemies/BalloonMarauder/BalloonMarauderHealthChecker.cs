using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonMarauderHealthChecker : MonoBehaviour
{
    [SerializeField] private GameObject side1, sede2, side3, side4;
    [SerializeField] private float timeToDie;

    private void FixedUpdate()
    {
        HealthChecker();
    }

    private void Die()
    {
        Destroy(gameObject, timeToDie);
    }

    public void HealthChecker()
    {
        if (side1 == null && sede2 == null && side3 == null && side4 == null)
        {
            Die();
        }
    }
}
