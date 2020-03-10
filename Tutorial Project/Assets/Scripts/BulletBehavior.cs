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

        transform.position -= (transform.up * 0.1f);

        if (Physics.Raycast(transform.position, transform.up, out hitInfo, Mathf.Infinity, LayerMask.GetMask("BulletCollidable")))
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


            hole.transform.position = transform.position;
            GameObject child = hole.transform.GetChild(0).gameObject;

            Debug.Log(hole.transform.position - transform.position);
            hole.transform.rotation = Quaternion.FromToRotation(hole.transform.up, hitInfo.normal);
            hole.transform.localScale = transform.localScale;

            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }


        //Destroy(gameObject);
    }


    public void setDamage(float dmg)
    {
        damage = dmg;
    }
}
