using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantBehavior : MonoBehaviour
{
    private GameManager gm;


    [SerializeField] float speed;

    [SerializeField] float attackDistance;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.getInstance();
    }

    // FixedUpdate is called on a fixed interval
    void FixedUpdate()
    {
        //move to player//
        Vector3 playerCoords = gm.getPlayerCoords();
        moveToward(playerCoords, true);
    }

    private void moveToward(Vector3 coords, bool attacking)
    {
        //move towards the position on the x and z axes
        Vector3 goalPosition = new Vector3(coords.x, transform.position.y, coords.z);

        if (!attacking || Vector3.Distance(goalPosition, transform.position) > attackDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, goalPosition, speed);
        }
        else    
        {
            attack();       //attack if nessecary
        }


        //face the position
        transform.LookAt(coords);

        //correct the monster's orientation so that it doesn't rotate on the x axis
        Vector3 transformCorrection = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
        transform.rotation = Quaternion.Euler(transformCorrection);
    }


    private void attack()
    {
        //fill in later
    }
}
