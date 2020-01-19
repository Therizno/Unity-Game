using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // singleton
    private static GameManager g;

    //[SerializeField] private GameObject player;


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
}
