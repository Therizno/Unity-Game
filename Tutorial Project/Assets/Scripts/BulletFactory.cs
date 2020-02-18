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

    public void createBullet(Vector3 dir, float vel, float damage)
    {
        GameObject bullet = Instantiate(bulletPrefab);

        Rigidbody rgbd = bullet.GetComponent<Rigidbody>();
        rgbd.velocity = (dir * vel); 
    }
}
