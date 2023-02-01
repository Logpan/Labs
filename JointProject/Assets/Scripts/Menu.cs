using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void SelectLevel()
    {
        SceneManager.LoadScene("LevelSelector");
    }

    public void Play()
    {
        SceneManager.LoadScene("Level" + GetLevel());
    }

    public void GoMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void OnApplicationQuit()
    {
        OnApplicationQuit();
    }

    int GetLevel()
    {
        return GameObject.Find("_GameController").GetComponent<GameController>().level;
    }
}
