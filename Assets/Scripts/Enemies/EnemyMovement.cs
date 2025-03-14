using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform ship;

    [Header("Slowdown mechanics")]
    [SerializeField] private float slowdownByCrossbowProcent = 20f;
    [SerializeField] private float slowdownByCrossbowDuration = 10f;
    [SerializeField] private float slowdownBySidesGunProcent = 40f;
    [SerializeField] private float slowdownBySidesGunDuration = 10f;
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        agent.SetDestination(ship.position);
    }

    // Для чертежа оружий с замедлением противника
    private void SetSlowerSpeed(float procent)
    {
        agent.speed -= agent.speed / 100 * procent;
    }

    // Для чертежа арбалета
    private void SetNormalSpeed()
    {
        agent.speed += agent.speed / 100 * 20;
    }

    // Метод используется для чертежа арбалета
    // при попадании в противника 
    public void SlowdownByCrossbow()
    {
        SetSlowerSpeed(slowdownByCrossbowProcent);
        Invoke("SetNormalSpeed", slowdownByCrossbowDuration);
    }

    // Метод используется для чертежа автоматов по бокам
    // при попадании в противника 
    public void SlowdownBySidesGun()
    {
        SetSlowerSpeed(slowdownBySidesGunProcent);
        Invoke("SetNormalSpeed", slowdownBySidesGunDuration);
    }
}