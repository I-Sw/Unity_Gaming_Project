using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    CharacterMovement player;
    V2_LevelGenerator levelGen;
    UpgradeManager upgrades;
    EnemyManager enemies;
    UIManager ui;
    int levelsCleared;
    int upgradesRemaining;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<CharacterMovement>();
        levelGen = FindObjectOfType<V2_LevelGenerator>();
        upgrades = FindObjectOfType<UpgradeManager>();
        enemies = FindObjectOfType<EnemyManager>();
        ui = FindObjectOfType<UIManager>();
        levelsCleared = 0;
        upgradesRemaining = 5;
        ui.updateUI(upgradesRemaining);
        levelGen.generateLevel();
        upgrades.placeUpgrades(upgradesRemaining);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void upgradeCollected(string name)
    {
        upgradesRemaining--;
        ui.updateUI(name);
        ui.updateUI(upgradesRemaining);
        if(upgradesRemaining == 0)
        {
            player.transform.position = new Vector3(36, .5f, 36);
            levelGen.regenerateMaze();
            levelsCleared++;
            upgrades.placeUpgrades(5 + levelsCleared);
            upgradesRemaining = 5 + levelsCleared;
            ui.updateUI(upgradesRemaining);
            player.setHP(player.maxHP);
            enemies.placeEnemies(3 + levelsCleared);           
        }
    }

    public void updateUI(float health)
    {
        ui.updateUI(health);
    }

    public void updateUI(bool status)
    {
        ui.updateUI(status);
    }

    public void characterDied()
    {
        ui.died();
    }
}
