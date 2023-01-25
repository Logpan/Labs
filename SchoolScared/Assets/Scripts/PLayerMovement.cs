using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PLayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 1;
    private float movementX;
    private float movementY;
    public GameObject Bullet;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnFire()
    {
        Vector3 bulletPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + transform.localScale.x + 0.1f, 0);
        GameObject bullet = Instantiate(Bullet, bulletPosition, Quaternion.identity);
        Destroy(bullet, 2f);
    }

        void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(movementX, movementY);
        rb.MovePosition(transform.position + movement * speed * Time.fixedDeltaTime);
    }
}
