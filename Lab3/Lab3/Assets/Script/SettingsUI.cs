using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SettingsUI : MonoBehaviour
{
    public void Easy()
    {
        GameManager.instance.difficultyLevel = 0;
        SceneManager.LoadScene("Menu");
    }

    public void Hard()
    {
        GameManager.instance.difficultyLevel = 1;
        SceneManager.LoadScene("Menu");
    }

    public void Impossible()
    {
        GameManager.instance.difficultyLevel = 2;
        SceneManager.LoadScene("Menu");
    }

    public void BackMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
