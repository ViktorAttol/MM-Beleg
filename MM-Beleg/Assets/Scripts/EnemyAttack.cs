using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Handles shooting funtionality of Enemy Entity.
/// </summary>
public class EnemyAttack : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject firePoint;
    private float fireRate = 1f;
    private float nextShoot = 0f;
    private Enemy enemy;
   
    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    /// <summary>
    /// Instantiates a bullet with EntityDimension of this Entity.
    /// FireRate reduced when this EntityDimension is inactive.
    /// </summary>
    /// <param name="_scale"> Scaling multiplier for fireRate. </param>
    public void Shoot(float _scale)
    {
        nextShoot += _scale;
        if(nextShoot >= fireRate && Random.Range(0, 100) > 90)
        {
            nextShoot = 0f;
            GameObject bullet = GameObject.Instantiate(bulletPrefab);
            bullet.transform.position = firePoint.transform.position;
            bullet.transform.rotation = firePoint.transform.rotation;
            Bullet activeBullet = bullet.GetComponent<Bullet>();
            activeBullet.SetDimenion(enemy.GetDimension());
            activeBullet.SetTargetDimension(EntityDimension.PLAYER);
            LevelController.Instance.AddEntityToList(activeBullet, enemy.GetDimension());
        }
    }
}
