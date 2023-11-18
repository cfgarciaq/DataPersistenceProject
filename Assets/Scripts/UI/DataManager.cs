using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }
    public int HigestScore { get; private set; }
    public string PlayerName { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadScore();
    }    

    public bool CheckBestScore(string playername,int score)
    {
        if(score <= HigestScore)
        {
            return false;
        }
        else
        {
            PlayerName = playername;
            HigestScore = score;

            return true;
        }
    }

    [System.Serializable]
    private class BestPlayer
    {
        public string Name;
        public int Score;
    }

    private void SaveScore()
    {
        BestPlayer best = new BestPlayer();
        best.Name = PlayerName;
        best.Score = HigestScore;

        string json_bestScore = JsonUtility.ToJson(best);

        File.WriteAllText($"{Application.persistentDataPath}/savedBestScore.json", json_bestScore);
    }

    private void LoadScore()
    {
        string path = $"{Application.persistentDataPath}/savedBestScore.json";

        if (File.Exists(path))
        {
            string json_savedBestScore = File.ReadAllText(path);
            BestPlayer bestPlayer = JsonUtility.FromJson<BestPlayer>(json_savedBestScore);
            
            HigestScore = bestPlayer.Score;
            PlayerName = bestPlayer.Name;
        }
        else
        {
            HigestScore = 0;
            PlayerName = "NoHighScore"
        }
    }
}
