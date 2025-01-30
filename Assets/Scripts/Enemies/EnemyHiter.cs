using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHiter : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float timeToHit;
    public bool isHitting;
    private float time;
    private GameObject ship;

    private void Update()
    {
        Hit(ship);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ship"))
        {
            isHitting = true;
            ship = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ship"))
        {
            isHitting = false;
        }
    }

    private void Hit(GameObject ship)
    {
        if (isHitting)
        {
            time += Time.deltaTime;

            if (time >= timeToHit)
            {
                time = 0;
            }
            if (time == 0)
            {
                ship.GetComponent<ShipHealth>().TakeDamage(damage); // Атака корабля
            }
        }
        if (!isHitting)
        {
            time = 0;
        }
    }
}