using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private GameObject parentObject;

    [SerializeField] private int damage;
    [SerializeField] private float cooldownTime;

    private MeleeAttacker parent;

    private Rigidbody rb;

    private Vector3 oldPosition;
    private float timeSinceAttack;

    void Awake()
    {
        timeSinceAttack = 0;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        parent = parentObject.GetComponent<MeleeAttacker>();

        oldPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceAttack += Time.deltaTime;

        /*
         * check to see if we missed any hitboxes, and hit them if needed
         */
        if (parent.isAttacking())
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
        if (parent.isAttacking() && timeSinceAttack > cooldownTime && hb != null && parent.playerTeam() != hb.playerTeam())
        {
            hb.damageParent(damage);

            //reset cooldown
            timeSinceAttack = 0;
        }
    }
}
