using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShipShooter : MonoBehaviour
{
    [SerializeField] private Rigidbody bullet;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float speed;
    [SerializeField] private float fireRate;
    [SerializeField] private float maxDistance;
    private float nextFire;
    private float distance;
    private Vector3 direction;
    
    [SerializeField] private Aim aim;

    private ShipInput shipInput;

    private void Start()
    {
        shipInput = GetComponent<ShipInput>();
    }

    private void Update()
    {
        Shoot();

        aim.ShowRedAim(distance, maxDistance);
    }

    private void Shoot()
    {
        distance = Vector3.Distance(aim.GetAimPoint(), shootPoint.position);

        if (shipInput.GetShootButton() && Time.time >= nextFire && distance <= maxDistance)
        {
            direction = aim.GetAimPoint() - shootPoint.position;

            Rigidbody curBullet = Instantiate(bullet, shootPoint.position, Quaternion.identity);
            curBullet.velocity = direction * speed;

            nextFire = Time.time + fireRate;
        }
    }
}
