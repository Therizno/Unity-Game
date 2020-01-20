using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    private GameManager gm;

    [SerializeField] private float sensitivity;
    [SerializeField] private float smoothing;
    [SerializeField] private float aimSensitivity;

    //total camera rotation
    private Vector2 mouseLook;

    private GameObject player;


    // Awake is called before the first frame update
    void Awake()
    {
        //lock cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    //Start is called right after Awake, and is for interaction with other objects
    void Start()
    {
        gm = GameManager.getInstance();

        player = transform.parent.gameObject;
    }


    // FixedUpdate is called once per set unit of time
    void FixedUpdate()
    {
        //get raw input
        var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        //smooth input and add it to total mouse movement
        mouseLook += smoothMouseInput(mouseDelta);

        //clamp x axis
        clampX();

        //transform camera
        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);

        //transform player
        player.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, player.transform.up);
    }


    //uses lerping to smooth mouse input
    private Vector2 smoothMouseInput(Vector2 raw) {

        Vector2 smoothV = new Vector2();

        raw *= (gm.getPlayerAiming() ? aimSensitivity : sensitivity) * smoothing;

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
}
