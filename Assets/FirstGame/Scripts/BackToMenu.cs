using Newtonsoft.Json.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    public GameObject saveGameManager;
    GameObject menu;
    private void OnEnable()
    {
        menu = GameObject.FindGameObjectWithTag("Menu").transform.GetChild(0).gameObject;

    }


    public void SaveGameFG()
    {

        saveGameManager.GetComponent<SaveFirstGame>().SaveGame();
        transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
        menu.SetActive(true);

    }

    public void SaveGameSG()
    {
        saveGameManager.GetComponent<SaveGameSG>().SaveGame();
        transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
        menu.SetActive(true);
    }
}
