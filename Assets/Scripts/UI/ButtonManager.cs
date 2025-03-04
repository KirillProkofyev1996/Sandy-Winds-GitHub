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
    /// SceneMove ����� ��� �������� ����� �������
    /// </summary>
    public void SceneMove(int SceneNumber)
    {
        SceneManager.LoadScene(SceneNumber);
    }

    /// <summary>
    /// Link ��������� ���������� �� �������. � string ������������ ������ ��� �������
    /// </summary>
    public void Link(string LinkID)
    {
        Application.OpenURL(LinkID);
    }

    /// <summary>
    /// Exit ������ ��� ������ �� ����
    /// </summary>
    public void Exit()
    {
        Application.Quit();
    }

    /// <summary>
    /// ������������ ����� ��� ��������� �������� ����
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
