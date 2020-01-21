﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    GameManager gm;

    [SerializeField] private float walkAnimSpeed;
    [SerializeField] private float aimWalkSpeedMultiplier;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.getInstance();

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("movement", gm.getPlayerMoveSpeed() != 0);
        anim.SetFloat("speed", gm.getPlayerMomentum() * walkAnimSpeed);
        anim.SetBool("sprinting", gm.getPlayerSprinting());
        anim.SetBool("jump", gm.getPlayerJumping());
        anim.SetBool("aim", gm.getPlayerAiming());
        anim.SetFloat("aim walk speed", gm.getPlayerMomentum() * aimWalkSpeedMultiplier);
    }
}
