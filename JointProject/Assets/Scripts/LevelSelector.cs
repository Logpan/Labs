using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public void Level()
    {
        GameObject.Find("_GameController").GetComponent<GameController>().level = GetLevel();
        Back();
    }

    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }

    int GetLevel()
    {
        return int.Parse(EventSystem.current.currentSelectedGameObject.name.Substring(5));
    }
}
