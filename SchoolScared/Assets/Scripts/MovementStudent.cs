using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum States
{
    Idle = 0,
    Walk = 1,
    Run = 2,
    Dead = 3
}


enum Direction
{
    Forward,
    Backward
}

public class MovementStudent : MonoBehaviour
{
    NavMeshAgent agent;   
    public States state;
    public Animator anim;
    public bool dead = false;
    Direction direction;

    public void randomChair(GameObject classroom)
    {
        int rand;
        if (classroom.transform.GetChild(0).childCount == 0)
        {
            transform.parent = classroom.transform.GetChild(0);
            agent.SetDestination(classroom.transform.GetChild(0).position);
        }
        else
        {
            do
            {
                rand = Random.Range(0, classroom.transform.childCount);
            } while (classroom.transform.GetChild(rand).childCount > 0);
            transform.parent = classroom.transform.GetChild(rand);
            agent.SetDestination(classroom.transform.GetChild(rand).position);
        }
    }


    public void animStudent ()
    {
        if ( dead != true )
        {
            if (agent.velocity.x == 0 && agent.velocity.y == 0 && state != States.Idle)
            {
                state = States.Idle;
            }
            else if (agent.velocity.x != 0 || agent.velocity.y != 0 && state != States.Walk)
            {
                if(agent.velocity.x < 0 && direction == Direction.Forward)
                {
                    transform.Rotate(new Vector3(0, 180, 0));
                    direction = Direction.Backward;
                }
                else if (agent.velocity.x > 0 && direction == Direction.Backward)
                {
                    transform.Rotate(new Vector3(0, 0, 0));
                    direction = Direction.Forward;
                }
                state = States.Walk;
            }
        }
        else
            state = States.Dead;
       

        anim.SetInteger("State", (int)state);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if( col.gameObject.tag == "Zombie" )
        {
            dead = true;
            //agent.velocity = new Vector3 (0,0,0);
        }
    }

    void Update()
    {
        animStudent();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        direction = Direction.Forward;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
}
