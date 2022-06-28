using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour, IEntity
{
    EntityDimension dimension;
    public int bulletDamage = 1;
    private EntityDimension targetDimension;
    public bool targetIsEnemy = true;
    private float bulletSpeed = 800f;
    private Rigidbody2D thisBody;
    private float lifeDistance = 100f;
    
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
            if (other.GetComponent<Enemy>() != null || other.GetComponent<Player>() != null)
            {
                target.TakeDamage(bulletDamage);
                Destroy(this.GameObject());
            }
            // hit other bullets code here
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
        //transform.position += transform.right * _speed * bulletSpeed * Time.deltaTime;
        //thisBody.AddForce(transform.up * _speed * bulletSpeed, ForceMode2D.Impulse);
        thisBody.velocity = ((transform.right).normalized) * _speed * bulletSpeed * Time.fixedDeltaTime ;
        lifeDistance -= _speed;
        if(lifeDistance <= 0) Destroy(this.GameObject());
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

    public void TakeDamage(int damage)
    {
        throw new NotImplementedException();
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
        thisBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
