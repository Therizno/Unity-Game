using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBehavior : MonoBehaviour, Observer
{
    private BulletFactory fac;

    [SerializeField] private float pelletVelocity;
    [SerializeField] private float pelletSize;
    [SerializeField] private float numPellets;
    [SerializeField] private float maxPelletSpread;
    [SerializeField] private float pelletDamage;

    [SerializeField] private uint maxShellCapacity;

    private bool isChambered;
    private uint shellsHeld;     //does not include chambered shell


    //Awake is called before start
    void Awake()
    {
        isChambered = true;
        shellsHeld = maxShellCapacity;
    }

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
            fire();
        }
    }


    private void fire()
    {
        //keep track of ammo 
        if (shellsHeld == 0 && isChambered)
        {
            isChambered = false;
        }
        else
        {
            shellsHeld -= 1;
        }


        for (int i = 0; i < numPellets; i++)
        {
            switch (i % 4)
            {
                //distribute bullets between each quadrant 
                case 0:
                    fac.createBullet(transform.forward + (transform.right * Random.Range(0.0f, maxPelletSpread)) + (transform.up * Random.Range(0.0f, maxPelletSpread)), pelletVelocity, pelletSize, pelletDamage);
                    break;

                case 1:
                    fac.createBullet(transform.forward + (-transform.right * Random.Range(0.0f, maxPelletSpread)) + (transform.up * Random.Range(0.0f, maxPelletSpread)), pelletVelocity, pelletSize, pelletDamage);
                    break;

                case 2:
                    fac.createBullet(transform.forward + (transform.right * Random.Range(0.0f, maxPelletSpread)) + (-transform.up * Random.Range(0.0f, maxPelletSpread)), pelletVelocity, pelletSize, pelletDamage);
                    break;

                case 3:
                    fac.createBullet(transform.forward + (-transform.right * Random.Range(0.0f, maxPelletSpread)) + (-transform.up * Random.Range(0.0f, maxPelletSpread)), pelletVelocity, pelletSize, pelletDamage);
                    break;
            }
        }
    }


    //getters and setters

    public void setShellsHeld(int shells)
    {
        if (shells > 0 && shells < maxShellCapacity)
            shellsHeld = (uint)shells;
    }
}
