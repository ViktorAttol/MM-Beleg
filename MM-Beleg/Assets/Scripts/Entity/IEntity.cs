using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntity
{
    public GameObject GetDeathEffect();
    public int GetHealth();
    public EntityDimension GetDimension();
    public void SetDimenion(EntityDimension dimension);
    public float GetMoveSpeed();
    public bool IsDead()
    {
        return GetHealth() <= 0;
    }
    public void Move(float scale);
    public void SetTarget(Transform _target);

    public void TakeDamage(int damage);

}
