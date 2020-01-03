using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController cc;

    public float speed;
    public float jumpSpeed;
    public float jumpTime;
    public float jumpInterval;

    private float coolDown;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();

        //prevent player from jumping when spawned
        coolDown = jumpTime;
    }

    // FixedUpdate is called once per fixed length of time (use for physics)
    void FixedUpdate()
    {
        coolDown++;

        float moveX = Input.GetAxis("Vertical") * -speed;
        float moveY = 0;
        float moveZ = Input.GetAxis("Horizontal") * speed;
        

        if (Input.GetAxis("Jump") != 0)
        {
            if (coolDown >= jumpInterval)
            {
                coolDown = 0;
            }
            
        }

        if (coolDown < jumpTime)
        {
            moveY += jumpSpeed;
        }

        cc.Move(Vector3.up * moveY);
        cc.Move(Vector3.right * moveX);
        cc.Move(Vector3.forward * moveZ);
        cc.SimpleMove(new Vector3(0, 0, 0));
    }
}
