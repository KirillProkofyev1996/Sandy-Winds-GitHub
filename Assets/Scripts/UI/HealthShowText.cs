using UnityEngine;
using TMPro;

public class HealthShowText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private float health;
    [SerializeField] private ShipHealth shipHealth;

    private void Update()
    {
        healthText.text = shipHealth.GetHealth().ToString();
    }
}
