using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum States
{
    Idle = 0,
    Running = 1,
    Jumping = 2,
    Falling = 3,
    Sliding = 4,
    Dead = 5,
    Walking = 6,
    Hurt = 7
}

public class Cat : MonoBehaviour
{
    public GameUI gameUI;
    public Rigidbody2D rig;
    public Animator anim;
    public BoxCollider2D boxCollider;
    public States state;
    public LayerMask groundLayer;
    float horizontalInput;

    float jumpVerticalPushOff = 10;
    Vector2 savedlocalScale;

    float horizonatlSpeed = 1;
    int lives = 3;
    int score = 0;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        savedlocalScale = transform.localScale;
        gameUI.HideAndShowEnd(false);
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        // what do the below lines do ?
        if (horizontalInput > 0.001f)
        {
            transform.localScale = new Vector2(savedlocalScale.x, savedlocalScale.y);
        }
        else if (horizontalInput < -0.001f)
        {
            transform.localScale = new Vector2(-savedlocalScale.x, savedlocalScale.y);
        }

        if (state == States.Idle)
        {
            if (IsGrounded())
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    rig.velocity = new Vector2(rig.velocity.x, jumpVerticalPushOff);
                    state = States.Jumping ;
                }
                else if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
                {
                    state = States.Walking;
                }
            }
        }

        if(state == States.Walking)
        {
            horizonatlSpeed = 1;

            if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                state = States.Idle;
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                state = States.Running;
            }
            else if( Input.GetKey(KeyCode.LeftControl))
            {
                state = States.Sliding;
            }
            else if (Input.GetKey(KeyCode.Space))
            {
                rig.velocity = new Vector2(rig.velocity.x, jumpVerticalPushOff);
                state = States.Jumping;
            }
        }
        else if (state == States.Running)
        {
            horizonatlSpeed = 5;
            if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) || !Input.GetKey(KeyCode.LeftShift))
            {
                state = States.Walking;
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                state = States.Sliding;
            }
            else if (Input.GetKey(KeyCode.Space))
            {
                rig.velocity = new Vector2(rig.velocity.x, jumpVerticalPushOff);
                state = States.Jumping;
            }
        }
        else if (state == States.Jumping)
        {
            if(rig.velocity.y < 0)
                state = States.Falling;
        }
        else if (state == States.Sliding)
        {
            if (!((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)) && Input.GetKey(KeyCode.LeftControl)))
            {
                state = States.Running;
            }
        }
        else if (state == States.Falling)
        {
            if (IsGrounded())
                state = States.Idle;
        }
        else if(state == States.Hurt)
        {
            state = States.Idle;
        }

        if (state != States.Dead)
        {
            rig.velocity = new Vector2(horizontalInput * horizonatlSpeed, rig.velocity.y);
        }else if (state == States.Dead)
        {
            StartCoroutine(End());
        }

        anim.SetInteger("State", (int)state);
    }

    IEnumerator End()
    {
        new WaitForSeconds(2);
        gameUI.HideAndShowEnd(true);
        yield return null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Box")
        {
            if (state == States.Sliding)
            {
                if( (collision.gameObject.GetComponent<Rigidbody2D>().velocity.x < 0 && rig.velocity.x < 0) || (collision.gameObject.GetComponent<Rigidbody2D>().velocity.x > 0 && rig.velocity.x > 0))
                {
                    score += 1;
                    gameUI.UpdateScore(score);
                }
                else
                {
                    if (lives > 0)
                        state = States.Hurt;
                    else
                        state = States.Dead;
                    lives--;
                    anim.SetInteger("State", (int)state);
                }
            }
            else
            {
                if (lives > 0)
                    state = States.Hurt;
                else
                    state = States.Dead;
                lives--;
                anim.SetInteger("State", (int)state);
            }
            gameUI.UpdateLives(lives);
            Destroy(collision.gameObject);
        }

    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
}
