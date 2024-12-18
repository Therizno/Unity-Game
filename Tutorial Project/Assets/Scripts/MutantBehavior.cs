﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantBehavior : MonoBehaviour, Damageable, MeleeAttacker, RagdollHub
{
    private GameManager gm;

    private CharacterController cc;

    private MutantAnimation muAnim;
    private MutantAnimationEvents muAnimEvents;


    [SerializeField] private float speed; 
    [SerializeField] private float attackDistance;

    [SerializeField] private int maxHealth;

    [SerializeField] private float ragdollStartTimer; 
    [SerializeField] private float ragdollFreezeTimer;

    private List<Observer> observers; 

    private int health;
    private bool isDead;

    private bool isAttack;


    // called before start
    void Awake()
    {
        observers = new List<Observer>();

        health = maxHealth;
        isDead = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.getInstance();

        cc = GetComponent<CharacterController>();

        muAnim = GetComponent<MutantAnimation>();
        muAnimEvents = GetComponent<MutantAnimationEvents>();
    }


    // FixedUpdate is called on a fixed interval
    void FixedUpdate()
    {
        if (!isDead)
        {
            //move to player//
            Vector3 playerCoords = gm.getPlayerCoords();
            moveToward(playerCoords);

            //give the animator info//
            setAnimVars();


            //check that we're still alive//
            if (health <= 0)
            {
                isDead = true;
                StartCoroutine(onDeath());
            }
        }
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


    IEnumerator onDeath()
    {
        muAnim.setDead();   //trigger death animation

        //wait for death animation to play before ragdolling 
        yield return new WaitForSeconds(ragdollStartTimer);

        cc.enabled = false;

        notifyAll(GameEvent.MonsterDeath);

        muAnim.disableAnimations();

        yield return new WaitForSeconds(ragdollFreezeTimer);

        notifyAll(GameEvent.FreezeRigidbody);
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
        return muAnimEvents.isAttacking();
    }



    /*
     * Observable methods
     */

    public void addObserver(Observer o)
    {
        observers.Add(o);
    }

    public void removeObserver(Observer o)
    {
        observers.Remove(o);
    }

    private void notifyAll(GameEvent g)
    {
        foreach (Observer o in observers)
        {
            o.notify(g);
        }
    }
}
