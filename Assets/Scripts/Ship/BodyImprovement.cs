using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyImprovement : MonoBehaviour
{
    [SerializeField] private int mode;

    [SerializeField] private float hp;
    [SerializeField] private float strength;
    [SerializeField] private float speed;
    [SerializeField] private InputField mode01Value01;
    [SerializeField] private InputField mode02Value01;
    [SerializeField] private InputField mode03Value01;

    private void Start()
    {
        mode = 0;

        hp = 100;

        strength = 0;

        speed = 100;
    }

    public void Setmode00()
    {
        mode = 0;

        hp = 100;

        speed = 100;
    }

    public void Setmode01()
    {
        mode = 1;

        if (hp == 100)
        {
            hp += float.Parse(mode01Value01.text);
        }
    }

    public void Setmode02()
    {
        mode = 2;

        if (strength == 0)
        {
            strength += float.Parse(mode02Value01.text);

            hp = 100;
        }
    }

    public void Setmode03()
    {
        mode = 3;

        if (speed == 100)
        {
            speed += float.Parse(mode03Value01.text);

            hp = 100;
        }
    }
}
