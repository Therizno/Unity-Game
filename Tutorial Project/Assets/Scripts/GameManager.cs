using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // singleton
    private static GameManager g;


    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerArms;

    private PlayerMovement playerMovement;
    private PlayerAnimation playerAnim;

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
        playerAnim = playerArms.GetComponent<PlayerAnimation>();
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
