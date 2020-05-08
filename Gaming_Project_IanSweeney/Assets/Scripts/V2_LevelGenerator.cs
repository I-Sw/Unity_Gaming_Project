
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V2_LevelGenerator : MonoBehaviour
{
    string[,] levelArray = new string[7, 7];
    List<GameObject> roomArray = new List<GameObject>();
    System.Random random = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        levelArray[3, 3] = "1111";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void regenerateMaze()
    {
        foreach (GameObject room in roomArray)
        {
            Destroy(room);
        }
        System.Array.Clear(levelArray, 0, levelArray.Length);
        generateLevel();
    }

    public void generateLevel()
    {
        //Room doors denoted by 1's using compass directions (NESW)
        //Sets the center point of the array to a "1111" room        
        int shortSide = 1;
        int longSide = 2;
        int currentX = 3;
        int currentY = 3;

        for (int i = 0; i < 3; i++)
        {
            //up 1, right short, down long, left long, up long, right short. long and short + 2, repeat
            currentY += 1;
            generateRoom(currentX, currentY);

            for (int one = 0; one < shortSide; one++)
            {
                currentX += 1;
                generateRoom(currentX, currentY);
            }

            for (int two = 0; two < longSide; two++)
            {
                currentY -= 1;
                generateRoom(currentX, currentY);
            }

            for (int three = 0; three < longSide; three++)
            {
                currentX -= 1;
                generateRoom(currentX, currentY);
            }

            for (int four = 0; four < longSide; four++)
            {
                currentY += 1;
                generateRoom(currentX, currentY);
            }

            for (int five = 0; five < shortSide - 1; five++)
            {
                currentX += 1;
                generateRoom(currentX, currentY);
            }

            currentX += 1;

            shortSide += 1;
            longSide += 2;
        }
    }

    //This method tests the 4 spaces to each side of the room at the given position.
    //If a room as found in these spaces they are checked for corresponding doors, if one is found a connecting door is added.
    //If no room is found a random generator decides whether to add a door or not.
    
    public string generateRoom(int currentX, int currentY)
    {
        string currentRoom = "";
        string roomToCheck = "";


        //Checking Above
        if (currentY != 6)
        {
            roomToCheck = levelArray[currentX, currentY + 1];

            if(!string.IsNullOrEmpty(roomToCheck))
            {
                if(roomToCheck[2] == '1')
                {
                    currentRoom += "1";
                }

                else
                    currentRoom += "0";
            }

            else if (string.IsNullOrEmpty(roomToCheck))
            {
                if (random.Next(0, 4) == 0)
                    currentRoom += 0;

                else
                    currentRoom += 1;
            }
        }

        else if (currentY == 6)
        {
            currentRoom += 0;
        }
        
        //Checking Right
        if(currentX != 6)
        {
            roomToCheck = levelArray[currentX + 1, currentY];

            if (!string.IsNullOrEmpty(roomToCheck))
            {
                if (roomToCheck[3] == '1')
                {
                    currentRoom += "1";
                }

                else
                    currentRoom += "0";
            }

            else if (string.IsNullOrEmpty(roomToCheck))
            {
                if (random.Next(0, 4) == 0)              
                    currentRoom += 0;               

                else
                    currentRoom += 1;
            }
        }

        else if (currentX == 6)
        {
            currentRoom += 0;
        }

        //Checking Below
        if(currentY !=0)
        {
            roomToCheck = levelArray[currentX, currentY - 1];

            if (!string.IsNullOrEmpty(roomToCheck))
            {
                if (roomToCheck[0] == '1')
                {
                    currentRoom += "1";
                }

                else
                    currentRoom += "0";
            }

            else if (string.IsNullOrEmpty(roomToCheck))
            {
                if (random.Next(0, 4) == 0)
                    currentRoom += 0;

                else
                    currentRoom += 1;
            }
        }

        if(currentY == 0)
        {
            currentRoom += 0;
        }

        //Checking Left
        if(currentX != 0)
        {
            roomToCheck = levelArray[currentX - 1, currentY];

            if (!string.IsNullOrEmpty(roomToCheck))
            {
                if (roomToCheck[1] == '1')
                {
                    currentRoom += "1";
                }

                else
                    currentRoom += "0";
            }

            else if (string.IsNullOrEmpty(roomToCheck))
            {
                if (random.Next(0, 4) == 0)
                    currentRoom += 0;

                else
                    currentRoom += 1;
            }
        }

        if(currentX == 0)
        {
            currentRoom += 0;
        }

        //Placing Room
        placeRoom(currentX, currentY, currentRoom);

        return currentRoom;
    }

    public void placeRoom(int xPos, int zPos, string roomType)
    {
        Debug.Log(roomType);
        levelArray[xPos, zPos] = roomType;
        Vector3 roomPosition = new Vector3((xPos*12), 0, (zPos*12));
        Object roomPrefab = Resources.Load("Room_Prefabs/Room_" + roomType);
        GameObject room = (GameObject)GameObject.Instantiate(roomPrefab, roomPosition, new Quaternion());
        Debug.Log("GO Created");
        roomArray.Add(room);
    }
}
