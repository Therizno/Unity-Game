using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    GameManager gm;

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
    }
}
