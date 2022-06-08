using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;


using GameAnalyticsSDK;
using System.IO;



public class GameManager : Singleton<GameManager>
{

    public int currentLevel = 1;

    public int currentLevelText = 1;

    public int totalCoins = 0;

    public bool completedTutorial = false;

    public float previousLine = 0;

    public float previousRating = 0;
    SaveData saveData = new SaveData();

    public void GameStart()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Level" + currentLevel);       
    }

    public void Success()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Level" + currentLevel);        
    }

    public void Fail()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "Level" + currentLevel);      
    }

  
    void Awake()
    {
        DontDestroyOnLoad(this);
        GameAnalytics.Initialize();
        //FB.Init();
        LoadState();
    }

    private void OnDestroy()
    {
        SaveState();
    }

    public void SaveState()
    {
        string filePath = Application.persistentDataPath + "/saveFile.art";

        saveData.currentLevel = currentLevel;
        saveData.currentLevelText = currentLevelText;
        saveData.totalCoins = totalCoins;
        saveData.completedTutorial = completedTutorial;
        saveData.previousLine = previousLine;
        saveData.previousRating = previousRating;
    }

    public void LoadState()
    {
        string filePath = Application.persistentDataPath + "/saveFile.art";
        if (!File.Exists(filePath)) return; // No state to load
        
        currentLevel = saveData.currentLevel;
        currentLevelText = saveData.currentLevelText;
        totalCoins = saveData.totalCoins;
        previousLine = saveData.previousLine;
        previousRating = saveData.previousRating;
        completedTutorial = saveData.completedTutorial;
    }
    
    public void ClearState()
    {
        string filePath = Application.persistentDataPath + "/saveFile.art";
        if (!File.Exists(filePath)) return; // No state to clear

        File.Delete(filePath);
    }
}