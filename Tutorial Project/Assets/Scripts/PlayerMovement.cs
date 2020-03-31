using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float baseSpeed;
    [SerializeField] private float sprintMultiplier;

    [SerializeField] private float jumpSpeed;
    [SerializeField] private float jumpCooldown;

    [SerializeField] private float aimMultiplier;

    [SerializeField] private float crouchSpeed;
    [SerializeField] private float crouchHeight;


    [SerializeField] private AnimationCurve jumpFalloff;

    [SerializeField] private GameObject cameraObject;


    private float moveSpeed;

    private bool jumping;
    private float timeInAir;
    private float timeSinceLand;

    private bool aiming;
    private bool aimingToggleable;

    private bool crouching;
    private float originalHeight;
    private float lastLerpValue;


    private CharacterController cc;


    //Start is called right after Awake, and is for interaction with other objects
    void Start()
    {
        cc = GetComponent<CharacterController>();
        originalHeight = cc.height;
        lastLerpValue = originalHeight;
    }

    // FixedUpdate is called once per fixed length of time (use for physics)
    void FixedUpdate()
    {
        checkAiming();


        moveInput();

        jumpInput();

        crouchInput();
    }


    private void moveInput()
    {
        //get raw data
        float vertInput = Input.GetAxis("Vertical");
        float horzInput = Input.GetAxis("Horizontal");

        //move player in each direction
        Vector3 forwardMove = transform.forward * vertInput;
        Vector3 strafeMove = transform.right * horzInput;

        Vector3 move = Vector3.ClampMagnitude(forwardMove + strafeMove, 1.0f) * baseSpeed * getSpeedModifiers();

        moveSpeed = Vector3.Magnitude(move);

        cc.SimpleMove(move);
    }


    private void jumpInput()
    {

        //evaluate upward movement based on jump curve
        if (jumping)
        {
            timeSinceLand = 0;

            timeInAir += Time.deltaTime;

            //avoid glitchy collisions while jumping
            cc.slopeLimit = 90.0f;

            //calculate forces and move player
            float jumpForce = jumpSpeed * jumpFalloff.Evaluate(timeInAir);

            cc.Move(Vector3.up * jumpForce * Time.deltaTime);


            //this line prevents the isGrounded check from working incorrectly
            cc.SimpleMove(Vector3.zero);

            //check for upward obstruction or a floor below
            jumping = !cc.isGrounded && (cc.collisionFlags & CollisionFlags.Above) == 0;

        }
        else
        {
            timeInAir = 0;

            timeSinceLand += Time.deltaTime;

            //reset slopeLimit
            cc.slopeLimit = 45.0f;

            //check for player jump input
            jumping = Input.GetAxis("Jump") != 0 && cc.isGrounded && !isSprinting() && timeSinceLand > jumpCooldown;
        }
        
    }


    private void crouchInput()
    {
        if (Input.GetAxis("Crouch") != 0)
        {
            cc.height = Mathf.Lerp(cc.height, crouchHeight, crouchSpeed);

            float dHeight = cc.height - lastLerpValue;

            cameraObject.transform.position = new Vector3(cameraObject.transform.position.x, cameraObject.transform.position.y + ((cameraObject.transform.position.y / cc.height/2) * (dHeight/2)), cameraObject.transform.position.z);
            transform.position = new Vector3(transform.position.x, transform.position.y + (dHeight / 2), transform.position.z);

            lastLerpValue = cc.height;
        }
        else
        {
            cc.height = Mathf.Lerp(cc.height, originalHeight, crouchSpeed);

            float dHeight = cc.height - lastLerpValue;

            cameraObject.transform.position = new Vector3(cameraObject.transform.position.x, cameraObject.transform.position.y + ((cameraObject.transform.position.y / cc.height/2) * (dHeight/2)), cameraObject.transform.position.z);

            lastLerpValue = cc.height;
        }
    }


    //checks if the player should be aiming 
    private void checkAiming()
    {
        //check for aim key
        if (Input.GetButton("Fire2"))
        {
            if (aimingToggleable)
            {
                aimingToggleable = false;
                aiming = !aiming;
            }
        }
        else
        {
            aimingToggleable = true;
        }

        // cancel aiming if the player decides to sprint 
        if (isSprinting())
        {
            aiming = false;
            aimingToggleable = true;
        }

        // stop player from aiming if currently jumping
        if (isJumping())
        {
            aiming = false;
            aimingToggleable = false;
        }
    }



    //returns any modifiers to player speed
    private float getSpeedModifiers()
    {
        return (isSprinting() ? sprintMultiplier : 1.0f) * (isAiming() ? aimMultiplier : 1.0f);
    }


    // getters and setters

    public bool isJumping()
    {
        return jumping;
    }

    public bool isSprinting()
    {
        return Input.GetAxis("Sprint") != 0 && Input.GetAxis("Vertical") > 0 && (cc.collisionFlags & CollisionFlags.Sides) == 0;
    }

    public bool isAiming()
    {
        return aiming;
    }

    public float getCurrentMoveSpeed()
    {
        return moveSpeed;
    }

    public float getPlayerMomentum()
    {
        return Vector3.Magnitude(cc.velocity);
    }
}
