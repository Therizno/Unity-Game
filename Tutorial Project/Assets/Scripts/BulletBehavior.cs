using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private float damage;

    // Start is called before the first frame update (use for getting other objects)
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }


    public void setDamage(float dmg)
    {
        damage = dmg;
    }
}
