using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleanup : MonoBehaviour
{
    [SerializeField] float cleanupTime;     //the time to delete this object, in seconds

    // Start is called before the first frame update (use for interaction with other objects)
    void Start()
    {
        //destroy object after cooldown
        Destroy(gameObject, cleanupTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
