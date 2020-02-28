using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject StoneBlock;

    // Start is called before the first frame update
    void Start()
    {
        generateLevel(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void generateLevel(int levelNum)
    {
        /*
        //int roomNum = Random.Range(1,5);
        int roomNum = 1;

        for(int i = 0; i <= roomNum; i++)
        {
            int roomXSize = Random.Range(3, 8);
            int roomZSize = Random.Range(3, 8);

            for(int j = 0; j <= roomXSize; j++)
            {
                for(int k = 0; k <= roomZSize; k++)
                {
                    createBlockAt(new Vector3(j, 1, k));
                }
            }
        }
        */

        int zStartPoint = Random.Range(0, 10);
        int xDistance = Random.Range(10, 15);

        Debug.Log(zStartPoint);
        Debug.Log(xDistance);

        for (int i = 0; i <= xDistance; i++)
        {
            createBlockAt(new Vector3(i - 15, 0, zStartPoint));
        }

        for(int j = zStartPoint; j <= 15; j++)
        {
            createBlockAt(new Vector3(xDistance-15, 0, j));
        }
    }

    //Takes a Vector3 location and Instantiates a stone block at that location
    public void createBlockAt(Vector3 position)
    {
        GameObject blockObject = Instantiate(StoneBlock, position, Quaternion.identity);
        blockObject.transform.parent = GameObject.Find("Map_Walls").transform;
    }

    public void createRoomAt(Vector3 position)
    {
        for(int i = 0; i <= 10; i++)
        {        
            
            for(int j = 0; j <= 10; j++)
            {
                createBlockAt(new Vector3(position.x + i, -1, position.z + j));
            }
        }

    }
}
