using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEffects : MonoBehaviour, Observer
{
    private AudioSource audioSource;

    [SerializeField] private AudioClip shot;
    [SerializeField] private GameObject fireParticles;

    private ParticleSystem shotEffects;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        shotEffects = fireParticles.GetComponent<ParticleSystem>();
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
            shotEffects.Play();
        }
        
    }
}
