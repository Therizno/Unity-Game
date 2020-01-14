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
            float jumpForce = jumpMultiplier * jumpFalloff.Evaluate(timeInAir);

            cc.Move(Vector3.up * jumpForce * Time.deltaTime);
            Debug.Log(cc.isGrounded ? "Grounded" : "Not grounded");
            //check for upward obstruction or a floor below (janky fix)
            isJumping = jumpForce != 0 && cc.collisionFlags != CollisionFlags.Above;
            Debug.Log((Vector3.up * jumpForce * Time.deltaTime).y);
            timeInAir += Time.deltaTime;
        }
        else
        {
            timeInAir = 0;
            Debug.Log(cc.isGrounded ? "Grounded" : "Not grounded");
            //check for player jump input
            isJumping = (Input.GetAxis("Jump") != 0 && cc.isGrounded);
        }

        Debug.Log(isJumping ? "Jumping" : "Not jumping");
        
    }
}
