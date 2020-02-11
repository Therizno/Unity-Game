using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    GameManager gm;

    //animation speed variables. Control animations only
    [SerializeField] private float walkAnimSpeed;
    [SerializeField] private float sprintAnimSpeed;
    [SerializeField] private float aimWalkSpeedMultiplier;

    private Animator anim;

    private AnimationClip currentClip;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.getInstance();

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //pass variables to the animator 
        anim.SetBool("movement", gm.getPlayerMoveSpeed() != 0);
        anim.SetFloat("speed", gm.getPlayerMomentum() * walkAnimSpeed);
        anim.SetFloat("sprint speed", gm.getPlayerMomentum() * sprintAnimSpeed);
        anim.SetBool("sprinting", gm.getPlayerSprinting());
        anim.SetBool("jump", gm.getPlayerJumping());
        anim.SetBool("aim", gm.getPlayerAiming());
        anim.SetFloat("aim walk speed", gm.getPlayerMomentum() * aimWalkSpeedMultiplier);

        // get the current state of the animator
        if (anim.GetCurrentAnimatorClipInfo(0).Length > 0)
        {
            currentClip = anim.GetCurrentAnimatorClipInfo(0)[0].clip;
            Debug.Log(currentClip.name);
        }
    }
}
