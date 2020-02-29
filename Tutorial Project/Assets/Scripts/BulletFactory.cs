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

    public void createBullet(Vector3 dir, float vel, float size, float damage)
    {
        //create the bullet
        GameObject bullet = Instantiate(bulletPrefab);

        //give it velocity
        Rigidbody rgbd = bullet.GetComponent<Rigidbody>();
        rgbd.velocity = (dir * vel);

        //position it
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;

        bullet.transform.Rotate(90, 0, 0);

        bullet.transform.localScale = new Vector3(size, size, size);


        //set the damage
        bullet.GetComponent<BulletBehavior>().setDamage(damage);
    }
}
