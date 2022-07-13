using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    // public EnemySpawnManager enemySpawnManager;
    public LinkedList<IEntity>[] entities;
    public LinkedList<IEntity> bufferDimensionList;
    public static LevelController Instance;
    private float moveValueCurrentDimension = 1f;
    private float moveValueNotCurrentDimension = 0.5f;
    private bool levelActive = true;
    private EnemySpawnManager enemySpawnManager;

    private int points = 0;

    public int GetPoints()
    {
        return points;
    }

    public void AddPoints(int points)
    {
        this.points += points;
        //LevelDataHandler.currentPlayerPoints += points;
    }
    
    private void OnEnable()
    {
        InitDimensions();
        bufferDimensionList = new LinkedList<IEntity>();
    }

    private void Awake()
    {
        if (Instance != null) {
            Destroy(Instance);
        }
        Instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnGameOver()
    {
        levelActive = false;
        LevelDataHandler.AddPlayerPoints(points);
        ReturnToMenu();
    }
    
    public void SetEnemySpawnManager(EnemySpawnManager spawnManager)
    {
        enemySpawnManager = spawnManager;
    }

    public void SetLevelActive(bool active)
    {
        levelActive = active;
    }

    private void FixedUpdate()
    {
        if(levelActive) GameTick();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void AddEntityToList(IEntity _entity, EntityDimension dimension)
    {
        if (_entity == null)
        {
            return;
        }

        entities[(int) dimension].AddLast(_entity);
    }

    public void RemoveEntityFromList(IEntity _entity)
    {
        entities[(int)_entity.GetDimension()].Remove(_entity);
    }
    private void InitDimensions()
    {
        entities = new LinkedList<IEntity>[System.Enum.GetValues(typeof(EntityDimension)).Length];
        for (int i = 0; i < entities.Length; i++)
        {
            entities[i] = new LinkedList<IEntity>();
        }
    }

    private void GameTick()
    {
        EntityDimension currDimension = DimensionController.Instance.GetCurrentDimension();
        for (int i = 0; i < entities.Length; i++)
        {
            float moveValue = moveValueNotCurrentDimension;
            if ((int) currDimension == i || i == 3) moveValue = moveValueCurrentDimension;
            moveValue *= Time.fixedDeltaTime;
            bufferDimensionList.AddRange(entities[i]);
            foreach(IEntity entity in bufferDimensionList)
            {
                entity.Move(moveValue);
            }
            bufferDimensionList.Clear();
        }
        enemySpawnManager.SpawnTick(Time.fixedDeltaTime);
    }

    public void KillAllEntities()
    {
        foreach (LinkedList<IEntity> entityList in entities)
        {
            foreach (IEntity entity in entityList)
            {
                if (entity is Player) continue;
                GameObject.Destroy(((MonoBehaviour)entity).gameObject);
            }
        }
    }
}
