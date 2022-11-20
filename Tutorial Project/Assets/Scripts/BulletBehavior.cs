using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private int damage;
    private float speed;
    private float size;

    [SerializeField] private GameObject bulletHole1;
    [SerializeField] private GameObject bulletHole2;


    // Start is called before the first frame update (use for getting other objects)
    void Start()
    {

    }

    // FixedUpdate is called once per fixed period of time
    void FixedUpdate()
    {
        detectHit(transform.position);

        //move the bullet forward
        transform.position += transform.up * speed;
    }

    private void detectHit(Vector3 startPos)
    {
        /*
        * Raycast to detect whether there is a collidable object between the bullet's current
        * position and it's next
        */

        RaycastHit hitInfo;

        if (Physics.Raycast(startPos, transform.up, out hitInfo, speed, LayerMask.GetMask("BulletCollidable")))
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

            //Position the bullet hole at the point of contact
            hole.transform.position = hitInfo.point;

            //Rotate it to sit flush against the surface
            hole.transform.rotation = Quaternion.FromToRotation(hole.transform.up, hitInfo.normal);

            //Scale it correctly
            hole.transform.localScale = hole.transform.localScale * size;

            //Attach it to the gameObject it hit
            hole.transform.SetParent(hitInfo.collider.gameObject.transform);
            

            //Deal damage if object has a hitbox
            HitboxBehavior hb = hitInfo.collider.gameObject.GetComponent<HitboxBehavior>();

            if (hb != null)
            {
                hb.damageParent(damage);
            }



            //Destroy the bullet
            Destroy(gameObject);
        }
    }


    public void setDamage(int dmg)
    {
        damage = dmg;
    }

    public void setSpeed(float spd)
    {
        speed = spd;
    }

    public void setSize(float bulSize)
    {
        size = bulSize;
        transform.localScale = new Vector3(size, size, size);
    }
}
