using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    public AudioClip SoundEffectDamage;
    public AudioClip SoundEffectShoot;
    public AudioClip SoundEffectSwapDimension;

    public static SFXPlayer instance;
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

    public void PlaySoundEffect(AudioClip sound, Vector3 position)
    {
        AudioSource.PlayClipAtPoint(sound, position, 0.7f);
    }
}
