using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createBullet(Vector3 dir, float vel, float size, int damage)
    {
        //create the bullet
        GameObject bullet = Instantiate(bulletPrefab);
        BulletBehavior bbh = bullet.GetComponent<BulletBehavior>();

        //give it velocity
        bbh.setSpeed(vel);

        //rotate it
        bullet.transform.rotation = Quaternion.FromToRotation(bullet.transform.up, dir);

        //position it
        bullet.transform.position = transform.position;

        //scale it
        bbh.setSize(size);

        //set the damage
        bbh.setDamage(damage);
    }
}
