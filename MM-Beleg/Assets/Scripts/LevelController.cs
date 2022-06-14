using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    // public EnemySpawnManager enemySpawnManager;
    public LinkedList<IEntity>[] entities;
    public static LevelController instance;

    private void OnEnable()
    {
        InitDimensions();
    }

    private void Awake()
    {
        if (instance != null) {
            Destroy(instance);
        }
        instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        MoveEntities();
    }

    public void ReturnToMenuClicked()
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

    private void MoveEntities()
    {
        foreach (LinkedList<IEntity> list in entities)
        {
            foreach(IEntity entity in list)
            {
                entity.Move(1f);
            }
        }
    }
}
