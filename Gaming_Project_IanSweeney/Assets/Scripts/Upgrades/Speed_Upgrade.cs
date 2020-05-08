using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed_Upgrade : Upgrade
{
    CharacterMovement player = new CharacterMovement();
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        rotate();
    }

    public override void applyUpgrade()
    {
        player.increaseSpeed(1f);
    }
}
