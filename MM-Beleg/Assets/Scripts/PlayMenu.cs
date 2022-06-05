using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMenu : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLevelClicked()
    {
        LevelDataHandler.levelName = "level1";
        LevelDataHandler.difficulty = 0;
        LevelDataHandler.unlockedWeapons = null;
        LevelDataHandler.lifePoints = 3;
        SceneManager.LoadScene("Level");
    }
}
