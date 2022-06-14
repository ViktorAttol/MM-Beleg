using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private LevelController controller;
    public GameObject enemy1;
    public int spawnCount = 3;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        SpawnWave();
    }
    private void SpawnWave()
    {
        foreach (EntityDimension dimension in System.Enum.GetValues(typeof(EntityDimension)))
        {
            for (int j = 0; j < spawnCount; j++)
            {
                Vector2 spawnPos = player.GetComponent<Transform>().position;
                spawnPos += Random.insideUnitCircle.normalized * 20;

                GameObject toSpawn = GameObject.Instantiate(enemy1, spawnPos, Quaternion.identity);
                controller.AddEntityToList(toSpawn.GetComponent<IEntity>(), dimension);
                toSpawn.transform.SetParent(this.transform);
                toSpawn.GetComponent<Enemy>().SetTarget(player.transform);
                toSpawn.GetComponent<Enemy>().SetDimenion(dimension);
            }
        }
    }

}
