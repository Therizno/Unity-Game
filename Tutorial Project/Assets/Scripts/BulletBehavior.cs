using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private float damage;

    [SerializeField] private GameObject bulletHole1;
    [SerializeField] private GameObject bulletHole2;

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
        //Raycast to find the normal of the surface of the hit at the point of contact

        RaycastHit hitInfo;

        if (Physics.Raycast(transform.position - transform.forward, transform.forward, out hitInfo, Mathf.Infinity))
        {
            // instantiate the bullet hole

            GameObject hole;

            if (Random.Range(0, 2) == 0)
            {
                hole = Instantiate(bulletHole1);
            }
            else
            {
                hole = Instantiate(bulletHole2);
            }


            hole.transform.position = hitInfo.point;
            hole.transform.rotation = Quaternion.Euler(hitInfo.normal);
            hole.transform.localScale = transform.localScale;
        }


        Destroy(gameObject);
    }


    public void setDamage(float dmg)
    {
        damage = dmg;
    }
}
