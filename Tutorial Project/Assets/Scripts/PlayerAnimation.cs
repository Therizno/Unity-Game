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
    private bool reload;


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
        

        //pass certain variables once when booleans are triggered

        if (fire)
        {
            anim.SetBool("fire", true);
            fire = false;
        }
        else
        {
            anim.SetBool("fire", false);
        }

        if (reload)
        {
            anim.SetBool("reload", true);
            reload = false;
        }
        else
        {
            anim.SetBool("reload", false);
        }
    }


    //Observer methods

    public void notify(GameEvent g)
    {
        if (g == GameEvent.FireWeapon)
        {
            fire = true;
        }
        else if (g == GameEvent.ReloadWeapon)
        {
            reload = true;
        }
    }


    public bool isFireReady()
    {
        AnimatorStateInfo inf = anim.GetCurrentAnimatorStateInfo(0);
        return !inf.IsName("Jump") && !inf.IsName("Run") && !isReloading();
    }

    public bool isFiring()
    {
        AnimatorStateInfo inf = anim.GetCurrentAnimatorStateInfo(0);
        return inf.IsName("Shot") || inf.IsName("Aiming_Shot");
    }

    public bool isReloading()
    {
        AnimatorStateInfo inf = anim.GetCurrentAnimatorStateInfo(0);
        return inf.IsName("Begin_Reload") || inf.IsName("Reload") || inf.IsName("End_Reload");
    }



    //used for shotgun only
    public void setEmptyShellCapacity(float numShells)
    {
        anim.SetFloat("shells left", numShells);
    }

} 
