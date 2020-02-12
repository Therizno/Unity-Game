using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour, Observer
{
    private GameManager gm;

    //animation speed variables. Control animations only
    [SerializeField] private float walkAnimSpeed;
    [SerializeField] private float sprintAnimSpeed;
    [SerializeField] private float aimWalkSpeedMultiplier;

    private Animator anim;

    private bool fire;

    int i = 0;
    // Start is called before the first frame update (use for getting other objects)
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


        if (fire)
        {
            Debug.Log(i);
            anim.SetBool("fire", true);
            fire = false;
            i++;
        }
        else
        {
            anim.SetBool("fire", false); 
        }
    }


    //Observer methods

    public void notify(GameEvent g)
    {
        if (g == GameEvent.FireWeapon)
        {
            fire = true;
        }
    }



    public bool isFiringAnimated()
    {
        AnimatorStateInfo inf = anim.GetCurrentAnimatorStateInfo(0);
        return inf.IsName("Shot") || inf.IsName("Aiming_Shot");
    }
}
