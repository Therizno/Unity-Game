using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private float bulletScaleMultiplier;
    [SerializeField] private float raycastOffset;

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
        /*
         * Raycast to find the normal of the surface of the hit at the point of contact
         */

        RaycastHit hitInfo;

        //A point slightly behind the bullet 
        Vector3 offset = transform.position - (transform.up * raycastOffset);

        if (other.gameObject.layer == LayerMask.NameToLayer("BulletCollidable")
            && Physics.Raycast(offset, transform.up, out hitInfo, Mathf.Infinity, LayerMask.GetMask("BulletCollidable")))
        {
            // instantiate a bullet hole
            GameObject hole;

            if (Random.Range(0, 2) == 0)
            {
                hole = Instantiate(bulletHole1);
            }
            else
            {
                hole = Instantiate(bulletHole2);
            }

            //Position the bullet hole slightly above the surface
            hole.transform.position = offset;

            //Rotate it to sit flush against the surface
            hole.transform.rotation = Quaternion.FromToRotation(hole.transform.up, hitInfo.normal);

            //Scale it correctly
            hole.transform.localScale = transform.localScale * bulletScaleMultiplier;

            //Destroy the bullet
            Destroy(gameObject);
        }
    }


    public void setDamage(float dmg)
    {
        damage = dmg;
    }
}
