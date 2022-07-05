using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject firePoint;

    void Start()
    {
        
    }

    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab);
            //bullet.transform.parent = transform;
            bullet.transform.position = firePoint.transform.position;
            bullet.transform.rotation = firePoint.transform.rotation;
            //bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.transform.up * 30, ForceMode2D.Impulse);
            Bullet activeBullet = bullet.GetComponent<Bullet>();
            activeBullet.SetDimenion(DimensionController.Instance.GetCurrentDimension());
            activeBullet.SetTargetDimension(DimensionController.Instance.GetCurrentDimension());
            LevelController.Instance.AddEntityToList(activeBullet, activeBullet.GetDimension());
        }
    }
}
