using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour, Health
{
    Vector3 desired_direction;
    //Creates an object to store a connection to CameraControl
    CameraControl my_camera;
    //Creates an object to store a connection to Animator
    Animator my_animation;
    //Creates an object to store a connection to a ParticleSystem
    ParticleSystem particles;
    //Creates Text objects to store UI text
    Text UI_Health;
    Text UI_Teleport;
    Text UI_Upgrades;
    //Creates variables, to handle character movement
    private float current_speed = 2;
    private float base_speed = 2;
    private float turning_speed = 180;
    private float teleport_cooldown = 2;
    private float teleport_distance = 3;
    //Creates a variable to store character health
    private float charHP = 100;
    //Creates a string value, which stores current animation, which is passed back to Animator to set animations
    private string current_animation;

    // Start is called before the first frame update
    void Start()
    {
        //Retrieves CameraControl for use in this script
        my_camera = FindObjectOfType<CameraControl>();
        //Retrieves Animator for use in this script
        my_animation = GetComponent<Animator>();
        //Retrieves ParticleSystem for use and ensures it is off
        particles = GameObject.Find("CharacterParticles").GetComponent<ParticleSystem>();
        particles.Stop();
        //Retrieves UI text objects for use in this script
        UI_Health = GameObject.Find("UI_Health").GetComponent<Text>();
        UI_Teleport = GameObject.Find("UI_Teleport").GetComponent<Text>();
        UI_Upgrades = GameObject.Find("UI_Upgrades").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        desired_direction = Vector3.zero;

        if(isCharacterMoving())
        {
            my_animation.SetBool(current_animation, true);
            desired_direction = moveTo();

            if (desired_direction.magnitude > 0.1f)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desired_direction, Vector3.up), 0.1f);
                transform.position += current_speed * transform.forward * Time.deltaTime;
            }
        }

        else
        {
            my_animation.SetBool(current_animation, false);
        }

        if (should_disconnect_camera())
        {
            disconnect_camera();
        }


        //If teleport cooldown is below 2 (~2 seconds) adds time.deltaTime to the cooldown
        if (teleport_cooldown < 2)
            teleport_cooldown += Time.deltaTime;

        else if (teleport_cooldown >= 2)
            UI_Teleport.text = "Teleport: Ready";

        //If right mouse is pressed executes a short range teleport, including particle effects
        if (Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.Space))
        {
            //Tests to ensure teleport cooldown is at least 2 (~2 seconds since last teleport)
            if(teleport_cooldown >= 2)
            {
                //Moves the character forward in the new set direction
                //Creates a tempPosition vector storing the x and z values of the intended teleport location
                Vector3 tempPosition = transform.position + (teleport_distance * transform.forward);
                //Samples the y coordinate height of the terrain, adding it onto the tempPosition vector
                //tempPosition.y = terrain.SampleHeight(tempPosition);
                //Moves the character to the new positions
                transform.position = tempPosition;
                //Starts a Coroutine written below, which carries out a particle effect function
                StartCoroutine(ParticleBurst(particles, 0.3f));
                //Returns teleport cooldown to zero
                teleport_cooldown = 0;
                UI_Teleport.text = "Teleport: Waiting...";
            }
        }
    }

    //Referneced from https://answers.unity.com/questions/1546300/stop-a-particle-effect-after-spawn-or-certain-amou.html
    //This Coroutine carries out a particle effect function
    IEnumerator ParticleBurst(ParticleSystem particles, float time)
    {
        //Turns on the Particle System
        particles.Play();
        //Waits for a given amount of time
        yield return new WaitForSeconds(time);
        //Turns off the Particle System
        particles.Stop();
    }

    //This Coroutine carries out a dash function
    IEnumerator Dash(float time)
    {
        //Increases character speed by 20
        base_speed = base_speed + 20;
        //Waits for a given amount of time
        yield return new WaitForSeconds(time);
        //Reduces character speed by 20, back to its original value
        base_speed = base_speed - 20;
    }

    //Returns true if any of WASD are pressed
    private bool isCharacterMoving()
    {
        //If leftShift is held increases movement speeed and changes current animation to running instead of walking
        if (Input.GetKey(KeyCode.LeftShift))
        {
            current_speed = base_speed + 1;
            my_animation.SetBool("is_walking", false);
            current_animation = "is_running";
        }

        //Else sets movespeed to default and changes current animation to walking
        else
        {
            current_speed = base_speed;
            my_animation.SetBool("is_running", false);
            current_animation = "is_walking";
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            return true;
        }
            
        else
        {
            return false;
        }           
    }

    private Vector3 moveTo()
    {
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
            return ((turn_forward() + turn_left()).normalized);

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
            return ((turn_forward() + turn_right()).normalized);

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
            return ((turn_backwards() + turn_left()).normalized);

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
            return ((turn_backwards() + turn_right()).normalized);

        if (Input.GetKey(KeyCode.W))
            return turn_forward();

        if (Input.GetKey(KeyCode.A))
            return turn_left();

        if (Input.GetKey(KeyCode.S))
            return turn_backwards();

        if (Input.GetKey(KeyCode.D))
            return turn_right();

        return Vector3.zero;
    }

    private Vector3 turn_forward()
    {
        Vector3 cam_forward= Camera.main.transform.forward;
        return (new Vector3(cam_forward.x, 0, cam_forward.z)).normalized;
    }

    private Vector3 turn_backwards()
    {
        Vector3 cam_back = -Camera.main.transform.forward;
        return (new Vector3(cam_back.x, 0, cam_back.z)).normalized;
        
    }

    private Vector3 turn_left()
    {
        Vector3 cam_left = -Camera.main.transform.right;
        return (new Vector3(cam_left.x, 0, cam_left.z)).normalized;
    }

    private Vector3 turn_right()
    {
        Vector3 cam_right = Camera.main.transform.right;
        return (new Vector3(cam_right.x, 0, cam_right.z)).normalized;
    }

    private bool should_disconnect_camera()
    {
        return Input.GetKey(KeyCode.Q);
    }

    private void disconnect_camera()
    {
        my_camera.disconnect();
    }

    //Handles on trigger collisions
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger");
        //Based off of https://learn.unity.com/tutorial/collecting-scoring-and-building-the-game
        //Allows the player to pick up items with the PickUp tag, which currently handles a movement speed upgrade
        if (other.tag == "PickUp")
        {
            if (other.name == "SpeedUpgrade")
            {
                base_speed = base_speed + 1;
                UI_Upgrades.text += "Speed\n";
            }

            if (other.name == "TeleportUpgrade")
            {
                teleport_distance += 1;
                UI_Upgrades.text += "Teleport\n";
            }

            other.gameObject.SetActive(false);
        }
    }

    public void setHP(float HP)
    {
        charHP = HP;
    }

    public float getHP()
    {
        return charHP;
    }

    public void reduceHP(float reduction)
    {
        charHP -= reduction;
    }

    public void increaseHP(float increase)
    {
        charHP += increase;
    }
}
