using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public List<GameObject> classrooms = new List<GameObject>();
    public List<GameObject> students = new List<GameObject>();
    bool bTime;
    float oldTime;
    float endGameTime;
    GameObject classroom;
    int iDeadStudent = 0;
    bool end = false;
    
    public TextMeshProUGUI loose;
    public TextMeshProUGUI win;

    void ChangeClassroom()
    {
        int rand;
        do
        {
            rand = Random.Range(0, classrooms.Count);
        }while(classroom != null && classroom == classrooms[rand]);

        classroom = classrooms[rand];

        foreach(GameObject s in students)
        {
            if(s.GetComponent<MovementStudent>().dead == false)
                s.GetComponent<MovementStudent>().randomChair(classroom);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        bTime = true;
        oldTime = Time.time;
        loose.enabled = false;
        win.enabled = false;
        endGameTime = Time.time + 60;
    }

    IEnumerator time()
    {
        bTime = false;
        yield return new WaitWhile(() => classroom.transform.GetChild(0).GetChild(0).transform.position.x != classroom.transform.GetChild(0).transform.position.x);
        oldTime = Time.time + 5;
        bTime = true;
    }
    // Update is called once per frame
    void Update()
    {
        
        if (end == false)
        {
            if (endGameTime < Time.time)
            {
                end = true;
                win.enabled = true;
                Time.timeScale = 0;
            }
            if (bTime && oldTime < Time.time)
            {
                ChangeClassroom();
                StartCoroutine(time());
            }
             iDeadStudent = 0;
            foreach (GameObject s in students)
            {
                if (s.GetComponent<MovementStudent>().dead == true)
                {
                    iDeadStudent++;
                }
            }
            if(students.Count - 1  == iDeadStudent)
            {
                end = true;
                loose.enabled = true;
                Time.timeScale = 0;
            }
        }
    }
}
