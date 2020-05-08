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
    //Creates an object to store a connection to the game manager
    GameManager gameManager;
    //Creates an object to store a connection to the player character
    CharacterMovement character;
    //Creates a float value for the enemies health
    float enemyHP;
    //Creates an int value for enemy movement speed
    int current_speed = 1;
    //Creates three Vector3 variables to store the player and enemy positions as well as the vector between them
    Vector3 charPos;
    Vector3 enemyPos;
    Vector3 enemyToChar;
    //Creates an object to store the Animator
    Animator animations;
    //Creating a float value to store attack cooldown
    float attackCooldown;


    // Start is called before the first frame update
    void Start()
    {
        //Retrieves the game manager script
        gameManager = FindObjectOfType<GameManager>();
        //Retrieves the player character
        character = FindObjectOfType<CharacterMovement>();
        //Sets default enemy state to Seeking
        enemyState = State.Seeking;
        //Gets Animator
        animations = FindObjectOfType<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(enemyState)
        {
            case State.Seeking:
                Debug.Log("seeking");
                animations.SetBool("Walking", false);
                //Tests for line of sight, if it is true enemy enters Chasing mode
                if (lineOfSight() == true)
                {
                    enemyState = State.Chasing;
                }
                break;
             
            case State.Chasing:
                Debug.Log("chasing");
                //Rotates the enemy to look at the player
                this.transform.LookAt(character.transform);
                //Moves the enemy forward to the player
                this.transform.position += current_speed * transform.forward * Time.deltaTime;
                //Sets animator to walking
                animations.SetBool("Walking", true);
                //Tests for line of sight, if it is false returns to Seeking mode
                if (lineOfSight() == false)
                {
                    enemyState = State.Seeking;
                }
                if (attackRange())
                {
                    enemyState = State.Attacking;
                }
                break;

            case State.Attacking:
                Debug.Log("Attacking");
                //Rotates the enemy to look at the player
                this.transform.LookAt(character.transform);
                //Moves the enemy forward to the player
                this.transform.position += current_speed * transform.forward * Time.deltaTime;
                //Increases attack cooldown
                attackCooldown += Time.deltaTime;
                //if attackCooldown is high enough attack occurs
                if(attackCooldown > 3)
                {
                    attack();
                }                
                break;
        }
    }

    bool lineOfSight()
    {
        charPos = character.transform.position;
        enemyPos = this.transform.position;
        enemyToChar = charPos - enemyPos;
        enemyPos.y += 1;

        Debug.Log("los-called");
        RaycastHit hit;

        //Debug.DrawRay(enemyPos, enemyToChar*20, Color.red, 2f);
        if (Physics.Raycast(enemyPos, enemyToChar, out hit, 100f))
        {
            if(hit.collider.tag == "Player")
            {
                Debug.Log("player hit");
                return true;
            }           
        }

        return false;
    }

    private bool attackRange()
    {
        charPos = character.transform.position;
        enemyPos = this.transform.position;
        enemyToChar = charPos - enemyPos;
        if (enemyToChar.magnitude < 1)
        {
            return true;
        }

        return false;
    }

    private void attack()
    {
        attackCooldown = 0;
        charPos = character.transform.position;
        enemyPos = this.transform.position;
        enemyToChar = charPos - enemyPos;
        if (enemyToChar.magnitude < 1)
        {
            character.reduceHP(10);
        }
        else
        {
            enemyState = State.Seeking;
        }
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