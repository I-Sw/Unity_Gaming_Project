# Unity_Gaming_Project
Unity Gaming Project for Semester 2 of Year 2

Built around a central GameManager.cs script
This script interacts with other scripts to for the basis of the Observer pattern.
CharacterMovement.cs and CameraControl.cs control the character and attached camera movement.
Frame Rate independence is displayed throughout the code.
EnemyControl.cs makes use for the state pattern in its control of enemies.
The Health interface is used by both the CharacterMovement and EnemyControl scripts (though enemies cannot be damaged)
The Abstract class Upgrade is used alongside inheriting clases to display and make use of polymorphism inside of the GameManager when applying upgrades.

The Game is based around a proceduraly generated maze with a number of enemies and upgrades.
Once all the upgrades are collected the maze will regenerate repeatedly with an increasing number of enemies and upgrades each time.
If the players health reaches 0, the game will be over.
