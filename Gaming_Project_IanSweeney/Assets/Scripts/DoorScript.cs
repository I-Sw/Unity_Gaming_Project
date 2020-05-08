using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    LevelGenerator levelGen = new LevelGenerator();
    V2_LevelGenerator levelGen2 = new V2_LevelGenerator();
    bool triggered = false;

    // Start is called before the first frame update
    void Start()
    {       
        levelGen = FindObjectOfType<LevelGenerator>();      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Based off of https://learn.unity.com/tutorial/collecting-scoring-and-building-the-game
        //Allows the player to pick up items with the PickUp tag, which currently handles a movement speed upgrade
        if (other.tag == "Player")
        {
            if(triggered == false)
            {
                if(this.tag == "Door")
                {
                    Debug.Log("Door2");
                    double xPos = this.transform.position.x;
                    double zPos = this.transform.position.z;
                    double arrayX = (xPos - (xPos % 12)) / 12;
                    double arrayZ = (zPos - (zPos % 12)) / 12;             
                    levelGen2.generateRoom((int)arrayX, (int)arrayZ);
                
                }
            }
            Debug.Log("Door");

            triggered = true;

            /*
            if(this.name == "Door_Corridor")
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, new Quaternion(0f, transform.position.y + 45, 0f, 0f), .1f);
                gameObject.SetActive(false);
                levelGen.createCorridorAt(transform.position);
                Debug.Log(other.transform.rotation.y.ToString());
                levelGen.snapBlockAt(transform.position);
            }

            if(this.name == "Door_Room")
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, new Quaternion(0f, transform.position.y + 45, 0f, 0f), .1f);
                gameObject.SetActive(false);
                levelGen.createRoomAt(transform.position);
            } 
            */                 
        }
    }

    private (float, float) returnGridPosition()
    {
        float xPos, zPos;

        xPos = this.transform.position.x - (this.transform.position.x % 12);
        zPos = this.transform.position.z - (this.transform.position.z % 12);

        return (xPos, zPos);
    }
}
