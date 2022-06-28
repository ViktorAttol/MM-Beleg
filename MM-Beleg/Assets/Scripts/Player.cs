using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour, IEntity
{
    private EntityDimension playerDimension = EntityDimension.PLAYER;
    public PlayerMovement playerMovement;
    public int health = 3;
    
    public GameObject GetDeathEffect()
    {
        throw new System.NotImplementedException();
    }

    public EntityDimension GetDimension()
    {
        return playerDimension;
    }

    public float GetHealth()
    {
        throw new System.NotImplementedException();
    }

    public float GetMoveSpeed()
    {
        throw new System.NotImplementedException();
    }

    public void Move(float _speed)
    {
        playerMovement.Move(_speed);
    }

    public void SetDimenion(EntityDimension dimension)
    {
       //activeDimension = dimension;
    }

    public void SetTarget(Transform _target)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        LevelController.Instance.AddEntityToList(this, EntityDimension.PLAYER);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(this.GameObject());
            //here game over
        }
    }
}
