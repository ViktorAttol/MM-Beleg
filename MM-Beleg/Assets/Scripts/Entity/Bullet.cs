using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Entity instantiated on shoot method of Player or Enemy. 
/// Causes Entities that collide with it to take damage.
/// </summary>
public class Bullet : MonoBehaviour, IEntity
{
    EntityDimension dimension;
    public int bulletDamage = 1;
    private EntityDimension targetDimension;
    public bool targetIsEnemy = true;
    private float bulletSpeed = 800f;
    private Rigidbody2D thisBody;
    private float lifeDistance = 100f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<IEntity>() == null) return;
        IEntity target = other.GetComponent<IEntity>();
        if (target.GetDimension() == targetDimension)
        {
            if (other.GetComponent<Enemy>() != null || other.GetComponent<Player>() != null)
            {
                target.TakeDamage(bulletDamage);
                // Effect in OnDestroy wirf Fehler
                EffectsManager.instance.SpawnEffect(EffectsManager.instance.BulletHitEffect, this.transform.position, 8f);
                Destroy(this.GameObject());
            }
        }
    }

    /// <inheritdoc />
    public EntityDimension GetDimension()
    {
        return dimension;
    }

    /// <inheritdoc />
    public void SetDimenion(EntityDimension dimension)
    {
        this.dimension = dimension;
        GetComponent<SpriteRenderer>().color = DimensionColors.dimensionColors[(int)dimension];
    }

    /// <inheritdoc />
    public void Move(float scale)
    {
        thisBody.velocity = ((transform.right).normalized) * scale * bulletSpeed  ;
        lifeDistance -= scale;
        if(lifeDistance <= 0) Destroy(this.GameObject());
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
    
    void Start()
    {
        thisBody = GetComponent<Rigidbody2D>();
    }

    public int GetHealth()
    {
        throw new System.NotImplementedException();
    }

    public float GetMoveSpeed()
    {
        throw new System.NotImplementedException();
    }

    public void SetTarget(Transform _target)
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(int damage)
    {
        throw new System.NotImplementedException();
    }
}
