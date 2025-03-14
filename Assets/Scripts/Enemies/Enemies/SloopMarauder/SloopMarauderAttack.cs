using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SloopMarauderAttack : MonoBehaviour
{
    [SerializeField] private Rigidbody bullet;
    [SerializeField] private float speed;
    [SerializeField] private float radius;
    [SerializeField] private float timeToShoot;
    [SerializeField] private int countsToSuperShoot;
    [SerializeField] private float superShootAngle;
    [SerializeField] private int superShootBullets;
    [SerializeField] private GameObject ship;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private bool isCanShoot = true;
    [SerializeField] private float slowdownBySidesGunProcent = 30f; // Для автоматов по бокам с возможностью замедлить атаку
    [SerializeField] private float slowdownBySidesGunDuration = 10f; // Для автоматов по бокам с возможностью замедлить атаку
    private float time;
    private int counts;
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
            if (distance <= radius && Time.time >= time && counts < countsToSuperShoot - 1)
            {
                Rigidbody newBullet = Instantiate(bullet, shootPoint.position, Quaternion.identity);
                newBullet.velocity = direction * speed;
                time = Time.time + timeToShoot;

                counts++;
            }
            if (distance <= radius && Time.time >= time && counts == countsToSuperShoot - 1)
            {
                float halfSpread = superShootAngle / 2f;
                float angleIncrement = superShootAngle / (superShootBullets - 1);

                for (int i = 0; i < superShootBullets; i++)
                {
                    float angle = -halfSpread + i * angleIncrement;
                    Quaternion bulletRotation = Quaternion.Euler(0, angle, 0);
                    Rigidbody newBullet = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
                    newBullet.velocity = bulletRotation * direction * speed;
                }

                counts = 0;
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
