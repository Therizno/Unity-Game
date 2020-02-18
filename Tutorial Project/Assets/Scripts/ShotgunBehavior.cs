using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBehavior : MonoBehaviour, Observer
{
    private BulletFactory fac;

    [SerializeField] float bulletVelocity;

    // Start is called before the first frame update
    void Start()
    {
        fac = GetComponent<BulletFactory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void notify(GameEvent e)
    {
        if (e == GameEvent.FireWeapon)
        {
            fac.createBullet(transform.forward, bulletVelocity, 0);
        }
    }
}
