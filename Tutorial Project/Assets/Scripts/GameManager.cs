using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // singleton
    private static GameManager g;


    [SerializeField] private GameObject player;

    private PlayerMovement playerMovement;

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
        playerMovement = player.GetComponent<PlayerMovement>();
    }



    // getters and setters

    public float getPlayerMoveSpeed()
    {
        return playerMovement.getCurrentMoveSpeed();
    }
}
