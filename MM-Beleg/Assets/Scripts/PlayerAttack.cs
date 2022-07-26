using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles shooting functionality of Player Entity.
/// </summary>
public class PlayerAttack : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject firePoint;
    public GameObject firePoint2;
    public GameObject firePoint3;
    private Weapon currWeapon;
    private Player player;
    private List<Weapon> playerWeapons = new List<Weapon>();
    private float minigunShootTime = 0.1f;
    private float currentMinigunShootTime = 0f;
    
    /// <summary>
    /// Adds available weapons to Player Weapon list. 
    /// </summary>
    void Start()
    {
        player = GetComponent<Player>();
        playerWeapons.Add(Weapon.Rifle);
        if(LevelDataHandler.unlockedShotgun) playerWeapons.Add(Weapon.Shotgun);
        if(LevelDataHandler.unlockedMinigun) playerWeapons.Add(Weapon.Minigun);
    }

    void Update()
    {
        SetChangeWeaponInput();
        Shoot();
    }

    /// <summary>
    /// Fires applicable weapon on mouse input.  
    /// </summary>
    void Shoot()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(currWeapon == Weapon.Rifle) ShootRifle();
            else if(currWeapon == Weapon.Shotgun) ShootShotgun();
        }

        if (Input.GetMouseButton(0))
        {
            if (currWeapon == Weapon.Minigun) ShootMinigung();
        }
        
    }

    void ShootMinigung()
    {
        currentMinigunShootTime += Time.deltaTime;
        if (currentMinigunShootTime >= minigunShootTime)
        {
            currentMinigunShootTime = 0;
            ShootBullet(firePoint);
        }
    }
    
    void ShootRifle()
    {
        ShootBullet(firePoint);
    }
    
    void ShootShotgun()
    {
        ShootBullet(firePoint);
        ShootBullet(firePoint2);
        ShootBullet(firePoint3);
    }

    /// <summary>
    /// Instantiates a Bullet at the assigned FirePoint in the currently active EntityDimension.
    /// </summary>
    /// <param name="currFirepoint"> Firepoint at which to instantiate Bullet. </param>
    void ShootBullet(GameObject currFirepoint)
    {
        GameObject bullet = GameObject.Instantiate(bulletPrefab);
        bullet.transform.position = currFirepoint.transform.position;
        bullet.transform.rotation = currFirepoint.transform.rotation;
        Bullet activeBullet = bullet.GetComponent<Bullet>();
        activeBullet.SetDimenion(DimensionController.Instance.GetCurrentDimension());
        activeBullet.SetTargetDimension(DimensionController.Instance.GetCurrentDimension());
        LevelController.Instance.AddEntityToList(activeBullet, activeBullet.GetDimension());

        SFXPlayer.instance.PlaySoundEffect(SFXPlayer.instance.SoundEffectShoot, currFirepoint.transform.position);
    }
  
    /// <summary>
    /// Sets the currently assigned weapon based on keyboard input.
    /// </summary>
    void SetChangeWeaponInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeWeaponRequest(Weapon.Rifle);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            ChangeWeaponRequest(Weapon.Shotgun);
        } 
        else if (Input.GetKeyDown(KeyCode.R))
        {
            ChangeWeaponRequest(Weapon.Minigun);
        }
    }

    void ChangeWeaponRequest(Weapon weapon)
    {
        if (weapon == currWeapon || !playerWeapons.Contains(weapon)) return;
        currWeapon = weapon;
        player.SetCurrentWeapon(currWeapon);
    }
}
