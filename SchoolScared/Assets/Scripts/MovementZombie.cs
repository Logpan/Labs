using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementZombie : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    NavMeshAgent agent;
    public List<GameObject> waypoints;
    GameObject currentWaypoint;
    public GameObject dirMarker;
    public GameObject dirMarker2;



    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        currentWaypoint = waypoints[Random.Range(0, waypoints.Count)];
        agent.SetDestination(currentWaypoint.transform.position);
    }

    bool CheckPosition()
    {
        if(agent.velocity.x == 0)
        {
            currentWaypoint = waypoints[Random.Range(0, waypoints.Count)];
            return true;
        }
        return false;
    }

    bool CheckStudent()
    {
       /* Vector2 a = new Vector2(-3, 5);
        Vector2 b = new Vector2(10, 1);

        float dotProduct = Vector2.Dot(a, b);*/

        

        Vector2 airplanelookDirection = dirMarker.transform.position - transform.position;

        Vector2 airplanetoGreenShipDir = dirMarker2.transform.position - transform.position;
        float theta = Mathf.Acos(Vector2.Dot(airplanelookDirection, airplanetoGreenShipDir) / (airplanelookDirection.magnitude * airplanetoGreenShipDir.magnitude));
        theta = theta * Mathf.Rad2Deg;
       
        //float dotp = Vector2.Dot(airplanelookDirection, airplanetoGreenShipDir);
        if (theta < 90 / 2)
        {
            transform.GetChild(1).transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
        }
        else
        {
            transform.GetChild(1).transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckStudent();
        if(CheckPosition())
        {
            agent.SetDestination(currentWaypoint.transform.position);
        }
    }
}
