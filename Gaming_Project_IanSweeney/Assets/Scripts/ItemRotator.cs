using UnityEngine;
using System.Collections;

////Based off of https://learn.unity.com/tutorial/collecting-scoring-and-building-the-game
public class ItemRotator : MonoBehaviour
{ 
    void Update()
    {
        //Handles basic rotation for display of floating items
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }
}
