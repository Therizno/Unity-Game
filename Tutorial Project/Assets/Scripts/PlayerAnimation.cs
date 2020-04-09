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

    //used for shotgun only
    private uint gunCapacityLeft;
    private bool loadedShell;


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

        //relevant to shotgun only
        anim.SetFloat("shells left", ((float)gunCapacityLeft));


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

        //relevant to shotgun only
        repeatReload();
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


    //relevant only when shotgun equipped
    public void repeatReload()
    {
        AnimatorTransitionInfo itf = anim.GetAnimatorTransitionInfo(0);

        if (itf.IsName("Base Layer.Reload -> Base Layer.Reload") || itf.IsName("Reload -> End_Reload") || itf.IsName("End_Reload -> Idle") && gunCapacityLeft > 0)
        {
            if (anim.IsInTransition(0))
            {
                if (!loadedShell)
                {
                    gunCapacityLeft -= 1;
                    loadedShell = true;
                }
            }
            else
            {
                loadedShell = false;
            }
        }
    }


    //getters and setters
    public void setGunCapacityLeft(int shells)
    {
        if (shells > 0)
            gunCapacityLeft = (uint)shells;
    }

    public int updatedGunCapacityLeft()
    {
        return (int)gunCapacityLeft;
    }
}
