using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Instantiates Enemy Entities.
/// </summary>
public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private LevelController levelController;
    public GameObject enemy1;
    private int spawnCount = 3;
    private GameObject player;
    private float spawnFrequency = 3f;
    private float spawnTimeCounter = 0f;
    private float timePassed = 0f;
    private List<EntityDimension> enemyDimensions = new List<EntityDimension>();
    
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        levelController.SetEnemySpawnManager(this);
        SetDimensions();
    }

    /// <summary>
    /// Calls Spawn with the rate of timePassed. 
    /// Enemies will be instantiated at increasing rates the longer the level is active.
    /// </summary>
    /// <param name="tickTime"> Time value. </param>
    public void SpawnTick(float tickTime)
    {
        spawnTimeCounter += tickTime;
        timePassed += tickTime;
        if (spawnTimeCounter >= spawnFrequency)
        {
            SpawnEnemy(timePassed);
            spawnTimeCounter = 0f;
        }
    }

    private void SetDimensions()
    {
        foreach (EntityDimension dimension in System.Enum.GetValues(typeof(EntityDimension)))
        {
            if (dimension == EntityDimension.PLAYER) continue;
            enemyDimensions.Add(dimension);
        }
    }
    
    private void SpawnEnemy(float probability)
    {
        int count = 1 + (int) (Mathf.Sqrt(probability)/2);

        EntityDimension spawnDimension = enemyDimensions[Random.Range(0, enemyDimensions.Count)];
        
        for (int j = 0; j < count; j++)
        {
            Vector2 spawnPos = player.GetComponent<Transform>().position;
            spawnPos += Random.insideUnitCircle.normalized * 22;

            GameObject toSpawn = GameObject.Instantiate(enemy1, spawnPos, Quaternion.identity);
            levelController.AddEntityToList(toSpawn.GetComponent<IEntity>(), spawnDimension);
            toSpawn.transform.SetParent(this.transform);
            Enemy enemy = toSpawn.GetComponent<Enemy>();
            enemy.SetTarget(player.transform);
            enemy.SetDimenion(spawnDimension);
            enemy.sprite.GetComponent<SpriteRenderer>().color = DimensionColors.dimensionColors[(int) spawnDimension];
        }
    }
    
    
    private void SpawnWave()
    {
        foreach (EntityDimension dimension in System.Enum.GetValues(typeof(EntityDimension)))
        {
            if(dimension == EntityDimension.PLAYER) continue;
            for (int j = 0; j < spawnCount; j++)
            {
                Vector2 spawnPos = player.GetComponent<Transform>().position;
                spawnPos += Random.insideUnitCircle.normalized * 25;

                GameObject toSpawn = GameObject.Instantiate(enemy1, spawnPos, Quaternion.identity);
                levelController.AddEntityToList(toSpawn.GetComponent<IEntity>(), dimension);
                toSpawn.transform.SetParent(this.transform);
                Enemy enemy = toSpawn.GetComponent<Enemy>();
                enemy.SetTarget(player.transform);
                enemy.SetDimenion(dimension);
                enemy.sprite.GetComponent<SpriteRenderer>().color = DimensionColors.dimensionColors[(int) dimension];
            }
        }
    }
}
