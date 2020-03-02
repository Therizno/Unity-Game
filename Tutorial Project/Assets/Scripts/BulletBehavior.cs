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
        GameObject hole;

        GameObject collParent = other.gameObject;

        if (Random.Range(0, 2) == 0)
        {
            hole = Instantiate(bulletHole1);
        }
        else
        {
            hole = Instantiate(bulletHole2);
        }

        hole.transform.position = transform.position;
        hole.transform.rotation = collParent.transform.rotation;
        hole.transform.localScale = transform.localScale;


        Destroy(gameObject);
    }


    public void setDamage(float dmg)
    {
        damage = dmg;
    }
}
