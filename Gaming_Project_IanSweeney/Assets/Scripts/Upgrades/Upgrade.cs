using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Upgrade : MonoBehaviour
{
    public void rotate()
    {
        //Based off of https://learn.unity.com/tutorial/collecting-scoring-and-building-the-game
        //Handles basic rotation for display of floating items
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }

    public abstract void applyUpgrade();
}
