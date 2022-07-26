using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages effects used in game. 
/// </summary>
public class EffectsManager : MonoBehaviour
{
    public GameObject PlayerDamageEffect;
    public GameObject BulletHitEffect;
    public GameObject EnemyDeathEffect;
    public static EffectsManager instance;
    public RipplePostProcessor Ripple;
    
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    /// <summary>
    /// Instantiates effect at given position for set time.
    /// </summary>
    /// <param name="effect"> Original effect to be copied. </param>
    /// <param name="position"> Position for new effect. </param>
    /// <param name="duration"> Length of time in seconds until effect is destroyed. </param>
    public void SpawnEffect(GameObject effect, Vector3 position, float duration)
    {
        GameObject fx = GameObject.Instantiate(effect, position, Quaternion.identity);
        Destroy(fx, duration);
    }

    /// <summary>
    /// Creates a ripple effect on screen. 
    /// </summary>
    /// <param name="strength"> The strength of the ripple effect. </param>
    public void RippleEffect(float strength)
    {
        int x = (Screen.width / 2);
        int y = (Screen.height / 2);
        Ripple.RippleEffect(x, y, strength);
    }
}
