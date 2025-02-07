using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class GiantHyenaAttack : MonoBehaviour
{
    [SerializeField] private float distanceAttack;

    [SerializeField] private float retreatDistance;
    [SerializeField] private float retreatTime;
    [SerializeField] private float attackDistance;
    [SerializeField] private float attackDuration;

    [SerializeField] private GameObject ship;

    public bool isAttack;
    private float distance;
    private Vector3 direction;
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
        if (distance <= distanceAttack && isAttack == false)
        {
            StartCoroutine(AttackRoutine());
        }
    }

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
        yield return new WaitForSeconds(1f);
    }
}
