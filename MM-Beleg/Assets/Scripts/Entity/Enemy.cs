using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Entity instantiated by SpawnManager. 
/// Moves towards target Player.
/// </summary>
public class Enemy : MonoBehaviour, IEntity
{
    private Transform target;
    private float moveSpeed = 7;
    public Rigidbody2D rb;
    private EntityDimension dimension;
    private int health = 3;
    private EnemyAttack enemyAttack;
    public GameObject sprite;
    public RotateSprite rotateSprite;
    private int pointValueForDestruction = 20;
    
    void OnEnable()
    {
        enemyAttack = GetComponent<EnemyAttack>();
        rotateSprite = sprite.GetComponent<RotateSprite>();
    }

    private void OnDestroy()
    {
        LevelController.Instance.RemoveEntityFromList(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<IEntity>() == null) return;
        IEntity target = other.GetComponent<IEntity>();
        if (target.GetDimension() == EntityDimension.PLAYER)
        {
            target.TakeDamage(1);
        }
    }

    private Vector2 RepelForce()
    {
        Vector2 repelForce = Vector2.zero;
        foreach (Enemy enemy in FindObjectsOfType<Enemy>())
        {
            if (Vector3.Distance(enemy.transform.position, transform.position) < 3.5f)
            {
                repelForce += (rb.position - (Vector2)enemy.GetComponent<Transform>().position).normalized;
            }
        }
        return repelForce;
    }

    /// <inheritdoc />
    public int GetHealth()
    {
        return health;
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
    }

    /// <inheritdoc />
    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    /// <inheritdoc />
    public void Move(float scale)
    {
        rotateSprite.Rotate(scale);
        Vector2 direction = this.target.position - this.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
        Vector2 newPos = (Vector2)transform.position + (direction * GetMoveSpeed() * scale);

        // Ich habe mir die Rechenleistung im Profiler angeguckt und der war minimal (0.10ms)
        newPos += RepelForce() * scale;
        rb.MovePosition(newPos);
        enemyAttack.Shoot(scale);
    }

    /// <inheritdoc />
    public void SetTarget(Transform _target)
    {
        this.target = _target;
    }

    /// <inheritdoc />
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(this.GameObject());
            // ist nicht in OnDestroy() weil man sonst bei Player-Damage Punkte bekommen w�rde
            LevelController.Instance.AddPoints(pointValueForDestruction);
            // ist nicht in OnDestory() weil es sonst fehler wirft
            EffectsManager.instance.SpawnEffect(EffectsManager.instance.EnemyDeathEffect, this.transform.position, 8f);
        }
    }
}
