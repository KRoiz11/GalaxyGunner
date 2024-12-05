using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    //Health  variables
    [SerializeField] private float maxHealth;
    private float currentHealth;
    public HealthBar healthBar;

    //Life variables
    public int maxLives;
    public int currentLives;
    public Image[] hearts;

    //reference to heart sprites
    public Sprite emptyHeart;
    public Sprite filledHeart;

    //reference to game over UI
    public GameOverScreen GameOverScreen;
    
    // Start is called before the first frame update
    void Start()
    {   
        // sets player's health to full 
        currentHealth = maxHealth; 

        //sets slider to full
        healthBar.SetSliderMax(maxHealth);

        //sets player's lives to full
        currentLives = maxLives;
        
        //sets all heart sprites to filled
        foreach (Image heart in hearts)
        {
            heart.sprite = filledHeart;
        }
    }

    public void TakeDamage (float amount)
    {
        currentHealth -= amount;
        healthBar.SetSlider(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }

    }

    void Die()
    {
        currentLives --;
        // Updates the index to avoid out of range error
        if (currentLives >= 0)
            hearts[currentLives].sprite = emptyHeart;

        if (currentLives <= 0)
        {
            GameOverScreen.Setup();
        }
        else
        {
            currentHealth = maxHealth;
            healthBar.SetSliderMax(maxHealth);
        }
    }
}


