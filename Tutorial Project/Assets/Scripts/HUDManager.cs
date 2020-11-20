using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private float margin;
    [SerializeField] private float spaceBetweenElements;


    [SerializeField] private GameObject playerObj;

    [SerializeField] private GameObject ammoDisplayBox;
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
        Rect HUDBox = gameObject.GetComponent<RectTransform>().rect; //rect of HUD
        Rect ammoBox = ammoDisplayBox.GetComponent<RectTransform>().rect; //rect of ammo HUD box 

        //position the ammo HUD box so that it has room for the HUD elements inside with some margin
        ammoDisplayBox.transform.position = new Vector3(HUDBox.width + (ammoBox.width/2) - (magCapacity.renderedWidth + reserveCapacity.renderedWidth + spaceBetweenElements + 2*margin), ammoDisplayBox.transform.position.y, 0);


        //make the magazine ammo HUD element stick to the left side of the ammo HUD box element

        float currentY = magCapacityObj.GetComponent<RectTransform>().anchoredPosition.y;

        magCapacityObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(margin + (magCapacity.renderedWidth/2), currentY);


        //make the reserve ammo HUD element stick to the left side of the mag capacity HUD element

        currentY = reserveAmmoObj.GetComponent<RectTransform>().anchoredPosition.y;

        reserveAmmoObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(margin + magCapacity.renderedWidth + spaceBetweenElements, currentY);
    }
}
