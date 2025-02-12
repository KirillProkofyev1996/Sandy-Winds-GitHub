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
    [SerializeField] private float retreatDuration;
    [SerializeField] private float attackDistance;
    [SerializeField] private float attackDuration;
    [SerializeField] private float biteDistance;
    [SerializeField] private float biteDuration;


    [Header("Components")]
    [SerializeField] private GameObject ship;
    private EnemyHealth health;
    private bool isAttack;
    private bool isHowl;
    private bool isBite;
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
        BiteCheck();
    }

    // Метод обычной атаки в обычном состоянии и в состоянии УКУС
    private void Attack()
    {
        if (!isBite)
        {
            if (distance <= distanceToAttack && isAttack == false)
            {
                StartCoroutine(AttackRoutine());
            }
        }
        if (isBite)
        {
            if (distance <= distanceToAttack && isAttack == false)
            {
                StartCoroutine(BiteRoutine());
            }
        }
    }

    // Описание рутины обычной атаки гиены
    private IEnumerator AttackRoutine()
    {
        isAttack = true;

        Vector3 retreatPosition = transform.position - transform.forward * retreatDistance;
        float retreatStartTime = Time.time;
        while (Time.time < retreatStartTime + retreatDuration)
        {
            transform.position = Vector3.Lerp(transform.position, retreatPosition, (Time.time - retreatStartTime) / retreatDuration);
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
        yield return new WaitForSeconds(1f);
    }

    // Описание рутины суперспособности УКУС
    private IEnumerator BiteRoutine()
    {
        isAttack = true;

        Vector3 retreatPosition = transform.position - transform.forward * retreatDistance;
        float retreatStartTime = Time.time;
        while (Time.time < retreatStartTime + retreatDuration)
        {
            transform.position = Vector3.Lerp(transform.position, retreatPosition, (Time.time - retreatStartTime) / retreatDuration);
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);

        Vector3 attackTarget = transform.position + transform.forward * biteDistance;
        float attackStartTime = Time.time;
        while (Time.time < attackStartTime + biteDuration)
        {
            transform.position = Vector3.Lerp(transform.position, attackTarget, (Time.time - attackStartTime) / biteDuration);
            yield return null;
        }

        isAttack = false;
        isBite = false;
        yield return new WaitForSeconds(1f);
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

    // Метод активации суперспособности УКУС
    private void BiteCheck()
    {
        if (health.GetHealth() <= 150 && !isBite)
        {
            isBite = true;
        }
    }

    // Метод получения isBite для передачи в GiantHyenaContact, 
    // чтобы установить нужный урон при действии способности
    public bool GetIsBite()
    {
        return isBite;
    }
}
