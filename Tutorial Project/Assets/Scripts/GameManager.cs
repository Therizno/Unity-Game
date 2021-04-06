using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour, Observable
{
    [SerializeField] private Color32 colorGreen;
    [SerializeField] private Color32 colorYellow;
    [SerializeField] private Color32 colorRed;


    [SerializeField] private GameObject player;
    [SerializeField] private float bulletHoleCleanupTime;

    private PlayerBehavior playerBehavior;


    private List<Observer> observers;


    /*
     * singleton
     */

    private static GameManager g;

    // hide the constructor
    private GameManager()
    {

    }

    // get the instance of the singleton
    public static GameManager getInstance()
    {
        return g;
    }

    // Awake is called before the first frame update
    private void Awake()
    {
        if (g == null)
        {
            g = this;
        }
    }

    /*
     * end singleton
     */


    // Start is called before the first frame update (use for getting other objects)
    void Start()
    {
        observers = new List<Observer>();

        playerBehavior = player.GetComponent<PlayerBehavior>();
    }

    void Update()
    {
        checkEndState();
    }



    //checks when to end the game
    private void checkEndState()
    {
        if (playerBehavior.getHealth() == 0)
        {
            Time.timeScale = 0;

            //tell all other necessary scripts
            //allows post-death actions to take place while update() is not being called
            notifyAll(GameEvent.PlayerDeath);
        }
    }



    // getters and setters

    public Color32 green() { return colorGreen;  }
    public Color32 yellow() { return colorYellow; }
    public Color32 red() { return colorRed; }

    public float getBulletHoleCleanupTime()
    {
        return bulletHoleCleanupTime;
    }

    public Vector3 getPlayerCoords()
    {
        return player.transform.position;
    }

    public float getPlayerMoveSpeed()
    {
        return playerBehavior.getPlayerMoveSpeed();
    }

    public float getPlayerMomentum()
    {
        return playerBehavior.getPlayerMomentum();
    }

    public bool getPlayerSprinting()
    {
        return playerBehavior.getPlayerSprinting();
    }

    public bool getPlayerJumping()
    {
        return playerBehavior.getPlayerJumping();
    }

    public bool getPlayerAiming()
    {
        return playerBehavior.getPlayerAiming();
    }

    public bool getPlayerReloading()
    {
        return playerBehavior.getPlayerReloading();
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

    private void notifyAll(GameEvent game)
    {
        foreach (Observer o in observers)
        {
            o.notify(game);
        }
    }
}



public enum GameEvent
{
    // used for observer pattern
    FireWeapon, ReloadWeapon, PlayerDeath
}