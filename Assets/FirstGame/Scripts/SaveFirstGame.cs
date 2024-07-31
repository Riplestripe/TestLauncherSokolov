using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class SaveFirstGame : MonoBehaviour
{
    private void Awake()
    {
        LoadGame();
    }
    public void SaveGame()
    {
        PlayerData playerData = new PlayerData();
        playerData.score = GameObject.FindGameObjectWithTag("ScoreBtn").GetComponent<Clicker>().score;

        string json = JsonUtility.ToJson(playerData);
        string path = Application.persistentDataPath + "/PlayerData.json";
        System.IO.File.WriteAllText(path, json);

    }
        private void LoadGame()
    {
        string path = Application.persistentDataPath + "/PlayerData.json";

        if (File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path);
            PlayerData loadedData = JsonUtility.FromJson<PlayerData>(json);

            GameObject scorePlayer = GameObject.FindGameObjectWithTag("ScoreBtn");
            
            scorePlayer.GetComponent<Clicker>().score = loadedData.score;
        }
        else Debug.LogWarning("Not file founded!");
    }

    private void OnApplicationQuit()
    {
        SaveGame();

    }
}
