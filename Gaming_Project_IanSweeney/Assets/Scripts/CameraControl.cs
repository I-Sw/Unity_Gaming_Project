using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    //Creates an object to store a connection to the player character
    CharacterMovement character;
    //Creates int values which set the height and offset of the camera
    //This allows potential for camera zoom & movement
    int camera_height = 3;
    int camera_offset = 4;

    // Start is called before the first frame update
    void Start()
    {
        //Retrieves the player character for use in the camera control script
        character = FindObjectOfType<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        //Retrieves the position of the player character
		Vector3 position = character.transform.position;
        //Sets the camera position in relation to the player character
		this.transform.position = Vector3.Lerp(transform.position, new Vector3(position.x, position.y + camera_height, position.z - camera_offset),0.1f);
	}

    internal void disconnect()
    {
        //May need a check to ensure camera doesn't try to disconnect when already not connected to player
        transform.parent = null;
    }
}