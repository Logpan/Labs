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
    Transform player;



    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        currentWaypoint = waypoints[Random.Range(0, waypoints.Count)];
        agent.SetDestination(currentWaypoint.transform.position);
        player = GameObject.Find("Student").transform;
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

        Vector2 airplanetoGreenShipDir = player.position - transform.position;
        float theta = Mathf.Acos(Vector2.Dot(airplanelookDirection, airplanetoGreenShipDir) / (airplanelookDirection.magnitude * airplanetoGreenShipDir.magnitude));
        theta = theta * Mathf.Rad2Deg;
        //Debug.Log(theta);

        //float dotp = Vector2.Dot(airplanelookDirection, airplanetoGreenShipDir);
        if (theta < 90 / 2)
        {
            transform.GetChild(1).transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
            //Debug.DrawLine(transform.position, player.position, Color.red);
        }
        else
        {
            transform.GetChild(1).transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
            //Debug.DrawLine(transform.position, player.position, Color.green);
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckStudent();
        if(CheckPosition())
        {
           // agent.SetDestination(currentWaypoint.transform.position);
        }
        isFront();
    }

    bool isFront()
    {
        Vector2 directionOfPlayer = transform.position - player.position;
        float angle = Vector2.Angle(transform.forward, directionOfPlayer);
        Debug.Log(angle);
        Debug.Log(player.position);
        if(Mathf.Abs(angle) > 90 && Mathf.Abs(angle) < 270)
        {
            Debug.DrawLine(transform.position, player.position, Color.red);
            return true;
        }
        return false;
    }
}
