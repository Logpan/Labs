using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;




public class MovementZombie : MonoBehaviour
{
    enum States
    {
        Walk = 0,
        Eat = 1
    }

    Rigidbody2D rb;
    public float speed;
    NavMeshAgent agent;
    public List<GameObject> waypoints;
    GameObject currentWaypoint;
    public GameObject dirMarker;
    public GameObject dirMarker2;
    public List<GameObject> players = new List<GameObject>();
    Direction direction;
    States state;
    public Animator anim;
    
    

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
        //player = GameObject.Find("Student").transform;
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

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Student")
        {
            state = States.Eat;
            anim.SetInteger("State", (int)state);
        }
            
    }

    bool isFront()
    {
        foreach(GameObject player in players)
        {
            Vector2 directionOfPlayer = transform.position - player.transform.position;
            float angle = Vector2.Angle(transform.up, directionOfPlayer);

            //Debug.Log(directionOfPlayer.x);
            if ((directionOfPlayer.x < 0 || direction == Direction.Backward) && (directionOfPlayer.x > 0 || direction == Direction.Forward))
            {
                if (Mathf.Abs(angle) > 90 && Mathf.Abs(angle) < 120 && directionOfPlayer.x > -4 && directionOfPlayer.x < 4)
                {
                    agent.SetDestination(player.transform.position);
                    Debug.DrawLine(transform.position, player.transform.position, Color.red);
                    return true;

                }
            }
        }
        return false;
    }
} 