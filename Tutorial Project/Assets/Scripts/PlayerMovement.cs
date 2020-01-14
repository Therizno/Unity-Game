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

        //evaluate upward movement based of jump curve
        if (isJumping)
        {
            //avoid glitchy collisions while jumping
            cc.slopeLimit = 90.0f;

            //calculate forces and move player
            float jumpForce = jumpMultiplier * jumpFalloff.Evaluate(timeInAir);

            cc.Move(Vector3.up * jumpForce * Time.deltaTime);


            //this line prevents the isGrounded check from working incorrectly
            cc.SimpleMove(new Vector3(0,0,0));

            //check for upward obstruction or a floor below
            isJumping = !cc.isGrounded && cc.collisionFlags != CollisionFlags.Above;


            timeInAir += Time.deltaTime;
        }
        else
        {
            timeInAir = 0;

            //reset slopeLimit
            cc.slopeLimit = 45.0f;

            //check for player jump input
            isJumping = (Input.GetAxis("Jump") != 0 && cc.isGrounded);
        }
        
    }
}
