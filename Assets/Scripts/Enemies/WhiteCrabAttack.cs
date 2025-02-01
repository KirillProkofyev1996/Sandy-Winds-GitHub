using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WhiteCrabAttack : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float radius;
    [SerializeField] private float timeToHit;
    [SerializeField] private GameObject ship;
    private float time;
    private float distance;

    private void Update()
    {
        distance = Vector3.Distance(transform.position, ship.transform.position);
        Hit(ship);
    }

    private void Hit(GameObject ship)
    {
        if (distance <= radius)
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
        if (distance > radius)
        {
            time = 0;
        }
    }
}