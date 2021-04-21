using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * The flowfield pathfinding class
 */

public class MonsterPathfinder : MonoBehaviour
{
    [SerializeField] GameObject testMonster;

    private MutantBehavior behavior;

    private NavMeshTriangulation triMesh;

    // Start is called before the first frame update
    void Start()
    {
        behavior = testMonster.GetComponent<MutantBehavior>();

        triMesh = NavMesh.CalculateTriangulation();

    }

    // Update is called once per frame
    void Update()
    {
    
    }


    void isOccupiable(float x, float z)
    {

    }
}
