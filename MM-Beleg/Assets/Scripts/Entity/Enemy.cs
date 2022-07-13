using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

    public int points = 1;
    
    void OnEnable()
    {
        enemyAttack = GetComponent<EnemyAttack>();
        rotateSprite = sprite.GetComponent<RotateSprite>();
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        LevelController.Instance.RemoveEntityFromList(this);
    }

    public GameObject GetDeathEffect()
    {
        throw new System.NotImplementedException();
    }

    public EntityDimension GetDimension()
    {
        return dimension;
    }

    public int GetHealth()
    {
        return health;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
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

    public void Move(float scale)
    {
        //transform.Rotate(0, 0, -rotationSpeed * _scale * Time.deltaTime);
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

    public void SetTarget(Transform _target)
    {
        this.target = _target;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(this.GameObject());
            // ist nicht in OnDestroy() weil man sonst bei Player-Damage Punkte bekommen w�rde
            LevelController.Instance.AddPoints(points);
            // ist nicht in OnDestory() weil es sonst fehler wirft
            EffectsManager.instance.SpawnEffect(EffectsManager.instance.EnemyDeathEffect, this.transform.position, 8f);
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

    public void SetDimenion(EntityDimension dimension)
    {
        this.dimension = dimension;
    }
}
