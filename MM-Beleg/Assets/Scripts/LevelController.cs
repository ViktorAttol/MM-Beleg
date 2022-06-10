using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private string[] dimensions = { "blue", "red" };
    // public EnemySpawnManager enemySpawnManager;
    public LinkedList<IEntity>[] entities;

    private void OnEnable()
    {
        InitDimensions();
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

    public void AddEntityToList(IEntity _entity, int dimension)
    {
        if (dimension < 0 || dimension >= entities.Length)
        {
            return;
        }

        entities[dimension].AddLast(_entity);
    }

    public string[] GetDimensions()
    {
        return dimensions;
    }

    private void InitDimensions()
    {
        entities = new LinkedList<IEntity>[dimensions.Length];
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
