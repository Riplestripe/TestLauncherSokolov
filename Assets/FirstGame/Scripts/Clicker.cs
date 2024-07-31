using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    public int score;
    int clickScore = 1;
    public TMP_Text textMeshPro;

    private void Update()
    {
        textMeshPro.text = score.ToString();
    }

    public void ScoreClick()
    {
        score += clickScore;
    }
}
