using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{ 
    public float sensitivity;

    //total camera rotation
    Vector2 mouseLook;

    GameObject player;

    // Awake is called before the first frame update
    void Awake()
    {
        //lock cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    //Start is called right after Awake, and is for interaction with other objects
    void Start()
    {
        player = transform.parent.gameObject;
    }


    // FixedUpdate is called once per set unit of time
    void FixedUpdate()
    {
        var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        //add smoothing here if necessary

        mouseLook += mouseDelta;

        //transform camera
        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);

        //transform player
        player.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, player.transform.up);
    }
}
