using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //must have this line.
    public static GameManager instance;
    public int difficulty = 0;

    private void Awake()
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
}
