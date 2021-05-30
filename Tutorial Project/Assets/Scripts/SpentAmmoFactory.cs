using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpentAmmoFactory : MonoBehaviour
{
    [SerializeField] private GameObject parentCharacter;

    [SerializeField] private GameObject spentShellPrefab;

    [SerializeField] private Vector3 velocity;

    [SerializeField] private Vector2 xSpinRange;
    [SerializeField] private Vector2 ySpinRange;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void createSpentShell()
    {
        //create the shell
        GameObject shell = Instantiate(spentShellPrefab);

        shell.transform.position = gameObject.transform.position;
        shell.transform.rotation = gameObject.transform.rotation;

        shell.transform.Rotate(90, 0, 0);


        //give it momentum
        Rigidbody rgbd = shell.GetComponent<Rigidbody>();

        rgbd.AddForce((velocity.x * shell.transform.right) + (velocity.y * shell.transform.up) + (velocity.z * shell.transform.forward));
        rgbd.AddTorque((shell.transform.right * Random.Range(xSpinRange.x, xSpinRange.y)) + (shell.transform.up * Random.Range(ySpinRange.x, ySpinRange.y)));


        //make sure it doesn't collide with the character that fired it
        CharacterController cc = parentCharacter.GetComponent<CharacterController>();

        if (cc != null)
        {
            Physics.IgnoreCollision(cc, shell.GetComponent<Collider>());
        }
    }
}
