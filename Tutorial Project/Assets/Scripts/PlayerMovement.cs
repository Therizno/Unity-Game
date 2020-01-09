using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;

    CharacterController cc;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // FixedUpdate is called once per fixed length of time (use for physics)
    void FixedUpdate()
    {
        float vertInput = Input.GetAxis("Vertical");
        float horzInput = Input.GetAxis("Horizontal");

        Vector3 forwardMove = transform.forward * vertInput;
        Vector3 strafeMove = transform.right * horzInput;

        cc.SimpleMove((forwardMove + strafeMove) * speed);
    }
}
