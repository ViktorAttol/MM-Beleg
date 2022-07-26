using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Outlines necessary properties for all Entities in game.
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Returns current health level of this Entity.
    /// </summary>
    /// <returns> Health value. </returns>
    public int GetHealth();

    /// <summary>
    /// Returns assigned EntityDimension of this Entity.
    /// </summary>
    /// <returns> EntityDimension. </returns>
    public EntityDimension GetDimension();

    /// <summary>
    /// Sets given EntityDimension as assigned EntityDimension of this Entity.
    /// </summary>
    /// <param name="dimension"> EntityDimension to set. </param>
    public void SetDimenion(EntityDimension dimension);

    /// <summary>
    /// Returns assigned moveSpeed of this Entity.
    /// </summary>
    /// <returns> MoveSpeed. </returns>
    public float GetMoveSpeed();

    /// <summary>
    /// Handles movement functionality of this Entity.  
    /// Movement speed reduced by given scale if this assigned EntityDimension is inactive.
    /// </summary>
    /// <param name="scale"> Scaling multiplier by which to multiply movement speed. </param>
    public void Move(float scale);

    /// <summary>
    /// Sets given Transform as target towards which this Entity moves.
    /// </summary>
    /// <param name="_target"> Transform to set. </param>
    public void SetTarget(Transform _target);

    /// <summary>
    /// Reduces health value of this Entity. 
    /// </summary>
    /// <param name="damage"> Amount by which to decrease Entity health. </param>
    public void TakeDamage(int damage);

    /// <summary>
    /// Returns if the Entity has a non-null health value or not.
    /// </summary>
    /// <returns> True if health is at or blow 0.</returns>
    public bool IsDead()
    {
        return GetHealth() <= 0;
    }
}
