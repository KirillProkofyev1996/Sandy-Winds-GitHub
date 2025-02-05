using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WhiteCrabAttack : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float timeToHit;
    [SerializeField] private GameObject ship;
    private float time;
    private bool isAttack;

    private void Update()
    {
        Hit(ship);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ship"))
        {
            isAttack = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ship"))
        {
            isAttack = false;
        }
    }

    private void Hit(GameObject ship)
    {
        if (isAttack)
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
        if (!isAttack)
        {
            time = 0;
        }
    }
}