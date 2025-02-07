using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthShowText : MonoBehaviour
{
    [SerializeField] private Text healthText;
    [SerializeField] private float health;
    [SerializeField] private ShipHealth shipHealth;

    private void Update()
    {
        healthText.text = shipHealth.currentHealth.ToString();
    }
}
