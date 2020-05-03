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

    private bool chambered;
    private uint shellsHeld;     //shells currently held, does not include chambered shell
    private uint reserveShells;

    //Awake is called before start
    void Awake()
    {
        chambered = true;
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
        chambered = false;


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

    //tries to cycle the weapon, returns true if successful  
    public bool cycle()
    {
        if (shellsHeld > 0 && !chambered)
        {
            chambered = true;
            shellsHeld -= 1;

            return true;
        }

        return false;
    }

    //loads a single shell from reserve into the magazine 
    public bool reloadShell()
    {
        if (canReload())
        {
            shellsHeld++;
            reserveShells--;

            return true;
        }

        return false;
    }


    //getters and setters

    public int getShellsHolding()
    {
        return (int)shellsHeld + (chambered ? 1 : 0); 
    }

    public void setShellsHolding(int shells)
    {
        if (shells > 0 && shells < maxShellCapacity)
            shellsHeld = (uint)shells;
    }

    public void addReserveShells(int shells)
    {
        if(shells > 0)
            reserveShells += (uint)shells;
    }

    public int getReserveShells()
    {
        return (int)reserveShells;
    }

    public int getEmptyMagCapacity()
    {
        return (int)(maxShellCapacity - shellsHeld);
    }

    public bool isChambered()
    {
        return chambered;
    }

    public bool canReload()
    {
        return shellsHeld < maxShellCapacity && reserveShells > 0;
    }

    public int getMaxShellCapacity()
    {
        return (int)maxShellCapacity;
    }
}
