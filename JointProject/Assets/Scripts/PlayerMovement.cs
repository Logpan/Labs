using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.VersionControl.Asset;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rig;
    //public Animator anim;
    public CapsuleCollider2D capsuleCollider;
    //public States state;
    Vector3 respawnPoint;
    public LayerMask groundLayer;
    float horizontalInput;
    GameObject collided;
    float jumpVerticalPushOff = 5;
    Vector2 savedlocalScale;
    public Vector3 nextTileToBreak;
    KeyCode lastKeyPressed;

    float horizonatlSpeed = 3;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        savedlocalScale = transform.localScale;
        respawnPoint = transform.localPosition;
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

        CheckDirection();

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rig.velocity = new Vector2(rig.velocity.x, jumpVerticalPushOff);
        }
        else
        {
            rig.velocity = new Vector2(horizontalInput * horizonatlSpeed, rig.velocity.y);
        }

        Diggable();
        if (collided != null)
        {
            if (collided.name == "CheckPoint")
            {
                respawnPoint = transform.position;
            }

            
            //if (IsGrounded())
            //{
            //    if (Input.GetKeyDown(KeyCode.Q))
            //    {
            //        Debug.Log("Q press");
            //    }

                if (collided.name == "Diggable" && (Input.GetKeyDown(KeyCode.Q) || nextTileToBreak == Vector3.up + Vector3.up))
                {
                    collided.GetComponent<Tilemap>().SetTile(collided.GetComponent<Tilemap>().WorldToCell(transform.position + nextTileToBreak), null);
                }
            //}
        }
    }

    void CheckDirection()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            nextTileToBreak = Vector2.right;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            nextTileToBreak = Vector2.left;
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            nextTileToBreak = Vector2.up + Vector2.up;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            nextTileToBreak = Vector2.down;
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

    private void Diggable()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(capsuleCollider.bounds.center, capsuleCollider.bounds.size, 0, nextTileToBreak, 0.1f, groundLayer);
        if (raycastHit.collider != null)
        {
            collided = raycastHit.collider.gameObject;
            Debug.Log(raycastHit.collider.isTrigger);
        }
        else
            collided = null;
    }


}
