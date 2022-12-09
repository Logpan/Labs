using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementStudent : MonoBehaviour
{
    NavMeshAgent agent;   
    
    public void randomChair(GameObject classroom)
    {
        int rand;
        do
        {
            rand = Random.Range(0, classroom.transform.childCount);
        } while (classroom.transform.GetChild(rand).childCount > 0);
        transform.parent = classroom.transform.GetChild(rand);
        agent.SetDestination(classroom.transform.GetChild(rand).position);
    }

    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
}
