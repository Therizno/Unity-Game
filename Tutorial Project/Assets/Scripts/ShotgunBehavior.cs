using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBehavior : MonoBehaviour, Observer
{
    private BulletFactory fac;

    [SerializeField] private float bulletVelocity;
    [SerializeField] private float bulletSize;
    [SerializeField] private float numBullets;
    [SerializeField] private float maxBulletSpread;
    [SerializeField] private float bulletDamage;

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
            for (int i = 0; i < numBullets; i++)
            {
                switch (i % 4)
                {
                    //distribute bullets between each quadrant 
                    case 0:
                        fac.createBullet(transform.forward + (transform.right * Random.Range(0.0f, maxBulletSpread)) + (transform.up * Random.Range(0.0f, maxBulletSpread)), bulletVelocity, bulletSize, bulletDamage);
                        break;

                    case 1:
                        fac.createBullet(transform.forward + (-transform.right * Random.Range(0.0f, maxBulletSpread)) + (transform.up * Random.Range(0.0f, maxBulletSpread)), bulletVelocity, bulletSize, bulletDamage);
                        break;

                    case 2:
                        fac.createBullet(transform.forward + (transform.right * Random.Range(0.0f, maxBulletSpread)) + (-transform.up * Random.Range(0.0f, maxBulletSpread)), bulletVelocity, bulletSize, bulletDamage);
                        break;

                    case 3:
                        fac.createBullet(transform.forward + (-transform.right * Random.Range(0.0f, maxBulletSpread)) + (-transform.up * Random.Range(0.0f, maxBulletSpread)), bulletVelocity, bulletSize, bulletDamage);
                        break;
                }
            }
        }
    }
}
