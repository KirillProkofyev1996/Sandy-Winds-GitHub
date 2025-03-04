using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip visionSound;
    public Color ButtonColor;

    /// <summary>
    /// SceneMove нужен для перехода между сценами
    /// </summary>
    public void SceneMove(int SceneNumber)
    {
        SceneManager.LoadScene(SceneNumber);
    }

    /// <summary>
    /// Link позволяет переходить по ссылкам. В string выставляются ссылки для кнопках
    /// </summary>
    public void Link(string LinkID)
    {
        Application.OpenURL(LinkID);
    }

    /// <summary>
    /// Exit служит для выхода из игры
    /// </summary>
    public void Exit()
    {
        Application.Quit();
    }

    /// <summary>
    /// проигрывание звука при наведении курсором мыши
    /// </summary>
    public void Vision()
    {
        audioSource.PlayOneShot(visionSound);
    }

    public void VisionColor(GameObject ColorButton)
    {
        ColorButton.GetComponent<Image>().color = ButtonColor;
    }
}
