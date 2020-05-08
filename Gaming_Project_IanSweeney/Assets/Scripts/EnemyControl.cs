using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Seeking, //Looking for player
    Chasing, //Chasing player
    Attacking //Attacking player
}

public class EnemyControl : MonoBehaviour, Health
{
    //Creating a variable to store the enemies current state using the above enum
    //Set to seeking to begin
    State enemyState = State.Seeking;
    //Creates an object to store a connection to the player character
    CharacterMovement character;
    //Creates a float value for the enemies health
    float enemyHP;
    //Creates an int value for enemy movement speed
    int current_speed = 1;
    //Creates three Vector3 variables to store the player and enemy positions, as well as the vector between them
    Vector3 charPos;
    Vector3 enemyPos;
    Vector3 enemyToChar;


    // Start is called before the first frame update
    void Start()
    {
        //Retrieves the player character for use in the script
        character = FindObjectOfType<CharacterMovement>();
        //Sets default enemy state to Seeking
        enemyState = State.Seeking;
    }

    // Update is called once per frame
    void Update()
    {      
        switch(enemyState)
        {
            case State.Seeking:
                //Tests for line of sight, if it is true enemy enters Chasing mode
                if(lineOfSight() == true)
                {
                    enemyState = State.Chasing;
                }
                break;
             
            case State.Chasing:
                //Rotates the enemy to look at the player
                transform.LookAt(character.transform);
                //Moves the enemy forward to the player
                transform.position += current_speed * transform.forward * Time.deltaTime;
                //Tests for line of sight, if it is false returns to Seeking mode
                if (lineOfSight() == false)
                {
                    enemyState = State.Seeking;
                }
                break;
        }
    }

    bool lineOfSight()
    {
        charPos = character.transform.position;
        enemyPos = this.transform.position;
        enemyToChar = enemyPos - charPos;

        Debug.Log("los-called");
        RaycastHit hit;

        if (Physics.Raycast(enemyPos, enemyToChar, out hit, 20f))
        {
            if(hit.collider.tag == "Player")
            {
                return true;
            }           
        }

        return false;
    }

    public void setHP(float HP)
    {
        enemyHP = HP;
    }

    public float getHP()
    {
        return enemyHP;
    }

    public void reduceHP(float reduction)
    {
        enemyHP -= reduction;
    }

    public void increaseHP(float increase)
    {
        enemyHP += increase;
    }

}
