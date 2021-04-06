using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class StartManager : MonoBehaviour
{
    private Button startButton;


    /*
    * singleton
    */

    private static StartManager g;

    // hide the constructor
    private StartManager()
    {

    }

    // get the instance of the singleton
    public static StartManager getInstance()
    {
        return g;
    }

    // Awake is called before start
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



    // Start is called before the first frame update
    void Start()
    {
        startButton = GetComponent<Button>();
        startButton.onClick.AddListener(startGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void startGame()
    {
        SceneManager.LoadSceneAsync("SampleScene");
    }
}
