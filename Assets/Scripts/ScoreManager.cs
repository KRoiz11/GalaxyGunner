using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{  
    private int score = 0;
    //reference to TextMesh UI object
    public TextMeshProUGUI ScoreDisplay;

    //increases score by certain amount
    public void IncreaseScore(int amount)
    {
        score += amount;
    }

    private void Update()
    {
        if (ScoreDisplay != null)
        {
            ScoreDisplay.SetText("Score: " + score);
        }
    }
}
