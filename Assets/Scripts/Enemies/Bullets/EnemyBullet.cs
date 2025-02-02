using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ship"))
        {
            other.GetComponent<ShipHealth>().TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}