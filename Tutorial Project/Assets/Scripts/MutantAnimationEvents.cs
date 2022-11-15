using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantAnimationEvents : MonoBehaviour
{
    private bool attacking;


    // Awake is called before start
    void Awake()
    {
        attacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public bool isAttacking()
    {
        return attacking;
    }


    // any of these functions can be triggered by an animation

    public void startAttack()
    {
        attacking = true;
    }

    public void endAttack()
    {
        attacking = false;
    }
}
