using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantBehavior : MonoBehaviour
{
    private GameManager gm;


    [SerializeField] float speed;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.getInstance();
    }

    // FixedUpdate is called on a fixed interval
    void FixedUpdate()
    {
        //temp demo code
        Vector3 playerCoords = gm.getPlayerCoords();

        Vector3 goalPosition = new Vector3(playerCoords.x, transform.position.y, playerCoords.z);
        transform.position = Vector3.MoveTowards(transform.position, goalPosition, speed); 
    }
}
