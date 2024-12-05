using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float currentHealth;

    //score related
    public int scorePerKill = 10;
    private ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {   
        // sets player's health to full 
        currentHealth = maxHealth;
        //reference to ScoreManage script
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();
    }

    public void TakeDamage (float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            if (scoreManager != null) 
            {
                scoreManager.IncreaseScore(scorePerKill);
            }
            
            Destroy(gameObject);
        }
    }
}
