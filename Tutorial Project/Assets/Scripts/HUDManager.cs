using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private float margin;


    [SerializeField] private GameObject playerObj;

    [SerializeField] private GameObject magCapacityObj;
    [SerializeField] private GameObject reserveAmmoObj;


    private PlayerBehavior playerBehavior;

    private TextMeshProUGUI magCapacity;
    private TextMeshProUGUI reserveCapacity;

    // Start is called before the first frame update
    void Start()
    {
        playerBehavior = playerObj.GetComponent<PlayerBehavior>();

        magCapacity = magCapacityObj.GetComponent<TextMeshProUGUI>();
        reserveCapacity = reserveAmmoObj.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        magCapacity.SetText(playerBehavior.getClipAmmo() + "/");
        reserveCapacity.SetText("" + playerBehavior.getReserveAmmo());

        alignBottomRightElements();
    }

    private void alignBottomRightElements()
    {
        Rect HUDBox = gameObject.GetComponent<RectTransform>().rect;

        float leftSideX = (HUDBox.xMax - HUDBox.xMin);
        float bottomSideY = (HUDBox.yMax - HUDBox.yMin);

        //reserveAmmoObj.transform.position = new Vector3(leftSideX - margin, bottomSideY - margin, 0);
        reserveAmmoObj.transform.position = new Vector3(HUDBox.width - margin - reserveCapacity.renderedWidth, margin, 0);

        //reserveCapacity.renderedWidth is the width of the reserve ammo text box 
    }
}
