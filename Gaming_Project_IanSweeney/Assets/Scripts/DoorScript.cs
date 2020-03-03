using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    LevelGenerator levelGen = new LevelGenerator();

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
            if(this.name == "Door_Corridor")
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, new Quaternion(0f, transform.position.y+45, 0f, 0f), .1f);
                gameObject.SetActive(false);
                levelGen.createCorridorAt(transform.position);
            }

            if(this.name == "Door_Room")
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, new Quaternion(0f, transform.position.y + 45, 0f, 0f), .1f);
                gameObject.SetActive(false);
                levelGen.createRoomAt(transform.position);
            }           
        }
    }
}
