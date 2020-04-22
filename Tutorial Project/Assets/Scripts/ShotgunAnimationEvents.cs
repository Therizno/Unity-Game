using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunAnimationEvents : MonoBehaviour
{
    [SerializeField] private GameObject gunBehavior;

    private ShotgunBehavior shotgunBehavior;

    // Start is called before the first frame update
    void Start()
    {
        shotgunBehavior = gunBehavior.GetComponent<ShotgunBehavior>();
    }


    // any of these functions can be triggered by an animation

    public void cycleWeapon()
    {
        shotgunBehavior.cycle();
    }

    public void reloadShell()
    {
        shotgunBehavior.reloadShell();
    }
}
