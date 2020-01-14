using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpMultiplier;

    private bool isJumping;
    private float timeInAir;

    [SerializeField] private AnimationCurve jumpFalloff;

    CharacterController cc;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // FixedUpdate is called once per fixed length of time (use for physics)
    void FixedUpdate()
    {
        //get raw data
        float vertInput = Input.GetAxis("Vertical");
        float horzInput = Input.GetAxis("Horizontal");

        //move player in each direction
        Vector3 forwardMove = transform.forward * vertInput;
        Vector3 strafeMove = transform.right * horzInput;

        cc.SimpleMove((forwardMove + strafeMove) * speed);

        jumpInput();
    }


    void jumpInput()
    {
        //check for player input
        isJumping = Input.GetAxis("Jump") != 0 && !isJumping && cc.isGrounded;
        
        //evaluate upward movement based of jump curve
        if (isJumping)
        {
            float jumpForce = jumpMultiplier * jumpFalloff.Evaluate(timeInAir);
            
            cc.Move(Vector3.up * jumpForce);

            timeInAir += Time.deltaTime;
        }

        //check for upward obstruction or a floor below
        if (cc.isGrounded || cc.collisionFlags == CollisionFlags.Above)
        {
            isJumping = false;
            timeInAir = 0;
        }
    }
}
