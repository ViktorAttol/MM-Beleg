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
    
    private static int currentPlayerPoints = 0;

    public static void SetPlayerPoints(int addPoints)
    {
        currentPlayerPoints += addPoints;
    }

    public static int GetCurrentPlayerPoints()
    {
        return currentPlayerPoints;
    }
    
    public static bool shotgun = false;
    public static bool minigun = false;
    //public static 
}


