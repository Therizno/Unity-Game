using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private float leftMargin;
    [SerializeField] private float rightMargin;
    [SerializeField] private float spaceBetweenElements;

    [SerializeField] private float leftMarginL;


    [SerializeField] private GameObject playerObj;

    [SerializeField] private GameObject ammoDisplayBox;
    [SerializeField] private GameObject magCapacityObj;
    [SerializeField] private GameObject reserveAmmoObj;

    [SerializeField] private GameObject healthAmountObj;
    [SerializeField] private GameObject healthDisplayBox;


    private PlayerBehavior playerBehavior;

    private TextMeshProUGUI magCapacity;
    private TextMeshProUGUI reserveCapacity;

    private TextMeshProUGUI healthAmount;


    private GameManager gm;


    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.getInstance();


        playerBehavior = playerObj.GetComponent<PlayerBehavior>();

        magCapacity = magCapacityObj.GetComponent<TextMeshProUGUI>();
        reserveCapacity = reserveAmmoObj.GetComponent<TextMeshProUGUI>();

        healthAmount = healthAmountObj.GetComponent<TextMeshProUGUI>();
    }


    // Update is called once per frame
    void Update()
    {
        //update ammo information graphic
        magCapacity.SetText(playerBehavior.getClipAmmo() + "/");
        reserveCapacity.SetText("" + playerBehavior.getReserveAmmo());

        //update health graphic
        healthAmount.SetText(""+playerBehavior.getHealth());

        int health = playerBehavior.getHealth();
        int maxHealth = playerBehavior.getMaxHealth();

        if (health >= 2 * (maxHealth / 3))
        {
            healthAmount.color = gm.green();
        }
        else if (health >= maxHealth / 3)
        {
            healthAmount.color = gm.yellow();
        }
        else
        {
            healthAmount.color = gm.red();
        }


        alignBottomLeftElements();
        alignBottomRightElements();
    }


    private void alignBottomRightElements()
    {
        Rect HUDBox = gameObject.GetComponent<RectTransform>().rect; //rect of HUD


        //position the ammo HUD box so that it has room for the HUD elements inside with some margin

        float currentY = ammoDisplayBox.GetComponent<RectTransform>().anchoredPosition.y;

        ammoDisplayBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(HUDBox.width/2 - (magCapacity.renderedWidth + reserveCapacity.renderedWidth + spaceBetweenElements + rightMargin), currentY);


        //make the magazine ammo HUD element stick to the left side of the ammo HUD box element

        currentY = magCapacityObj.GetComponent<RectTransform>().anchoredPosition.y;

        magCapacityObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(leftMargin, currentY);


        //make the reserve ammo HUD element stick to the left side of the mag capacity HUD element

        currentY = reserveAmmoObj.GetComponent<RectTransform>().anchoredPosition.y;

        reserveAmmoObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(magCapacity.renderedWidth + spaceBetweenElements, currentY);
    }


    private void alignBottomLeftElements()
    {
        float healthStatRectX = healthAmountObj.GetComponent<RectTransform>().anchoredPosition.x;
        float currentY = healthDisplayBox.GetComponent<RectTransform>().anchoredPosition.y;

        //make the background of the health hud element move to fit the contents
        healthDisplayBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(healthStatRectX + leftMarginL, currentY);
    }
}
