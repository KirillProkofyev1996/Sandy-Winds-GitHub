using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCrew : MonoBehaviour
{
    [SerializeField] private int maxCrew;
    [SerializeField] private int crew;

    private void Start()
    {
        crew = maxCrew;
    }

    public void ImproveCrew(int value)
    {
        maxCrew += value;
        crew = maxCrew;
    }

    public int GetCrew()
    {
        return crew;
    }
}
