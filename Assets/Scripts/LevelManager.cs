using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static int level=1;
    // Start is called before the first frame update
    void Start()
    {
        string levelName = SceneManager.GetActiveScene().name;

        level= int.Parse(levelName.Replace("Level",""));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextLevel()
    {
        if (level == 3)
            level = 0;
        level++;
        SceneManager.LoadScene("Level "+level.ToString());


    }
    
}
