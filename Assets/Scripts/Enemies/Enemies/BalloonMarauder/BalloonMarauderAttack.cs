using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonMarauderAttack : MonoBehaviour
{
    [SerializeField] private Rigidbody bullet;
    [SerializeField] private float speed;
    [SerializeField] private float radius;
    [SerializeField] private float timeToShoot;
    [SerializeField] private GameObject ship;
    [SerializeField] private Transform shootPoint;
    private float time;
    private float distance;
    private Vector3 direction;

    private void Update()
    {
        distance = Vector3.Distance(transform.position, ship.transform.position);
        direction = (ship.transform.position - shootPoint.transform.position).normalized;
        Shoot();
    }

    private void Shoot()
    {
        if (distance <= radius && Time.time >= time)
        {
            Rigidbody newBullet = Instantiate(bullet, shootPoint.position, Quaternion.identity);
            newBullet.velocity = direction * speed;
            time = Time.time + timeToShoot;
        }
    }
}
