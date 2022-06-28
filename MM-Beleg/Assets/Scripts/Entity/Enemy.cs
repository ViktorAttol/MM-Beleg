using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour, IEntity
{
    private Transform target;
    private float moveSpeed = 10;
    private float rotationSpeed = 180;
    public Rigidbody2D rb;
    private EntityDimension dimension;
    private int health = 3;
    private EnemyAttack enemyAttack;
    void Start()
    {
        enemyAttack = GetComponent<EnemyAttack>();
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

    public float GetHealth()
    {
        return health;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public void Move(float _scale)
    {
        transform.Rotate(0, 0, -rotationSpeed * _scale * Time.deltaTime);

        Vector2 direction = this.target.position - this.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();

        Vector2 newPos = (Vector2)transform.position + (direction * GetMoveSpeed() * _scale * Time.fixedDeltaTime);

        // Ich habe mir die Rechenleistung im Profiler angeguckt und der war minimal (0.10ms)
        newPos += RepelForce() * Time.fixedDeltaTime;
        rb.MovePosition(newPos);
        enemyAttack.Shoot(_scale);
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
