using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollBehavior : MonoBehaviour, Observer
{
    [SerializeField] GameObject parentObject;
    [SerializeField] GameObject colliderObject;

    private RagdollHub parentObj;


    // Start is called before the first frame update
    void Start()
    {
        //attempts to attach itself to a RagdollHub
        parentObj = parentObject.GetComponent<RagdollHub>();

        if (parentObj != null)
        {
            parentObj.addObserver(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void notify(GameEvent game)
    {
        //ragdoll on hub death
        if (game == GameEvent.MonsterDeath)
        {
            activateRagdoll();
        }
    }


    public void activateRagdoll()
    {
        colliderObject.GetComponent<Collider>().isTrigger = false;

        Rigidbody rb = GetComponent<Rigidbody>();

        rb.isKinematic = false;
        rb.maxDepenetrationVelocity = 0.1f;
    }
}
