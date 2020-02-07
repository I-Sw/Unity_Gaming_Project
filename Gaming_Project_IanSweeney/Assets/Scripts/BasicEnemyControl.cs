using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyControl : MonoBehaviour
{
    //Creates an object to store a connection to the player character
    CharacterMovement character;
    //Creates an int value for enemy movement speed
    int current_speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        //Retrieves the player character for use in the script
        character = FindObjectOfType<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        //Calls the discrete_direction method, passing in the vector between the enemy and charatcer
        Vector3 dir = discrete_direction(character.transform.position - transform.position);
        //Rotates the enemy in the direction provided above
        transform.rotation = Quaternion.LookRotation(dir);
        //Moves the enemy forward toward their new target
        transform.position += current_speed * transform.forward * Time.deltaTime;
    }

    Vector3 discrete_direction(Vector3 continuous_vector)
    {
        //Calculates d, the larger between the x and z values
        float d = Mathf.Max(Mathf.Abs(continuous_vector.x), Mathf.Abs(continuous_vector.z));
        //Returns a new Vector3 formed by dividing x and z over d, and normalising
        return (new Vector3( (int) (continuous_vector.x / d), 0.0f, (int) (continuous_vector.z / d)  )).normalized;
        //Dividing x and z over the larger of the two ensures one of the values is exactly 1
        //Normalising ensures the product vector is of length 1, preventing diagonal movement from being faster
    }
}
