using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.VersionControl.Asset;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rig;
    //public Animator anim;
    public BoxCollider2D capsuleCollider;
    //public States state;
    public LayerMask groundLayer;
    float horizontalInput;

    float jumpVerticalPushOff = 5;
    Vector2 savedlocalScale;

    float horizonatlSpeed = 3;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<BoxCollider2D>();
        savedlocalScale = transform.localScale;
    }



    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput > 0.001f)
        {
            transform.localScale = new Vector2(savedlocalScale.x, savedlocalScale.y);
        }
        else if (horizontalInput < -0.001f)
        {
            transform.localScale = new Vector2(-savedlocalScale.x, savedlocalScale.y);
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rig.velocity = new Vector2(rig.velocity.x, jumpVerticalPushOff);
        }
        else
        {
            rig.velocity = new Vector2(horizontalInput * horizonatlSpeed, rig.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Diggable")
        {
            collision.gameObject.GetComponent<Tilemap>().SetTile(collision.gameObject.GetComponent<Tilemap>().WorldToCell(gameObject.transform.position), null);
        }
        if (!IsGrounded())
        {

            Debug.Log("Touch");
            StartCoroutine(Fall());
        }
    }


    IEnumerator Fall()
    {
        float temp = horizonatlSpeed;
        horizonatlSpeed = 0;
        yield return new WaitUntil(()=> IsGrounded());
        horizonatlSpeed = temp;
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(capsuleCollider.bounds.center, capsuleCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }


}
