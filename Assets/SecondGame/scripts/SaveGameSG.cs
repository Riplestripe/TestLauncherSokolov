using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveGameSG : MonoBehaviour
{
    private void Awake()
    {
        LoadGame();
    }
    public void SaveGame()
    {
        PlayerDataSG playerData = new PlayerDataSG();
        playerData.bestMin = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>().bestMin;
        playerData.bestSec = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>().bestSec;

        string json = JsonUtility.ToJson(playerData);
        string path = Application.persistentDataPath + "/PlayerDataSG.json";
        System.IO.File.WriteAllText(path, json);

    }
    private void LoadGame()
    {
        string path = Application.persistentDataPath + "/PlayerDataSG.json";

        if (File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path);
            PlayerDataSG loadedData = JsonUtility.FromJson<PlayerDataSG>(json);

            GameObject timer = GameObject.FindGameObjectWithTag("Timer");

            timer.GetComponent<Timer>().bestMin = loadedData.bestMin;
            timer.GetComponent<Timer>().bestSec = loadedData.bestSec;

        }
        else Debug.LogWarning("Not file founded!");
    }

    
}
