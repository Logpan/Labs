using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    //Rigidbody2D rb;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    rb = GetComponent<Rigidbody2D>();
    //    rb.velocity = new Vector2(5,0);
    //    rb.rotation = 35;
    //}

    //public void setVelocity(int vel)
    //{
    //    rb.velocity = new Vector2(vel, 0);
    //}

    void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(this.gameObject);
    }
}
