using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private bool plyrTeam;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        HitboxBehavior hb = other.gameObject.GetComponent<HitboxBehavior>();

        if (hb != null && plyrTeam != hb.playerTeam())
        {
            hb.damageParent(damage);
        }
    }
}
