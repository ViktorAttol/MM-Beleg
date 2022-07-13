using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class LevelDataHandler
{
    public static string levelName;
    public static int difficulty; // Todo rework if nesessary

    public static List<string> unlockedWeapons;
    public static int lifePoints;
    
    private static int currentPlayerPoints = 1000;

    public static void AddPlayerPoints(int addPoints)
    {
        currentPlayerPoints += addPoints;
    }

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
    
    public static bool unlockedShotgun = false;
    public static bool unlockedMinigun = false;

    public static int additionalLife = 0;

    public static int additionalSpeed = 0;
    //public static 
}


