using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantBehavior : MonoBehaviour, Damageable, MeleeAttacker
{
    private GameManager gm;

    private CharacterController cc;

    private MutantAnimation muAnim;


    [SerializeField] float speed; 
    [SerializeField] float attackDistance;

    [SerializeField] int maxHealth;


    private int health;

    private bool isAttack;


    // called before start
    void Awake()
    {
        health = maxHealth;
    }


    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.getInstance();

        cc = GetComponent<CharacterController>();

        muAnim = GetComponent<MutantAnimation>();
    }

    // FixedUpdate is called on a fixed interval
    void FixedUpdate()
    {
        //move to player//
        Vector3 playerCoords = gm.getPlayerCoords();
        moveToward(playerCoords);

        //give the animator info//
        setAnimVars();
    }

    private void moveToward(Vector3 coords)
    {
        //move towards the position on the x and z axes
        Vector3 goalPosition = new Vector3(coords.x, transform.position.y, coords.z);

        if (Vector3.Distance(goalPosition, transform.position) > attackDistance)
        {
            cc.SimpleMove(transform.forward * speed);

            isAttack = false;

        }
        else    
        {
            isAttack = true;       //attack if close enough
        }


        //face the position
        transform.LookAt(coords);

        //correct the monster's orientation so that it doesn't rotate on the x axis
        Vector3 transformCorrection = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
        transform.rotation = Quaternion.Euler(transformCorrection);
    }



    private void setAnimVars()
    {
        muAnim.setMoveSpeed(Vector3.Magnitude(cc.velocity));
        muAnim.setAttacking(isAttack);
    }


    /*
     *  Damageable methods
     */

    public void takeDamage(int amount)
    {
        health -= amount;

        if (health < 0)
        {
            health = 0;
        }
    }

    public void heal(int amount)
    {
        health += amount;

        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public bool playerTeam()
    {
        return false;
    }



    /*
     *  MeleeAttacker methods
     */

    public bool isAttacking()
    {
        return isAttack;
    }
}
