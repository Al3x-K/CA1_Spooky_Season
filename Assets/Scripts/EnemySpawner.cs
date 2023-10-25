using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject enemy; 
    [SerializeField] public float spawnRate = 2f;
    [SerializeField] public Transform spawnPoint;

    [Header("Limit Settings")]
    [SerializeField] private int maxEnemies = 3; 
    private int currentEnemies = 0; 

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", spawnRate, spawnRate);
    }
    void SpawnEnemy()
    {
        if(currentEnemies >= maxEnemies) return; 
        GameObject spawnedEnemy = Instantiate(enemy, spawnPoint.position, Quaternion.identity);
    
        EnemyController enemyController = spawnedEnemy.GetComponent<EnemyController>();
        enemyController.Initialize(this);

        currentEnemies++;
    }

    public void EnemyDied()
    {
        currentEnemies--;
    }
}
