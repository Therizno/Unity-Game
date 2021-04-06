using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Diagnostics;


public class GameManager : MonoBehaviour, Observable
{
    [SerializeField] private Color32 colorGreen;
    [SerializeField] private Color32 colorYellow;
    [SerializeField] private Color32 colorRed;


    [SerializeField] private GameObject player;
    [SerializeField] private float bulletHoleCleanupTime;

    private PlayerBehavior playerBehavior;

    private bool playerDead;
    private Stopwatch stopwatch;


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

        playerDead = false;
        stopwatch = new Stopwatch();

        playerBehavior = player.GetComponent<PlayerBehavior>();
    }

    void Update()
    {
        checkEndState();
    }



    //checks when to end the game
    private void checkEndState()
    {
        if (playerBehavior.getHealth() == 0 && !playerDead)
        {
            playerDead = true;

            //tell all other necessary scripts
            //allows post-death actions to take place before the game is frozen
            notifyAll(GameEvent.PlayerDeath);

            stopwatch.Start();
        }
        //wait for a few millis before freesing the game, so that the death message can render
        else if (playerDead && stopwatch.Elapsed.Milliseconds > 50)
        {
            //freeze the game
            Time.timeScale = 0;

            System.Threading.Thread.Sleep(1000);

            SceneManager.LoadSceneAsync("StartScreen");
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