using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles sound effects. 
/// </summary>
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

    /// <summary>
    /// Plays given audio clip.
    /// </summary>
    /// <param name="sound"> AudioClip to play. </param>
    /// <param name="position"> Position at which to play clip. </param>
    public void PlaySoundEffect(AudioClip sound, Vector3 position)
    {
        AudioSource.PlayClipAtPoint(sound, position, 0.7f);
    }
}
