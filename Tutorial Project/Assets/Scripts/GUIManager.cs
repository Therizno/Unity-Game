using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class GUIManager : MonoBehaviour
{
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
    }
}
