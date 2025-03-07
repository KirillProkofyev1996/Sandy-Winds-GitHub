using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform ship;

    [Header("Slowdown mechanics")]
    [SerializeField] private float slowdownProcent = 20f;
    [SerializeField] private float slowdownDuration = 10f;
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        agent.SetDestination(ship.position);
    }

    // Для чертежа арбалета
    private void SetSlowerSpeed()
    {
        agent.speed -= agent.speed / 100 * slowdownProcent;
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
        SetSlowerSpeed();
        Invoke("SetNormalSpeed", slowdownDuration);
    }
}