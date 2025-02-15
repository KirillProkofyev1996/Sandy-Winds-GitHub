using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCrew : MonoBehaviour
{
    [SerializeField] private int crew;

    public int GetCrew()
    {
        return crew;
    }
}
