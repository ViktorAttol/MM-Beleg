using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void SpawnEffect(GameObject effect, Vector3 position, float duration)
    {
        GameObject fx = GameObject.Instantiate(effect, position, Quaternion.identity);
        Destroy(fx, duration);
    }

    public void RippleEffect(float strength)
    {
        int x = (Screen.width / 2);
        int y = (Screen.height / 2);
        Ripple.RippleEffect(x, y, strength);
    }
}
