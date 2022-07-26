using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Facilitates information transfer between scenes. 
/// It contains information regarding player points and unlocked weapons and upgrades. 
/// </summary>
public static class LevelDataHandler
{
    public static string levelName;
    public static int difficulty;
    public static List<string> unlockedWeapons;
    public static int lifePoints;
    private static int currentPlayerPoints = 1000;
    public static bool unlockedShotgun = false;
    public static bool unlockedMinigun = false;
    public static int additionalLife = 0;
    public static int additionalSpeed = 0;

    /// <summary>
    /// Adds points to usuable total for shop purchases. 
    /// Called on level end for payout of collected points.
    /// </summary>
    /// <param name="addPoints"> Point value to be added. </param>
    public static void AddPlayerPoints(int addPoints)
    {
        currentPlayerPoints += addPoints;
    }

    /// <summary>
    /// Removes points from usable total for shop purchases.
    /// Called on purchase of shop item.
    /// </summary>
    /// <param name="subtractPoints"> Point value to be subtracted. </param>
    /// <returns> false if amount requested to subtract is more than currect points available.
    ///           true if purchase is valid. </returns>
    public static bool SubtractPlayerPoints(int subtractPoints)
    {
        if (currentPlayerPoints < subtractPoints) return false;
        currentPlayerPoints -= subtractPoints;
        return true;
    } 

    public static int GetCurrentPlayerPoints()
    {
        return currentPlayerPoints;
    }
}


