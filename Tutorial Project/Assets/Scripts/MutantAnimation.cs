using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantAnimation : MonoBehaviour
{
    //Animation speed multiplier
    [SerializeField] private float moveAnimSpeed;

    private Animator anim;

    private float curSpeed;
    private bool isAttack;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>(); 
    }


    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("move speed", curSpeed * moveAnimSpeed);
        anim.SetBool("moving", curSpeed != 0);
        anim.SetBool("attack", isAttack);

        isAttack = false;
    }


    public void setMoveSpeed(float speed)
    {
        curSpeed = speed;
    }

    public void isAttacking(bool attack)
    {
        isAttack = attack;
    }
}
