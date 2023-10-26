using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//the script was almost fully taken from Naoise's class
//with changes made in the unity editor
//and the fact that I have only 1 spawn Point per each type of enemy
public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject enemy; 
    [SerializeField] public float spawnRate = 2f;
    [SerializeField] public Transform spawnPoint;

    [Header("Limit Settings")]
    [SerializeField] private int maxEnemies = 3; 
    private int currentEnemies = 0; 

    void Start()
    {
        InvokeRepeating("SpawnEnemy", spawnRate, spawnRate); //repeats the spawn function in 2 seconds, every 2 seconds
    }
    void SpawnEnemy()
    {
        if(currentEnemies >= maxEnemies) return; //if there is a max number of enemies already it stops spawning
        GameObject spawnedEnemy = Instantiate(enemy, spawnPoint.position, Quaternion.identity); //allows me to spawn new objects in the scene
    
        EnemyController enemyController = spawnedEnemy.GetComponent<EnemyController>();
        enemyController.Initialize(this);

        currentEnemies++; //adds 1 to the number of current enemies
    }

    public void EnemyDied()
    {
        currentEnemies--; //substracts 1 from the number of current enemies
    }
}
