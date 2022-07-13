using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
    
    public void OnBuyShotgunBtnCLicked()
    {
        if (LevelDataHandler.unlockedShotgun) return;
        if (LevelDataHandler.SubtractPlayerPoints(costShotgun))
        {
            LevelDataHandler.unlockedShotgun = true;
            shotgunStatus.text = "UNLOCKED";
        }
    }
    
    public void OnBuyMinigunBtnCLicked()
    {
        if (LevelDataHandler.unlockedMinigun) return;
        if (LevelDataHandler.SubtractPlayerPoints(costMinigun))
        {
            LevelDataHandler.unlockedMinigun = true;
            minigunStatus.text = "UNLOCKED";
        }
    }
    
    public void OnBuyHealthBtnCLicked()
    {
        if (LevelDataHandler.additionalLife >= 3) return;
        if (LevelDataHandler.SubtractPlayerPoints(costHealth))
        {
            LevelDataHandler.additionalLife++;
            healthStatus.text = LevelDataHandler.additionalLife + "/3";
        }
    }
    
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
