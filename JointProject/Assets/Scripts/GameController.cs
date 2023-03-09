using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public int level = 0;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void NextLevel()
    {
        if (level == 0)
            level++;
        else
            Win();
        SceneManager.LoadScene("Level" + level);
    }

    public void Loose()
    {
        SceneManager.LoadScene("Loose");
    }

    public void Win()
    {
        SceneManager.LoadScene("Loose");
    }
}
