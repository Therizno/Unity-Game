using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //make cursor invisible
        Cursor.lockState = CursorLockMode.Locked;

        //keep the player from falling over
        rb.freezeRotation = true;
    }

    // FixedUpdate is called once per fixed length of time (use for physics)
    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal") * speed;
        float moveZ = Input.GetAxis("Vertical") * speed;

        transform.Translate(moveX, 0, moveZ);

        /*
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = transform.right * -speed;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = transform.right * speed;
        }*/
    }
}
