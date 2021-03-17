using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private bool plyrTeam;
    [SerializeField] private float cooldownTime;

    private float timeSinceAttack;


    void Awake()
    {
        timeSinceAttack = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceAttack += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        HitboxBehavior hb = other.gameObject.GetComponent<HitboxBehavior>();

        if (hb != null && timeSinceAttack > cooldownTime && plyrTeam != hb.playerTeam())
        {
            hb.damageParent(damage);

            //reset cooldown
            timeSinceAttack = 0;
        }
    }
}
