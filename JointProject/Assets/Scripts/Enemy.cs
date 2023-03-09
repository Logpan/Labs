using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    enum State
    {
        Idle = 0,
        Walk = 1,
        Sleep = 2,
    }
    Rigidbody2D rb;
    int velocity;
    Animator anim;
    State states;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        velocity = (int)transform.localScale.x;
        states = State.Walk;
        anim.SetInteger("State", (int)states);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider);
        if (collision.gameObject.tag == "Bullet")
        {
            StartCoroutine(sleep());
        }
        else if (collision.gameObject.tag != "Player" )
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            StartCoroutine(sleep());
        }
    }

    IEnumerator sleep()
    {
        states = State.Sleep;
        anim.SetInteger("State", (int)states);
        velocity = 0;
        yield return new WaitForSeconds(5);
        states = State.Walk;
        anim.SetInteger("State", (int)states);
        velocity = (int)transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(velocity, 0);
    }
}
