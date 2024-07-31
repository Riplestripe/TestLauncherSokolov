using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] GameObject replayBtn;
    [SerializeField] Timer timer;
    [SerializeField] SaveGameSG save;
    [SerializeField] TMP_Text bestResult;
    [SerializeField] GameObject backToMenu;
    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerMovemente>().enabled = false;
            replayBtn.SetActive(true);
            timer.delta = 0;
            if(timer.sec < timer.bestSec && timer.min <= timer.bestMin)
            {
                timer.bestMin = timer.min;
                timer.bestSec = timer.sec;
            }

            bestResult.gameObject.SetActive(true);
            backToMenu.SetActive(true);
            bestResult.text = "Best result: " +  timer.bestMin.ToString("D2") + " : " + timer.bestSec.ToString("D2");
            save.SaveGame();
        }
    }
}
