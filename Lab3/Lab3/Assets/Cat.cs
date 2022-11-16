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
    Walking = 6
}

public class Cat : MonoBehaviour
{
    public Rigidbody2D rig;
    public Animator anim;
    public BoxCollider2D boxCollider;
    public States state;
    public LayerMask groundLayer;
    float horizontalInput;

    float jumpVerticalPushOff = 1;
    Vector2 savedlocalScale;

    float horizonatlSpeed = 1;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        savedlocalScale = transform.localScale;
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
                    rig.velocity = new Vector2(rig.velocity.x, 10);
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
                rig.velocity = new Vector2(rig.velocity.x, 10);
                state = States.Jumping;
            }
        }
        else if (state == States.Running)
        {
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
                rig.velocity = new Vector2(rig.velocity.x, 10);
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

        else if (state == States.Dead)
        {

        }

        rig.velocity = new Vector2(horizontalInput * horizonatlSpeed, rig.velocity.y);

        anim.SetInteger("State", (int)state);
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
}
