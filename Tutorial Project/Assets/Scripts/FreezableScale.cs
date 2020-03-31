using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * attach this class to any object to allow the object's scale to be frozen
 */

public class FreezableScale : MonoBehaviour
{
    private Vector3 theScale;

    private bool frozen;

    // called before start
    void Awake()
    {
        theScale = gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(frozen)
            gameObject.transform.localScale = theScale;
    }


    public void setScale(Vector3 scale)
    {
        theScale = scale;
    }

    public void freezeScale()
    {
        frozen = true;
    }

    public void unfreezeScale()
    {
        frozen = false;
    }
}
