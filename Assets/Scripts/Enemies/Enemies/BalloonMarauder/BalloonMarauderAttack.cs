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
    [SerializeField] private bool isCanShoot = true;
    [SerializeField] private float slowdownBySidesGunProcent = 30f;
    [SerializeField] private float slowdownBySidesGunDuration = 10f;
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
        if (isCanShoot)
        {
            if (distance <= radius && Time.time >= time)
            {
                Rigidbody newBullet = Instantiate(bullet, shootPoint.position, Quaternion.identity);
                newBullet.velocity = direction * speed;
                time = Time.time + timeToShoot;
            }
        }
    }

    public void DestroyWeapon(int procent)
    {
        int currentProcent = Random.Range(0, 100);

        if (currentProcent <= procent)
        {
            isCanShoot = false;
        }
    }

    // Для чертежа автоматов по бокам
    private void SetNormalAttackSpeedFromSidesGun()
    {
        timeToShoot += timeToShoot / 100 * slowdownBySidesGunProcent;
    }

    // Для чертежа автоматов по бокам
    private void SetSlowdownAttackSpeedBySidesGun()
    {
        timeToShoot -= timeToShoot / 100 * slowdownBySidesGunProcent;
    }

    // Для чертежа боковых автоматов с замедлением стрельбы противника
    public void SlowdownAttackBySidesGun()
    {
        SetSlowdownAttackSpeedBySidesGun();
        Invoke("SetNormalAttackSpeedFromSidesGun", slowdownBySidesGunDuration);
    }
}
