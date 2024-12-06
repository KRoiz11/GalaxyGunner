using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameOverScreen : MonoBehaviour
{
    public void Setup()
    {
        // Ensure that the cursor is unlocked and visible when the game starts
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameObject.SetActive(true);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
