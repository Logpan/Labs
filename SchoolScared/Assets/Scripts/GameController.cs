using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<GameObject> classrooms = new List<GameObject>();
    public List<GameObject> students = new List<GameObject>();
    bool bTime;
    float oldTime;
    GameObject classroom;

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
            s.GetComponent<MovementStudent>().randomChair(classroom);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        bTime = true;
        oldTime = Time.time;
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
        if (bTime && oldTime < Time.time)
        {
            ChangeClassroom();
            StartCoroutine(time());
        }
    }
}
