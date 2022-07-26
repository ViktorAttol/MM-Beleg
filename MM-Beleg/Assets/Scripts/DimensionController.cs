using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Manages EntityDimension changes. 
/// </summary>
public class DimensionController : MonoBehaviour
{
    public static DimensionController Instance;
    private EntityDimension currentDimension = EntityDimension.RED;
    private Transform player;
    
    void Start()
    {
        currentDimension = EntityDimension.RED;
        if (Instance != null) {
            Destroy(Instance);
        }
        Instance = this;

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetDimension(EntityDimension.RED);
        } else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetDimension(EntityDimension.GREEN);

        } else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetDimension(EntityDimension.BLUE);
        } 
    }

    void SetDimension(EntityDimension dimension)
    {
        currentDimension = dimension;
        EffectsManager.instance.RippleEffect(0.3f);
        SFXPlayer.instance.PlaySoundEffect(SFXPlayer.instance.SoundEffectSwapDimension, player.position);
    }

    public EntityDimension GetCurrentDimension()
    {
        return currentDimension;
    }
}
