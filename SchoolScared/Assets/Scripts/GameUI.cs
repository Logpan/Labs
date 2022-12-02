using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("School");
    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void Difficulty()
    {
        SceneManager.LoadScene("Difficulty");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
