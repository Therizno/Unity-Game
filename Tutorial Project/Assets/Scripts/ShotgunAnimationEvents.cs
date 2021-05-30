using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunAnimationEvents : MonoBehaviour
{
    [SerializeField] private GameObject gunBehavior;
    [SerializeField] private GameObject shellDropper;
    [SerializeField] private GameObject animatedShell;

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

    public void dropShell()
    {
        shellDropper.GetComponent<SpentAmmoFactory>().createSpentShell();
    }


    //these are used to make the animation shell briefly invisible,
    //allowing the physics-based shell to be seen instead
    public void deactivateAnimShell()
    {
        animatedShell.GetComponent<SkinnedMeshRenderer>().enabled = false;
    }

    public void activateAnimShell()
    {
        animatedShell.GetComponent<SkinnedMeshRenderer>().enabled = true;
    }
}
