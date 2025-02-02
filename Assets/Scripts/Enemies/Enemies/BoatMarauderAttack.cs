using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMarauderAttack : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float distanceAttack;
    [SerializeField] private GameObject ship;
    [SerializeField] private float timeToAttack;
    private Vector3 direction;
    private float distance;
    private float time;
    private Rigidbody rb;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {   
        distance = Vector3.Distance(transform.position, ship.transform.position);
        direction = (ship.transform.position - transform.position).normalized;
        Attack();
    }

    private void Attack()
    {
        if (distance <= distanceAttack && Time.time >= time)
        {
            rb.velocity = direction * speed;
            time = Time.time + timeToAttack;
        }
    }
}
