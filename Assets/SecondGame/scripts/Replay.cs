using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Replay : MonoBehaviour
{
    Timer timer;
    GameObject player;
    Vector3 playerTransform;
    GameObject[] btns;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform.position;
        
        
    }
    public void ReplayPush()
    {
        timer = GetComponent<Timer>();
        timer.sec = 0;
        timer.min = 0;
        timer.delta = 1;
        player.transform.position = playerTransform;
        player.transform.eulerAngles = Vector3.zero;
        player.GetComponent<PlayerMovemente>().enabled = true;
        btns = GameObject.FindGameObjectsWithTag("FinishUI");
        foreach(GameObject gameObject in btns)
        {
            gameObject.SetActive(false);
        }
    }
}
