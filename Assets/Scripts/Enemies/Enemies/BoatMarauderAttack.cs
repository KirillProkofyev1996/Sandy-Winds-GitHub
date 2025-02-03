using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMarauderAttack : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float distanceAttack;
    [SerializeField] private GameObject ship;
    [SerializeField] private float timeToAttack;
    private float timeAttack;
    private Vector3 direction;
    private float distance;
    
    private Rigidbody rb;
    [SerializeField] private BoatMarauderContact contact;
    [SerializeField] private float maxBackDistance;
    [SerializeField] private float backMoveSpeed;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {   
        distance = Vector3.Distance(transform.position, ship.transform.position);
        direction = (ship.transform.position - transform.position).normalized;
        Attack();
        
        MoveBackAfterAttack();
    }

    private void Attack()
    {
        if (distance <= distanceAttack && Time.time >= timeAttack)
        {
            rb.velocity = direction * speed;
            timeAttack = Time.time + timeToAttack;
        }
    }

    private void MoveBackAfterAttack()
    {
        bool isContact = contact.GetIsContact();

        if (isContact)
        {
            rb.velocity = (-direction) * backMoveSpeed;

            if (distance >= maxBackDistance)
            {
                rb.velocity = Vector3.zero;
            }
        }
    }
}
