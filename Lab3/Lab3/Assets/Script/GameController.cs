using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    int speed = 0;
    int random = 0;
    public GameObject box;
    public GameObject right;
    public GameObject left;
    string lastSpawn = "left";
    // Start is called before the first frame update
    void Start()
    {
        random = 1000 / (GameManager.instance.difficultyLevel + 1);
        speed = GameManager.instance.difficultyLevel + 5;
    }

    // Update is called once per frame
    void Update()
    {
        if(Random.Range(0,random) == 0)
        {
            if (lastSpawn == "left")
            {
                GameObject boxR = Instantiate(box, right.transform.position, Quaternion.identity);
                boxR.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed,0);
                lastSpawn = "right";
            }
            else
            {
                GameObject boxL = Instantiate(box, left.transform.position, Quaternion.identity);
                boxL.GetComponent<Rigidbody2D>().velocity = new Vector2(speed,0);
                lastSpawn = "left";
            }

        }
    }
}
