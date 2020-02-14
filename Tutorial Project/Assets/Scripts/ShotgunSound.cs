using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunSound : MonoBehaviour, Observer
{
    private AudioSource audioSource;

    [SerializeField] private AudioClip shot;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // Observer methods

    public void notify(GameEvent g)
    {
        if (g == GameEvent.FireWeapon)
        {
            audioSource.PlayOneShot(shot);
        }
        
    }
}
