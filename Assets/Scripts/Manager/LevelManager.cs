using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static string _levelStatsMapFIleName;

    private const string PassedLevelsKey = "PassedLevels";

    public static int LevelsCount => 16;

    public static int CurrentLevelIndex;

    public static int PassedLevelsCount
    {
        get => PlayerPrefs.GetInt(PassedLevelsKey, 0);
        private set => PlayerPrefs.SetInt(PassedLevelsKey, value);
    }

    public static readonly Dictionary<int, int> Rewards = new();

    private void Awake()
    {
        DontDestroyOnLoad(this);

        for (var i = 0; i < PassedLevelsCount; i++)
        {
            Rewards.Add(i, Random.Range(0, 4));
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