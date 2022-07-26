using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Handles purchasing of items in the shop and related UI elements. 
/// </summary>
public class ShopButtons : MonoBehaviour
{
    private int costShotgun = 500;
    private int costMinigun = 1000;
    private int costHealth = 250;
    private int costSpeed = 250;
    
    public TextMeshProUGUI shotgunStatus;
    public TextMeshProUGUI minigunStatus;
    public TextMeshProUGUI healthStatus;
    public TextMeshProUGUI speedStatus;

    void Start()
    {
        if(LevelDataHandler.unlockedShotgun) shotgunStatus.text = "UNLOCKED";
        if(LevelDataHandler.unlockedMinigun) minigunStatus.text = "UNLOCKED";
        healthStatus.text = LevelDataHandler.additionalLife + "/3";
        speedStatus.text = LevelDataHandler.additionalSpeed + "/3";
    }
    
    /// <summary>
    /// Deducts cost of Shotgun from Points available. 
    /// Unlocks Shotgun use in game.
    /// </summary>
    public void OnBuyShotgunBtnCLicked()
    {
        if (LevelDataHandler.unlockedShotgun) return;
        if (LevelDataHandler.SubtractPlayerPoints(costShotgun))
        {
            LevelDataHandler.unlockedShotgun = true;
            shotgunStatus.text = "UNLOCKED";
        }
    }

    /// <summary>
    /// Deducts cost of Minigun from Points available. 
    /// Unlocks Minigun use in game.
    /// </summary>
    public void OnBuyMinigunBtnCLicked()
    {
        if (LevelDataHandler.unlockedMinigun) return;
        if (LevelDataHandler.SubtractPlayerPoints(costMinigun))
        {
            LevelDataHandler.unlockedMinigun = true;
            minigunStatus.text = "UNLOCKED";
        }
    }

    /// <summary>
    /// Deducts cost of Health from Points available. 
    /// Increases Player Health.
    /// </summary>
    public void OnBuyHealthBtnCLicked()
    {
        if (LevelDataHandler.additionalLife >= 3) return;
        if (LevelDataHandler.SubtractPlayerPoints(costHealth))
        {
            LevelDataHandler.additionalLife++;
            healthStatus.text = LevelDataHandler.additionalLife + "/3";
        }
    }

    /// <summary>
    /// Deducts cost of Spped from Points available. 
    /// Increases Player Speed.
    /// </summary>
    public void OnBuySpeedBtnCLicked()
    {
        if (LevelDataHandler.additionalSpeed >= 3) return;
        if (LevelDataHandler.SubtractPlayerPoints(costSpeed))
        {
            LevelDataHandler.additionalSpeed++;
            speedStatus.text = LevelDataHandler.additionalSpeed + "/3";
        }
    }
}
