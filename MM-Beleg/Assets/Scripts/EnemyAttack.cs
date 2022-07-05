using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

    void Update()
    {
      
    }

    public void Shoot(float _scale)
    {
        nextShoot += _scale;
        if(nextShoot >= fireRate && Random.Range(0, 100) > 90)
        {
            nextShoot = 0f;
            GameObject bullet = GameObject.Instantiate(bulletPrefab);
            //bullet.transform.parent = transform;
            bullet.transform.position = firePoint.transform.position;
            bullet.transform.rotation = firePoint.transform.rotation;
            //bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.transform.up * 30, ForceMode2D.Impulse);
            Bullet activeBullet = bullet.GetComponent<Bullet>();
            activeBullet.SetDimenion(enemy.GetDimension());
            activeBullet.SetTargetDimension(EntityDimension.PLAYER);
            LevelController.Instance.AddEntityToList(activeBullet, enemy.GetDimension());
        }
    }
}
