using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleanup : MonoBehaviour
{
    // Start is called before the first frame update (use for interaction with other objects)
    void Start()
    {
        //destroy object after cooldown
        Destroy(gameObject, GameManager.getInstance().getCleanupTime());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
