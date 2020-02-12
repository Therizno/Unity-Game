using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // singleton
    private static GameManager g;


    [SerializeField] private GameObject player;

    private PlayerBehavior playerBehavior;


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


    // Start is called before the first frame update (use for getting other objects)
    void Start()
    {
        //To do: replace this object with the PlayerBehavior object 
        playerBehavior = player.GetComponent<PlayerBehavior>();
    }



	// getters and setters

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
}

public enum GameEvent
{
    // localized events (can happen multiple places, multiple times at once)
    FireWeapon

    //globalized events (detected by the game manager (WIP))
}