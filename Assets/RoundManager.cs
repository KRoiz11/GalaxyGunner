using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundManager : MonoBehaviour
{  
    private int round = 1;
    //reference to TextMesh UI
    public TextMeshProUGUI RoundDisplay;

    //increments round
    public void IncreaseRound()
    {
        round += 1;
    }

    private void Update()
    {
        if (RoundDisplay != null)
        {
            RoundDisplay.SetText("Round: " + round);
        }
    }
}

