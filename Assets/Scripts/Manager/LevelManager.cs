using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;
    
    private static string _levelStatsMapFIleName;

    private const string PlayerSpriteIndexKey = "PlayerSpriteIndex";
    private const string PassedLevelsKey = "PassedLevels";

    public static int LevelsCount => 16;

    public static int CurrentLevelIndex;

    public static int PassedLevelsCount
    {
        get => PlayerPrefs.GetInt(PassedLevelsKey, 0);
        set => PlayerPrefs.SetInt(PassedLevelsKey, value);
    }

    public static Dictionary<int, int> Rewards = new();

    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this);

        for (var i = 0; i < PassedLevelsCount; i++)
        {
            Rewards.Add(0, Random.Range(0, 4));
        } 
    }

    public static void CompleteLevel()
    {
        if (CurrentLevelIndex == PassedLevelsCount)
            PassedLevelsCount++;

        if (!Rewards.ContainsKey(CurrentLevelIndex))
            Rewards.Add(CurrentLevelIndex, Random.Range(0, 4));
        else
        {
            var stars = Random.Range(0, 4);
            if (stars > Rewards[CurrentLevelIndex])
                Rewards[CurrentLevelIndex] = stars;
        }
        
        CurrentLevelIndex++;
    }
}