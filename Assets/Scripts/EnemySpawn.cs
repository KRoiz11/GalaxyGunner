using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject Enemy;

    //max no. of enemies
    public int initialEnemyLimit;
    public int currentEnemyLimit;
    //enemeies added after each wave
    public int additionalEnemies;


    //range of x and z values
    public float xPositionMin;
    public float xPositionMax;
    public float zPositionMin;
    public float zPositionMax;

    //references
    private RoundManager roundManager;


    // Start is called before the first frame update
    void Start()
    {
        currentEnemyLimit = initialEnemyLimit;
        StartCoroutine(EnemySpawner());

        roundManager = GameObject.FindObjectOfType<RoundManager>();
    }

    IEnumerator EnemySpawner()
    {   
        while (true)
        {
            //current no. of enemies
            int enemyCount = 0;

            while (enemyCount < currentEnemyLimit)
            {   
                //generates random x and z value
                float xPosition = Random.Range(xPositionMin,xPositionMax);
                float zPosition = Random.Range(zPositionMin,zPositionMax);

                //activates enemy game object
                Enemy.SetActive(true);

                //spawns in enemy
                Instantiate (Enemy, new Vector3(xPosition, 3, zPosition), Quaternion.identity);

                //time betwween each enemy spawn
                yield return new WaitForSeconds(0.05f);
                enemyCount += 1;
                //deactivates enemy game object
                Enemy.SetActive(false);

            }
            //wait for all enemies to be killed before restarting
            yield return new WaitUntil(()=> GameObject.FindGameObjectsWithTag("Enemy").Length == 0);

            //increments the enemy limit
            currentEnemyLimit += additionalEnemies;

            if (roundManager != null) 
            {
                roundManager.IncreaseRound();
            }
        }
    }
}
