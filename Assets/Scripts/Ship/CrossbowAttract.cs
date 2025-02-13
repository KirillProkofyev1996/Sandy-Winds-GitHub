using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowAttract : MonoBehaviour
{
    public float attractionForce = 5f; // Сила притяжения
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        AttractToOtherBolts();
    }

    private void AttractToOtherBolts()
    {
        // Находим все снаряды в сцене
        CrossbowAttract[] bolts = FindObjectsOfType<CrossbowAttract>();

        foreach (CrossbowAttract otherBolt in bolts)
        {
            if (otherBolt != this) // Игнорируем себя
            {
                float distance = Vector3.Distance(transform.position, otherBolt.transform.position);

                // Если снаряды достаточно близко, применяем силу притяжения
                if (distance < 5f) // Например, 5 единиц
                {
                    Vector3 directionToOther = (otherBolt.transform.position - transform.position).normalized;
                    Vector3 force = directionToOther * attractionForce * Time.fixedDeltaTime;

                    // Применяем силу притяжения
                    rb.AddForce(force);
                }
            }
        }
    }
}
