using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private bool plyrTeam;
    [SerializeField] private float cooldownTime;

    private float timeSinceAttack;

    private bool isAttacking = true;   //get this information from the parent (WIP)


    private Rigidbody rb;

    private Vector3 oldPosition;


    void Awake()
    {
        timeSinceAttack = 0;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        oldPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceAttack += Time.deltaTime;

        /*
         * check to see if we missed any hitboxes, and hit them if needed
         */
        if (isAttacking)
        {
            // a vector from our current position to our old one
            Vector3 route = oldPosition - transform.position;

            RaycastHit hit;             //ignore triggers, those are for bullet collision only
            if (rb.SweepTest(route, out hit, route.magnitude, QueryTriggerInteraction.Ignore))
            {
                dealDamage(hit.collider.gameObject.GetComponent<HitboxBehavior>());
            }
        }


        oldPosition = transform.position;
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
