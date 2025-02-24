using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PerformanceCatcher : MonoBehaviour
{
    private float timeForLevel;
    private float timeForTechnique;
    private float timeTotal;

    private int shotsTaken;
    private int shotsHit;
    private double accuracy;

    private int ouchRock;
    private int ouchFire;

    private String days = System.DateTime.Now.Day.ToString();
    private String hours = System.DateTime.Now.Hour.ToString();
    private String minutes = System.DateTime.Now.Minute.ToString();
    private String seconds = System.DateTime.Now.Second.ToString();
    private String time3;

    private bool firsttime = true;
    private LinkedList<string> performanceStrings = new LinkedList<string>();

    #region PerformanceCatcher
    private static PerformanceCatcher _instance;
    public static PerformanceCatcher Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("PerformanceCatcher is NUll");
            }
            return _instance;
        }
    }
    void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(this.gameObject);

        SceneManager.activeSceneChanged += ChangedActiveScene;
    }
    #endregion
    private void Start()
    {
        timeForLevel = 0;
        timeForTechnique = 0;
        timeTotal = 0;
        shotsTaken = 0;
        shotsHit = 0;
        ouchFire = 0;
        ouchRock = 0;
        accuracy = 0;
        performanceStrings.AddLast("This is the Performance Data for:");

        time3 = days + "_" + hours + "_" + minutes + "_" + seconds;
    }

    private void ChangedActiveScene(Scene current, Scene next)
    {

        if (next.name == "Initializer")
        {

        }
        else if (next.name == "StartingScene")
        {

            if (firsttime)
            {
                addControlLine(GameManager.Instance.Steuerung);
                firsttime = false;
            }
            else
            {
                timeForTechnique += timeForLevel;
                timeTotal += timeForTechnique;
                performanceStrings.AddLast("Level 3 / Time: " + timeForLevel.ToString());
                performanceStrings.AddLast("Level 3 / Shots: " + shotsTaken.ToString());
                performanceStrings.AddLast("Level 3 / Hits: " + shotsHit.ToString());
                performanceStrings.AddLast("Level 3 / Accuracy: " + computeAccuracy().ToString());
                performanceStrings.AddLast("Level 3 / ouchRock: " + ouchRock.ToString());
                performanceStrings.AddLast("Level 3 / ouchFire: " + ouchFire.ToString());
                performanceStrings.AddLast("Total Time Technique: " + timeForTechnique.ToString());
                addEmptyLine();
                timeForTechnique = 0;

                if (!GameManager.Instance.getAlleSteuerungenDurch())
                    addControlLine(GameManager.Instance.Steuerung);
            }

            if (GameManager.Instance.getAlleSteuerungenDurch())
            {
                performanceStrings.AddLast("Total Time: " + timeTotal.ToString());
                createPerformanceReview();
            }
        }
        else if (next.name == "Level_1")
        {

        }
        else if (next.name == "Level_2")
        {
            timeForTechnique += timeForLevel;
            performanceStrings.AddLast("Level 1 / Time: " + timeForLevel.ToString());
            performanceStrings.AddLast("Level 1 / Shots: " + shotsTaken.ToString());
            performanceStrings.AddLast("Level 1 / Hits: " + shotsHit.ToString());
            performanceStrings.AddLast("Level 1 / Accuracy: " + computeAccuracy().ToString());
        }
        else if (next.name == "Level_3")
        {
            timeForTechnique += timeForLevel;
            performanceStrings.AddLast("Level 2 / Time: " + timeForLevel.ToString());
        }

        accuracy = computeAccuracy();

        timeForLevel = 0;
        shotsTaken = 0;
        shotsHit = 0;
        ouchFire = 0;
        ouchRock = 0;
    }


    void Update()
    {
        timeForLevel += Time.deltaTime;
    }

    public void createPerformanceReview()
    {
        CreateNewTextFile();
    }

    private void CreateNewTextFile()
    {
        //using (StreamWriter sw = File.CreateText(Application.dataPath + "/UserStatistics/NewTextFile.txt"))
        using (StreamWriter sw = File.CreateText(Application.dataPath + "/UserStatistics/PerformanceReport-" + time3 + ".txt"))
        {
            for (int i = 0; i < performanceStrings.Count; i++)
            {
                sw.WriteLine(performanceStrings.ElementAt(i));
            }
        }

        AssetDatabase.Refresh();
    }

    private double computeAccuracy()
    {
        if (shotsTaken == 0)
        {
            return 0;
        }
        else
        {
            return (double) shotsHit / (double) shotsTaken;
        }
    }

    public void getReihenfolgeAsStrings()
    {
        string temp = "Code: ";
        int[] reihenfolge = GameManager.Instance.Reihenfolge;
        for (int i = 0; i < reihenfolge.Length; i++)
        {
            temp += reihenfolge[i] + " ";
        }
        performanceStrings.AddLast(temp);
    }

    public void addControlLine(int i)
    {
        performanceStrings.AddLast("Current Control Technique:");
        if (i == 1)
        {
            performanceStrings.AddLast("Claw Grip");
        }
        else if (i == 2)
        {
            performanceStrings.AddLast("Foot Pedal");
        }
        else if (i == 3)
        {
            performanceStrings.AddLast("Back Button");
        }
        else if (i == 4)
        {
            performanceStrings.AddLast("Tilt Control");
        }
    }

    public void addEmptyLine()
    {
        performanceStrings.AddLast("");
    }

    public void incShotsTaken()
    {
        shotsTaken++;
    }

    public void incShotsHit()
    {
        shotsHit++;
    }

    public void incOuchRock()
    {
        ouchRock++;
    }

    public void incOuchFire()
    {
        ouchFire++;
    }
}
