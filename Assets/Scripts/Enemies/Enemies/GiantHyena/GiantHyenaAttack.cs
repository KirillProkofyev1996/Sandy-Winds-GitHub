using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class GiantHyenaAttack : MonoBehaviour
{
    // Вызов метода ShootOff из ShipShooter,
    // который отключает стрельбу корабля на время
    [SerializeField] private UnityEvent OnHowl;
    [SerializeField] private PlayableDirector cameraShakeTimeline;


    [Header("Attack settings")]
    [SerializeField] private float distanceToAttack;
    [SerializeField] private float retreatDistance;
    [SerializeField] private float retreatTime;
    [SerializeField] private float attackDistance;
    [SerializeField] private float attackDuration;


    [Header("Components")]
    [SerializeField] private GameObject ship;
    private EnemyHealth health;
    private bool isAttack;
    private bool isHowl;
    private float distance;

    private void Start()
    {
        health = GetComponent<EnemyHealth>();
    }

    private void Update()
    {   
        distance = Vector3.Distance(transform.position, ship.transform.position);

        Attack();
        HowlAttack();
    }

    // Метод обычной атаки гиены
    private void Attack()
    {
        if (distance <= distanceToAttack && isAttack == false)
        {
            StartCoroutine(AttackRoutine());
        }
    }

    // Описание рутины обычной атаки гиены
    private IEnumerator AttackRoutine()
    {
        isAttack = true;

        Vector3 retreatPosition = transform.position - transform.forward * retreatDistance;
        float retreatStartTime = Time.time;
        while (Time.time < retreatStartTime + retreatTime)
        {
            transform.position = Vector3.Lerp(transform.position, retreatPosition, (Time.time - retreatStartTime) / retreatTime);
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);

        Vector3 attackTarget = transform.position + transform.forward * attackDistance;
        float attackStartTime = Time.time;
        while (Time.time < attackStartTime + attackDuration)
        {
            transform.position = Vector3.Lerp(transform.position, attackTarget, (Time.time - attackStartTime) / attackDuration);
            yield return null;
        }

        isAttack = false;
        yield return new WaitForSeconds(2f);
    }

    // Метод суперспостобности ВОЙ, который
    // отключает стрельбу корабля на время
    private void HowlAttack()
    {
        if (health.GetHealth() <= 350 && !isHowl)
        {
            isHowl = true;
            OnHowl.Invoke();
            cameraShakeTimeline.Play();
        }
    }
}
