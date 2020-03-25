using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHoleCleanup : MonoBehaviour
{
    // Start is called before the first frame update (use for interaction with other objects)
    void Start()
    {
        //destroy bullet hole after cooldown
        Destroy(gameObject, GameManager.getInstance().getBulletHoleCleanupTime());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
