using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour, IEntity
{
    EntityDimension dimension;
    private EntityDimension targetDimension;
    public bool targetIsEnemy = true;
    public GameObject GetDeathEffect()
    {
        throw new System.NotImplementedException();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<IEntity>() == null) return;
        IEntity target = other.GetComponent<IEntity>();
        if (target.GetDimension() == targetDimension)
        {
            Destroy(other.GameObject());
            Destroy(this.GameObject());
        }
    }

    public EntityDimension GetDimension()
    {
        return dimension;
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
        //throw new System.NotImplementedException();
    }

    public void SetDimenion(EntityDimension dimension)
    {
        this.dimension = dimension;
        GetComponent<SpriteRenderer>().color = DimensionColors.dimensionColors[(int) dimension];
    }
    

    public void SetTarget(Transform _target)
    {
        throw new System.NotImplementedException();
    }

    public void SetTargetDimension(EntityDimension target)
    {
        targetDimension = target;
    }

    public EntityDimension GetTargetDimension()
    {
        return targetDimension;
    }

    private void OnDestroy()
    {
        LevelController.Instance.RemoveEntityFromList(this);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
