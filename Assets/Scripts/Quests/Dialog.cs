using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialog : MonoBehaviour
{
    [SerializeField] private GameObject dialog;
    [SerializeField] private GameObject buttons;
    [SerializeField] private string[] phrases = {"hello", "hello", "test1", "test2"};
    int phrase = 0;

    public void Say() 
    {
        if (phrase >= phrases.Length) 
        {
            dialog.SetActive(false);
            buttons.SetActive(true);
            return;
        }

        Debug.Log(phrases[phrase]);
        dialog.GetComponentInChildren<TMP_Text>().SetText(phrases[phrase]);
        ++phrase;

        Invoke("Say", 1f);
    }
}
