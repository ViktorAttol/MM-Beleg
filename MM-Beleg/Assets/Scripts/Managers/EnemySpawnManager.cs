using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private LevelController levelController;
    public GameObject enemy1;
    private int spawnCount = 3;
    private GameObject player;
    private float spawnFrequency = 3f;
    private float spawnTimeCounter = 0f;
    private float timePassed = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        levelController.SetEnemySpawnManager(this);
        SpawnWave();
    }

    public void SpawnTick(float tickTime)
    {
        spawnTimeCounter += tickTime;
        timePassed += tickTime;
        if (spawnTimeCounter >= spawnFrequency)
        {
            SpawnEnemy(1f);
        }
    }

    
    
    private void SpawnEnemy(float probability)
    {
        
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
