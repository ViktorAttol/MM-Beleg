using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum Weapon
{
    Rifle, Shotgun, Minigun
}

/// <summary>
/// Controllable Player Entity. 
/// </summary>
public class Player : MonoBehaviour, IEntity
{
    private EntityDimension playerDimension = EntityDimension.PLAYER;
    public PlayerMovement playerMovement;
    private int health = 3;
    private Weapon weapon;

    /// <summary>
    /// Adds Player to game level.
    /// Increases health value by additionalLife purchased in shop.
    /// </summary>
    void Start()
    {
        LevelController.Instance.AddEntityToList(this, EntityDimension.PLAYER);
        health += LevelDataHandler.additionalLife;
    }

    /// <inheritdoc />
    public int GetHealth()
    {
        return health;
    }

    /// <inheritdoc />
    public EntityDimension GetDimension()
    {
        return playerDimension;
    }

    /// <inheritdoc />
    public void Move(float scale)
    {
        playerMovement.Move(scale);
    }

    /// <inheritdoc />
    public void TakeDamage(int damage)
    {
        health -= damage;

        SFXPlayer.instance.PlaySoundEffect(SFXPlayer.instance.SoundEffectDamage, this.transform.position);
        EffectsManager.instance.SpawnEffect(EffectsManager.instance.PlayerDamageEffect, this.transform.position, 8f);
        EffectsManager.instance.RippleEffect(1f);
        LevelController.Instance.KillAllEntities();

        if (health <= 0)
        {
            LevelController.Instance.OnGameOver();
        }
    }

    public Weapon GetCurrentWeapon()
    {
        return this.weapon;
    }

    public void SetCurrentWeapon(Weapon weapon)
    {
        this.weapon = weapon;
    }

    public void SetDimenion(EntityDimension dimension)
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
}
