using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Directory = System.IO.Directory;
using File = System.IO.File;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TextMeshProUGUI screenText;

    private int score = 0;
    List<int> highscores;
    
    const string DATA_DIR = "/Data/";
    const string DATA_HS_FILE = "hs.txt";
    string DATA_FULL_HS_FILE_PATH;

    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            if (IsHighScore(score))
            {
                //-1 is not a slot you can have in a list- that's why we default this int to that
                int highScoreSlot = -1;

                for (int i = 0; i < Highscores.Count; i++)
                {
                    
                    if (score > highScores[i])
                    {
                        highScoreSlot = i;
                        break; //leaves the for loop without continuing to iterate or returning anything
                    }
                    
                }
                //inserts the new score, which IS a high score (because we are still in the if(isHighScore) statement
                //at the correct point in the highScores list, highScoreSlot!
                highScores.Insert(highScoreSlot, score);

                //sets the list's length: starts at the beginning and gives you 5 slots total
                highScores = highScores.GetRange(0, 5);
                
                string scoreBoardText = "";
                
                //goes through everything in the highScores list -> doesn't need a count like a regular for loop!
                //you can't know which spot you're in at a given time, however -> needs a for loop
                foreach (var highScore in highScores)
                {
                    scoreBoardText += highScore + "\n";
                }

                highScoresString = scoreBoardText;
                //saves to file
                //File.WriteAllText(FILE_FULL_PATH, highScoresString);
                File.WriteAllText(DATA_FULL_HS_FILE_PATH, highScoresString);
            
            }
        }
    }
    
    string highScoresString = "";
    List<int> highScores;

    public List<int> Highscores
    {
        get
        {
            //if there arent any values in the highScores list, generate a new list to set it to
            if (highScores == null)
            {
                highScores = new List<int>();

                //inserts a 3 at the 0th slot of the list: this pushes the 0 from above back by one (if it's active)
                highScores.Insert(0, 3);
                highScores.Insert(1, 2);
                highScores.Insert(2, 1);
            }

            /*if (File.Exists(DATA_FULL_HS_FILE_PATH))
            {
                string fileContent = File.ReadAllText(DATA_FULL_HS_FILE_PATH);
                highscore = Int32.Parse(fileContent);
            }*/
            
            highScoresString = File.ReadAllText(DATA_FULL_HS_FILE_PATH);

            //cuts off white space at the top and bottom of a string
            //so, tabs, \n's, etc
            highScoresString = highScoresString.Trim();

            string[] highScoreArray = highScoresString.Split("\n");

            for (int i = 0; i < highScoreArray.Length; i++)
            {
                int currentScore = Int32.Parse(highScoreArray[i]);
                highScores.Add(currentScore);
            }
            
            return highScores;
        }

        
        set
        {
            /*
            highscore = value;
            string fileContent = "" + highscore;*/
            if (!Directory.Exists(Application.dataPath + DATA_DIR))
            {
                Directory.CreateDirectory(Application.dataPath + DATA_DIR);
            }

            if (!File.Exists(DATA_FULL_HS_FILE_PATH))
            {
                Directory.CreateDirectory(Application.dataPath + DATA_DIR);
            }

            /*File.WriteAllText(DATA_FULL_HS_FILE_PATH, fileContent);*/
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
        DATA_FULL_HS_FILE_PATH = Application.dataPath + DATA_DIR + DATA_HS_FILE;
    }

    private void Update()
    {
        screenText.text = "Apples Eaten: " + Score;
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
}
