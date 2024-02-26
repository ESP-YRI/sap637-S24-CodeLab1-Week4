using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.IO;
using Directory = System.IO.Directory;
using File = System.IO.File;

public class GameManagerV2 : MonoBehaviour
{
    public static GameManagerV2 Instance;
    public TextMeshProUGUI scoreText;
    
    private int score = 0;
    private int highScore = 0;
    
    //Score property
    //Checks if the highScore int must be updated only when the score int changes
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value; 
            if (score > HighScore)
            {
                HighScore = score;
            }
        }
    }
    
    //HighScore property
    //Saves the highScore int to /allows it to be retrieved from a file
    public int HighScore
    {
        get
        {
            if (File.Exists(DATA_FULL_HS_FILE_PATH)){
                string fileContent = File.ReadAllText(DATA_FULL_HS_FILE_PATH);
                highScore = Int32.Parse(fileContent);
            }
            
            return highScore;
        }
        set
        {
            highScore = value;
            Debug.Log("New High Score!!");
            string fileContent = "" + highScore; //string ver of highScore int
            
            if (!Directory.Exists(Application.dataPath + DATA_DIR))
            {
                Directory.CreateDirectory(Application.dataPath + DATA_DIR);
            }

            if (!File.Exists(DATA_FULL_HS_FILE_PATH))
            {
                Directory.CreateDirectory(Application.dataPath + DATA_DIR);
            }

            File.WriteAllText(DATA_FULL_HS_FILE_PATH, fileContent);
        }
    }

    const string DATA_DIR = "/Data/";
    const string DATA_HS_FILE = "highScores.txt";
    string DATA_FULL_HS_FILE_PATH;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
           Destroy(gameObject); 
        }
    }

    //sets the path to the file where the highScore is stored
    void Start()
    {
        DATA_FULL_HS_FILE_PATH = Application.dataPath + DATA_DIR + DATA_HS_FILE;
    }

    //Prints the score and highScore on screen
    void Update()
    {
        scoreText.text = "Apples Eaten: " + Score;
    }
}
