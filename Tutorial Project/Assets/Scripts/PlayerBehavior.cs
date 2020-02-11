using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour, Observable
{
    private GameManager gm;


    [SerializeField] private GameObject cam;
    [SerializeField] private GameObject fpsHands;

    private PlayerMovement playerMovement;
    private CameraRotation cameraRotation;
    private PlayerAnimation playerAnimation;

    private List<Observer> observers;


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
    }

    // Update is called once per frame
    void Update()
    {
        
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



    public bool fire()
    {
        return !gm.getPlayerJumping() && !gm.getPlayerSprinting() && Input.GetAxis("Fire1") != 0;
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
