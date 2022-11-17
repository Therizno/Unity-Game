using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    private GameManager gm;

    [SerializeField] private float sensitivity;
    [SerializeField] private float aimSensitMultiplier;
    [SerializeField] private float smoothing;
    [SerializeField] private float defaultFOV;
    [SerializeField] private float aimingFOV;
    [SerializeField] private float smoothFOVTransit;

    //total camera rotation
    private Vector2 mouseLook;

    private GameObject player;


    // Awake is called before the first frame update
    void Awake()
    {
        //lock cursor
        Cursor.lockState = CursorLockMode.Locked;

        //set FOV to default
        Camera.main.fieldOfView = defaultFOV;
    }

    //Start is called right after Awake, and is for interaction with other objects
    void Start()
    {
        gm = GameManager.getInstance();

        player = transform.parent.gameObject;
    }


    // FixedUpdate is called once per set unit of time
    void Update()
    {
        //get raw input
        var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        
        //smooth input and add it to total mouse movement
        mouseLook += smoothMouseInput(mouseDelta);

        //clamp x axis
        clampX();

        //transform camera
        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);

        //adjust the camera's field of view
        adjustFOV();

        //transform player
        player.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, player.transform.up);
    }


    //uses lerping to smooth mouse input
    private Vector2 smoothMouseInput(Vector2 raw) {

        Vector2 smoothV = new Vector2();

        raw *= (gm.getPlayerAiming() ? aimSensitMultiplier : 1.0f) *  sensitivity * smoothing;

        smoothV.x = Mathf.Lerp(smoothV.x, raw.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, raw.y, 1f / smoothing);

        return smoothV;
    }

    //prevents player perspective from turning upside down
    private void clampX() {

        //mouse's y is unity's x
        if (mouseLook.y > 90)
        {
            mouseLook.y = 90;
        }
        else if (mouseLook.y < -90)
        {
            mouseLook.y = -90;
        }
    }

    
    private void adjustFOV()
    {
        if (gm.getPlayerAiming())
        {
            Camera.main.fieldOfView -= (Camera.main.fieldOfView - aimingFOV)/smoothFOVTransit;
        }
        else
        {
            //return to default 
            Camera.main.fieldOfView += (defaultFOV - Camera.main.fieldOfView)/smoothFOVTransit;
        }
    }
}
