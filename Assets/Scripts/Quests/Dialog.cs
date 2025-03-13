using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class dialog : MonoBehaviour{
    [SerializeField] private GameObject dialog_text;
    [SerializeField] private GameObject quest_button;
    [SerializeField] private string[] phrases = {"hello", "hello", "test1", "test2"};
    int phrase = 0;
    public void say() {
        if (phrase >= phrases.Length) {
            this.gameObject.SetActive(false);
            quest_button.SetActive(true);
            return;
        } 
        Debug.Log(phrases[phrase]);
        dialog_text.GetComponent<TMP_Text>().SetText(phrases[phrase]);
        ++phrase;
        Invoke("say", 1f);
    }
}
