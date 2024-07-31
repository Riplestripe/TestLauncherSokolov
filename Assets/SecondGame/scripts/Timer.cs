using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public int sec;
    public int min;
    [SerializeField] TMP_Text timer;
    public int delta = 0;
    public int bestSec = 100, bestMin = 100;

    private void Start()
    {
        StartCoroutine(ITimer());
    }
    IEnumerator ITimer()
    {
        while (true)
        {
            if(sec == 59)
            {
                min++;
                sec = -1;
            }

            sec += delta;

            timer.text = min.ToString("D2") + " : " + sec.ToString("D2");
            yield return new WaitForSeconds(1);
        }
    }
}
