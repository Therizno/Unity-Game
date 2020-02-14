using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This class is for handling game aspects of the player rather than controls.
It notifies lower level player control classes when relevant events occur.
DOES NOT facilitate communication BETWEEN lower level control classes - instead
passes that information to GameManager. 
*/

public class PlayerBehavior : MonoBehaviour, Observable
{
    private GameManager gm;


    [SerializeField] private GameObject cam;
    [SerializeField] private GameObject fpsHands;

    private PlayerMovement playerMovement;
    private CameraRotation cameraRotation;
    private PlayerAnimation playerAnimation;
    private ShotgunSound shotgunSound;

    private List<Observer> observers;


    [SerializeField] float fireCooldown;

    private float timeSinceFire;


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
        shotgunSound = fpsHands.GetComponent<ShotgunSound>();

        addObserver(playerAnimation);
        addObserver(shotgunSound);
    }

    // Update is called once per frame
    void Update()
    {
        checkFire();
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
        if (!gm.getPlayerJumping() && !gm.getPlayerSprinting() && Input.GetAxis("Fire1") != 0 && timeSinceFire > fireCooldown)
        {
            notifyAll(GameEvent.FireWeapon);
            timeSinceFire = 0;
        }
        else
        {
            timeSinceFire += Time.deltaTime; 
        }
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
}
