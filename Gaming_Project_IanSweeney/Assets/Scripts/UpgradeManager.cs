using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    System.Random random = new System.Random();
    bool[,] upgradeArray = new bool[7, 7];
    int xPos;
    int zPos;
    int randomType;
    string upgradeType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void placeUpgrades(int count)
    {
        System.Array.Clear(upgradeArray, 0, upgradeArray.Length);

        for(int i = 0; i < count; i++)
        {
            xPos = random.Next(0, 7);
            zPos = random.Next(0, 7);

            if (!upgradeArray[xPos, zPos])
            {
                //sets the position in the upgrade array to true, signalling a placed upgrade
                upgradeArray[xPos, zPos] = true;

                //generates a random type for the upgrade to be placed
                randomType = random.Next(0, 3);
                if (randomType == 1)
                    upgradeType = "Health";
                else if (randomType == 2)
                    upgradeType = "Teleport";
                else
                    upgradeType = "Speed";

                //Creates a vector for the position of the upgrade
                Vector3 upgradePosition = new Vector3((xPos * 12), 1.5f, (zPos * 12));
                //Loads a prefab of the upgrade based on the random type selected 
                Object upgradePrefab = Resources.Load("Upgrade_Prefabs/" + upgradeType + "_Upgrade");
                //Instantiates the upgrade at the chosen position
                GameObject upgrade = (GameObject)GameObject.Instantiate(upgradePrefab, upgradePosition, new Quaternion());
                Debug.Log("Upgrade Created");
            }

            else
                i--;
        }        
    }
}
