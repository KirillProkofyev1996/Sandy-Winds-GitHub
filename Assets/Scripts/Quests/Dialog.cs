using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialog : MonoBehaviour
{
    [SerializeField] private GameObject dialogText;
    [SerializeField] private string[] phrases = {"hello", "hello", "test1", "test2"};
    int phrase = 0;

    public void Say() 
    {
        if (phrase >= phrases.Length) 
        {
            gameObject.SetActive(false);
            return;
        }

        Debug.Log(phrases[phrase]);
        dialogText.GetComponent<TMP_Text>().SetText(phrases[phrase]);
        ++phrase;

        Invoke("Say", 1f);
    }
}
