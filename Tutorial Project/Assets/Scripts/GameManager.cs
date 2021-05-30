using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour, Observable
{
    [SerializeField] private Color32 colorGreen;
    [SerializeField] private Color32 colorYellow;
    [SerializeField] private Color32 colorRed;


    [SerializeField] private GameObject player;
    [SerializeField] private float cleanupTime;

    private PlayerBehavior playerBehavior;

    private bool playerDead;


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
        }
        else if (playerDead)
        {
            //freeze the game
            Time.timeScale = 0;

            System.Threading.Thread.Sleep(1000);

            SceneManager.LoadScene("StartScreen");
            
        }
    }



    // getters and setters

    public Color32 green() { return colorGreen;  }
    public Color32 yellow() { return colorYellow; }
    public Color32 red() { return colorRed; }

    public float getCleanupTime()
    {
        return cleanupTime;
    }


    //player-related getters and setters

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
    FireWeapon, ReloadWeapon, PlayerDeath, MonsterDeath
}