using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.VersionControl.Asset;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public enum States
    {
        Idle = 0,
        Walk = 1,
        Jump = 2,
        Shoot = 3,
        Dig = 4,
    }

    public Rigidbody2D rig;
    public Animator anim;
    public CapsuleCollider2D capsuleCollider;
    public States state;
    public Vector3 respawnPoint;
    public LayerMask groundLayer;
    float horizontalInput;
    GameObject collided;
    float oldJump;
    float jumpVerticalPushOff = 5;
    Vector2 savedlocalScale;
    public Vector3 nextTileToBreak;
    KeyCode lastKeyPressed;
    
    
    public int lives = 3;
    public TextMeshProUGUI live;

    public GameObject goBullet;
    public TextMeshProUGUI Bullet;
    int iBullet;

    float horizonatlSpeed = 3;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        savedlocalScale = transform.localScale;
        respawnPoint = transform.localPosition;
        oldJump = jumpVerticalPushOff;
        iBullet = 0;
        Bullet.text = "Bullets : " + iBullet;
        live.text = "Lives : " + lives;
        state = States.Idle;
    }



    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput > 0.001f)
        {
            transform.localScale = new Vector2(savedlocalScale.x, savedlocalScale.y);
            state = States.Walk;
        }
        else if (horizontalInput < -0.001f)
        {
            transform.localScale = new Vector2(-savedlocalScale.x, savedlocalScale.y);
            state = States.Walk;
        }
        else
        {
            state = States.Idle;
        }

        CheckDirection();

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rig.velocity = new Vector2(rig.velocity.x, jumpVerticalPushOff);
            state = States.Jump;
        }
        else
        {
            rig.velocity = new Vector2(horizontalInput * horizonatlSpeed, rig.velocity.y);
        }

        Diggable();
        if (collided != null)
        {
            if (collided.name == "Diggable" && (Input.GetKeyDown(KeyCode.Q) || nextTileToBreak == Vector3.up + Vector3.up))
            {
                collided.GetComponent<Tilemap>().SetTile(collided.GetComponent<Tilemap>().WorldToCell(transform.position + nextTileToBreak), null);
                state = States.Dig;
            }
        }

        if(Input.GetKeyDown(KeyCode.F) && iBullet > 0)
        {
            Vector3 bulletPosition = new Vector3(gameObject.transform.position.x - transform.localScale.x + 0.1f, gameObject.transform.position.y + transform.localScale.y/2, 0);
            GameObject bullet = Instantiate(goBullet, bulletPosition, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2((int)-transform.localScale.x*5,0);
            Destroy(bullet, 2f);
            iBullet--;
            state = States.Shoot;
            Bullet.text = "Bullets : " + iBullet;
        }

        anim.SetInteger("State", (int)state);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "CheckPoint")
        {
            respawnPoint = transform.position;
        }
        else if(collision.gameObject.name == "Finish")
        {
            respawnPoint = new Vector3(0,0,0);
            GameObject.Find("_GameController").GetComponent<GameController>().NextLevel();
        }
        else if (collision.gameObject.name == "Crates")
        {
            collision.GetComponent<Tilemap>().SetTile(collided.GetComponent<Tilemap>().WorldToCell(transform.position + nextTileToBreak), null);
            iBullet += 5;
            Bullet.text = "Bullets : " + iBullet;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            transform.position = respawnPoint;
            lives--;
            live.text = "Lives : " + lives;
            if (lives == 0)
            {
                GameObject.Find("_GameController").GetComponent<GameController>().Loose();
            }    
        }
    }

    private void Diggable()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(capsuleCollider.bounds.center, capsuleCollider.bounds.size, 0, nextTileToBreak, 0.1f, groundLayer);
        if (raycastHit.collider != null)
        {
            collided = raycastHit.collider.gameObject;
            //Debug.Log(raycastHit.collider.isTrigger);
        }
        else
            collided = null;
    }


}
