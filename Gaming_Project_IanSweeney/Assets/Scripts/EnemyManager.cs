using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    int xPos;
    int zPos;
    System.Random random = new System.Random();
    List<GameObject> enemies = new List<GameObject>();
    Animator animationController;

    // Start is called before the first frame update
    void Start()
    {
        placeEnemies(3);
        animationController = FindObjectOfType<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void placeEnemies(int count)
    {
        if(enemies.Count > 0)
        {
            foreach (GameObject enemy in enemies)
                Destroy(enemy);

            enemies.Clear();
        }

        for (int i = 0; i < count; i++)
        {
            xPos = random.Next(0, 7);
            zPos = random.Next(0, 7);

            //Creates a vector for the position of the upgrade
            Vector3 enemyPosition = new Vector3((xPos * 12), 0.5f, (zPos * 12));
            //Loads a prefab of the upgrade based on the random type selected 
            Object enemyPrefab = Resources.Load("SKELETON");
            //Instantiates the upgrade at the chosen position
            GameObject enemy = (GameObject)GameObject.Instantiate(enemyPrefab, enemyPosition, new Quaternion());
            enemies.Add(enemy);
            Debug.Log("Enemy Created");
        }
    }
}
