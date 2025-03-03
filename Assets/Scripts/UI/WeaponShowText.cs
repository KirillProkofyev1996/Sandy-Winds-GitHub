using UnityEngine;
using TMPro;

public class WeaponShowText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI weaponText;
    [SerializeField] private ShipShooter shipShooter;

    private void Update()
    {
        weaponText.text = shipShooter.GetWeapon();
    }
}
