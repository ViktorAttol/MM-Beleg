using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Loads a fresh level. 
/// </summary>
public class PlayMenu : MonoBehaviour
{
    public void StartLevelClicked()
    {
        LevelDataHandler.levelName = "level1";
        LevelDataHandler.difficulty = 0;
        LevelDataHandler.unlockedWeapons = null;
        LevelDataHandler.lifePoints = 3;
        SceneManager.LoadScene("Level");
    }
}
