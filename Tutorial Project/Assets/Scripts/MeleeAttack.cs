using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private bool plyrTeam;
    [SerializeField] private float cooldownTime;

    private float timeSinceAttack;

    private bool isAttacking;   //get this information from the parent


    void Awake()
    {
        timeSinceAttack = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceAttack += Time.deltaTime;

        //check to see if we missed any hitboxes, and hit them if needed
    }


    /*
     *  deal damage if collides with a hitbox
     */
    private void OnTriggerEnter(Collider other)
    {
        HitboxBehavior hitb = other.gameObject.GetComponent<HitboxBehavior>();

        dealDamage(hitb);
    }


    private void dealDamage(HitboxBehavior hb)
    {
        if (isAttacking && timeSinceAttack > cooldownTime && hb != null && plyrTeam != hb.playerTeam())
        {
            hb.damageParent(damage);

            //reset cooldown
            timeSinceAttack = 0;
        }
    }
}
