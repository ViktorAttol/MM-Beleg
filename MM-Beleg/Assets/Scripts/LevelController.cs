using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages entity creation, movement, destruction in level, 
/// and keeps track of points collected by player for current run.
/// </summary>
public class LevelController : MonoBehaviour
{
    public LinkedList<IEntity>[] entities;
    public LinkedList<IEntity> bufferDimensionList;
    public static LevelController Instance;
    private float moveValueCurrentDimension = 1f;
    private float moveValueNotCurrentDimension = 0.5f;
    private bool levelActive = true;
    private EnemySpawnManager enemySpawnManager;
    private int points = 0;
    
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

    /// <summary>
    /// Sets level as inactive, pays out collected points to player for use in shop,
    /// Returns to main menu.
    /// </summary>
    public void OnGameOver()
    {
        levelActive = false;
        LevelDataHandler.AddPlayerPoints(points);
        ReturnToMenu();
    }

    public int GetPoints()
    {
        return points;
    }

    /// <summary>
    /// Increases collected point value.
    /// Called when Enemy defeated. 
    /// </summary>
    /// <param name="points"> value of points to add to total collected. </param>
    public void AddPoints(int points)
    {
        this.points += points;
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

    /// <summary>
    /// Loads the Main Menu scene.
    /// </summary>
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Adds given Entity to list of entities, sorted by dimension.
    /// </summary>
    /// <param name="_entity"> Entity to be added to the level. </param>
    /// <param name="dimension"> Dimension to which the above entity belongs. </param>
    public void AddEntityToList(IEntity _entity, EntityDimension dimension)
    {
        if (_entity == null)
        {
            return;
        }

        entities[(int) dimension].AddLast(_entity);
    }

    /// <summary>
    /// Removes the given Entity from the list of entities. 
    /// </summary>
    /// <param name="_entity"> Entity to be removed. </param>
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

    /// <summary>
    /// Destroys all active GameOjects of non-Player Entity type. 
    /// </summary>
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
