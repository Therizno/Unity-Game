using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This class is for handling the interface between the player and the game. 
It notifies lower level player control classes when relevant events occur. 
*/

public class PlayerBehavior : MonoBehaviour, Observable
{
    private GameManager gm;


    [SerializeField] private GameObject cam;
    [SerializeField] private GameObject fpsHands;
    [SerializeField] private GameObject bulletSource;

    private PlayerMovement playerMovement;
    private CameraRotation cameraRotation;
    private PlayerAnimation playerAnimation;
    private GunEffects gunEffects;
    private ShotgunBehavior shotgunBehavior;

    private List<Observer> observers;


    [SerializeField] float fireCooldown;

    private float timeSinceFire;


    [SerializeField] int startingAmmoShells;


    // called before start
    void Awake()
    {
        observers = new List<Observer>();
    }

    // Start is called before the first frame update (use for getting other objects)
    void Start()
    {
        gm = GameManager.getInstance();

        playerMovement = GetComponent<PlayerMovement>();
        cameraRotation = cam.GetComponent<CameraRotation>();
        playerAnimation = fpsHands.GetComponent<PlayerAnimation>();
        gunEffects = fpsHands.GetComponent<GunEffects>();
        shotgunBehavior = bulletSource.GetComponent<ShotgunBehavior>();

        addObserver(playerAnimation);
        addObserver(gunEffects);
        addObserver(shotgunBehavior);

        // give the player some starting ammo 
        shotgunBehavior.addReserveShells(startingAmmoShells);
    }

    // Update is called once per frame
    void Update()
    {
        checkFire();

        checkReload();
    }


    // Observable methods

    public void addObserver(Observer o)
    {
        observers.Add(o);
    }

    public void removeObserver(Observer o)
    {
        observers.Remove(o);
    }

    private void notifyAll(GameEvent g)
    {
        foreach (Observer o in observers)
        {
            o.notify(g);
        }
    }



    private void checkFire()
    {
        if (!gm.getPlayerJumping() && !gm.getPlayerSprinting() && Input.GetAxis("Fire1") != 0 && timeSinceFire > fireCooldown && playerAnimation.isFireReady() && shotgunBehavior.isChambered())
        {
            notifyAll(GameEvent.FireWeapon);
            timeSinceFire = 0;
        }
        else
        {
            timeSinceFire += Time.deltaTime; 
        }
    }


    private void checkReload()
    {
        if (!gm.getPlayerJumping() && !gm.getPlayerSprinting() && Input.GetAxis("Reload") != 0 && !playerAnimation.isReloading() && !playerAnimation.isFiring() && shotgunBehavior.canReload())
            notifyAll(GameEvent.ReloadWeapon);
        
        // reload either the number of shells that will fit in the mag, or the number of shells left
        playerAnimation.setReloadShellNum(Mathf.Min(shotgunBehavior.getEmptyMagCapacity(), shotgunBehavior.getReserveShells()));
    }


    // getters and setters

    public float getPlayerMoveSpeed()
    {
        return playerMovement.getCurrentMoveSpeed();
    }

    public float getPlayerMomentum()
    {
        return playerMovement.getPlayerMomentum();
    }

    public bool getPlayerSprinting()
    {
        return playerMovement.isSprinting();
    }

    public bool getPlayerJumping()
    {
        return playerMovement.isJumping();
    }

    public bool getPlayerAiming()
    {
        return playerMovement.isAiming();
    }

    public bool getPlayerReloading()
    {
        return playerAnimation.isReloading();
    }

    public int getClipAmmo()
    {
        return shotgunBehavior.getShellsHolding();
    }

    public int getReserveAmmo()
    {
        return shotgunBehavior.getReserveShells();
    }
}
