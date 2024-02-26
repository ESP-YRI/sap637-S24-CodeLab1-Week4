using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
//using File = System.IO.File;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance;
    public TextMeshProUGUI screenText;

    public int score = 0;
    
    const string DATA_DIR = "/DATA/";
    const string DATA_HS_FILE = "highScores.txt";
    string FULL_FILE_PATH;

    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;

        }
        
    }
    
    string highScoresString = "";
    List<int> highScores;

    public List<int> Highscores
    {
        get
        {
            //if there arent any values in the highScores list, and the file we store those high scores in exists
            if (highScores == null && File.Exists(FULL_FILE_PATH))
            {
                Debug.Log("File is here!");
                
                //makes a new list of high scores
                highScores = new List<int>();

                //sets the highScoresString to read everything in the highScores.txt file
                highScoresString = File.ReadAllText(FULL_FILE_PATH);
                
                //cuts off white space at the top and bottom of a string
                //so, tabs, \n's, etc
                highScoresString = highScoresString.Trim();
                
                //make an array of strings by splitting the highScoresString into multiple strings, using
                //line breaks to determine the point of splittin
                string[] highScoreArray = highScoresString.Split("\n");
                
                //iterates through the array of high scores (which were read from the txt file)
                for (int i = 0; i < highScoreArray.Length; i++)
                {
                    //turns each string highscore into an integer
                    int currentScore = Int32.Parse(highScoreArray[i]);
                    
                    //adds it to the highScores list 
                    highScores.Add(currentScore);
                }
            }
            
            //if there are no high scores AND no file to read them from, 
            else if(highScores == null)
            {
                Debug.Log("File NOT here");
                //generates a default list of high scores 3, 2, 1, and 0
                highScores = new List<int>();
                highScores.Add(3);
                highScores.Add(2);
                highScores.Add(1);
                highScores.Add(0);
            }
            
            return highScores;
        }
        
    }
    
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

    private void Start()
    {
        FULL_FILE_PATH = Application.dataPath + DATA_DIR + DATA_HS_FILE;
        Debug.Log("File Path = " + FULL_FILE_PATH);
    }

    private float timer = 0;
    public int maxTime = 5;
    private bool inGame = true;
    
    private void Update()
    {
        //if the game is being played, displays the current score and remaining time
        if (inGame)
        {
            screenText.text = "Time Left: " + (maxTime- (int)timer) + "\nApples Eaten: " + score;
        }
        //if the game is over, prints the final score and all the high scores
        else
        {
            screenText.text = "All Appled Out!\n" +"\nFinal Total: " + score + "\n" + "\nRecords:\n" + highScoresString;
        }

        //increments timer with the time elapsed between frames
        timer += Time.deltaTime;

        //once the timer runs out, the game ends, goes to the end screen, and checks for a high score
        if (timer >= maxTime && inGame)
        {
            inGame = false;
            SceneManager.LoadScene("End");
            Debug.Log("Setting high score...");
            SetHighScore();
        }
        
    }

    bool IsHighScore(int score)
    {
        for (int i = 0; i < Highscores.Count; i++)
        {
            if (highScores[i] < score)
            {
                return true;
            }
        }

        return false;
    }

    void SetHighScore()
    {
        //if the score the player got IS a high score:
        if (IsHighScore(score))
        {
            int highScoreSlot = -1;

            //goes through the entire HighScores list to figure out which slot to put the new highscore in
            for (int i = 0; i < Highscores.Count; i++)
            {
                if (score > highScores[i])
                {
                    highScoreSlot = i;
                    break;
                }
            }
                
                //puts the new high score in the appropriate place in the list
                highScores.Insert(highScoreSlot, score);

                //limits the number of highscores to the top 5
                highScores = highScores.GetRange(0, 5);
                
                string scoreboardText = "";

                //adds every high score in the highScores to scoreboardText, separated by a \n
                foreach (var highScore in highScores)
                {
                    scoreboardText += highScore + "\n";
                }

                //sets highScoresString to scoreboardText
                highScoresString = scoreboardText;
                
                //writes the list of highscores to the highScores.txt file
                File.WriteAllText(FULL_FILE_PATH, highScoresString);
                Debug.Log(highScoresString);
        }
    }
    
}

