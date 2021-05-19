using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxBehavior : MonoBehaviour
{
    [SerializeField] private GameObject parentObject;

    [SerializeField] private float damageMultiplier;

    private Damageable parentObj;


    // Start is called before the first frame update
    void Start()
    {
        parentObj = parentObject.GetComponent<Damageable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void damageParent(int damage)
    {
        parentObj.takeDamage((int)(damage * damageMultiplier));
    }

    public void healParent(int health)
    {
        parentObj.heal(health);
    }

    public bool playerTeam()
    {
        return parentObj.playerTeam();
    }
}
