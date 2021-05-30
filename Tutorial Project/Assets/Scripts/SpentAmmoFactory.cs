using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpentAmmoFactory : MonoBehaviour
{
    [SerializeField] private GameObject spentShellPrefab;

    [SerializeField] private Vector3 velocity;

    [SerializeField] Vector2 xSpinRange;
    [SerializeField] Vector2 ySpinRange;
    
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
        rgbd.AddTorque(new Vector3(Random.Range(xSpinRange.x, xSpinRange.y), Random.Range(ySpinRange.x, ySpinRange.y)));
    }
}
