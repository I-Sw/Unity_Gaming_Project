using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //Creates Text objects to store UI text
    Text UI_Health;
    Text UI_Teleport;
    Text UI_Upgrades;
    Text UI_UpgradeCount;
    Text UI_Died;
    Color visible;
    Color transparent;
    int speedUpgrades;
    int healthUpgrades;
    int teleportUpgrades;  

    // Start is called before the first frame update
    void Start()
    {
        //Retrieves UI text objects for use in this script
        UI_Health = GameObject.Find("UI_Health").GetComponent<Text>();
        UI_Teleport = GameObject.Find("UI_Teleport").GetComponent<Text>();
        UI_Upgrades = GameObject.Find("UI_Upgrades").GetComponent<Text>();
        UI_UpgradeCount = GameObject.Find("UI_UpgradeCount").GetComponent<Text>();
        UI_Died = GameObject.Find("UI_Died").GetComponent<Text>();
        visible = UI_Died.color;
        transparent = UI_Died.color;
        transparent.a = 0.0f;
        UI_Died.color = transparent;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateUI(bool status)
    {
        if (status)
            UI_Teleport.text = "<b>Teleport: Ready</b>";
        else
            UI_Teleport.text = "<b>Teleport: Waiting...</b>";
    }

    public void updateUI(float health)
    {
        UI_Health.text = "<b>Health: " + health + "</b>";
    }

    public void updateUI(string upgradeName)
    {
        if(upgradeName == "Speed_Upgrade(Clone)")
        {
            speedUpgrades++;
        }

        if (upgradeName == "Health_Upgrade(Clone)")
        {
            healthUpgrades++;
        }

        if (upgradeName == "Teleport_Upgrade(Clone)")
        {
            teleportUpgrades++;
        }

        UI_Upgrades.text = "<b>| Upgrades |</b> \n Speed: " + speedUpgrades + "\nHealth: " + healthUpgrades + "\nTeleport: " + teleportUpgrades;
    }

    public void updateUI(int count)
    {
        UI_UpgradeCount.text = "<b>Upgrades Remaining: </b>" + count;
    }

    public void died()
    {
        UI_Died.color = visible;
    }

}
