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
    Direction direction;

    enum Direction
    {
        Forward,
        Backward
    }
    // Start is called before the first frame update
    void Start()
    {
        direction = Direction.Forward;
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody2D>();
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

    //bool CheckStudent()
    //{
    //   /* Vector2 a = new Vector2(-3, 5);
    //    Vector2 b = new Vector2(10, 1);

    //    float dotProduct = Vector2.Dot(a, b);*/

        

    //    Vector2 airplanelookDirection = dirMarker.transform.position - transform.position;

    //    Vector2 airplanetoGreenShipDir = player.position - transform.position;
    //    float theta = Mathf.Acos(Vector2.Dot(airplanelookDirection, airplanetoGreenShipDir) / (airplanelookDirection.magnitude * airplanetoGreenShipDir.magnitude));
    //    theta = theta * Mathf.Rad2Deg;
    //    //Debug.Log(theta);

    //    //float dotp = Vector2.Dot(airplanelookDirection, airplanetoGreenShipDir);
    //    if (theta < 90 / 2)
    //    {
    //        transform.GetChild(1).transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
    //        //Debug.DrawLine(transform.position, player.position, Color.red);
    //    }
    //    else
    //    {
    //        transform.GetChild(1).transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
    //        //Debug.DrawLine(transform.position, player.position, Color.green);
    //    }
    //    return false;
    //}

    // Update is called once per frame
    void Update()
    {
        if (agent.velocity.x < 0 && direction == Direction.Forward)
        {
            transform.Rotate(new Vector3(0,180,0));
            direction = Direction.Backward;
        }
        else if (agent.velocity.x > 0 && direction == Direction.Backward)
        {
            transform.Rotate(new Vector3(0, 180, 0));
            direction = Direction.Forward;
        }
        //CheckStudent();
        if (CheckPosition())
        {
           agent.SetDestination(currentWaypoint.transform.position);
        }
        isFront();
    }

    bool isFront()
    {
        Vector2 directionOfPlayer = transform.position - player.position;
        float angle = Vector2.Angle(transform.up, directionOfPlayer);

        //Debug.Log(angle);
        if((directionOfPlayer.x < 0 || direction == Direction.Backward) && (directionOfPlayer.x > 0 || direction == Direction.Forward))
            if (Mathf.Abs(angle) > 90 && Mathf.Abs(angle) < 120)
            {
                Debug.DrawLine(transform.position, player.position, Color.red);
                return true;
            }
        return false;
    }
}