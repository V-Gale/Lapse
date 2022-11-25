using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    public GameManager gameManager;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameManager.gameOver.activeInHierarchy && !gameManager.normalEnd.activeInHierarchy && !gameManager.prismaticEnd.activeInHierarchy) 
        {
            if (GameIsPaused) ResumeGame();
            else PauseGame();
        }
    }

    void ResumeGame() 
    {        
        PauseMenuUI.SetActive(false);                   
        Time.timeScale = 1f;
        Cursor.visible = false;
        GameIsPaused = false;        
    }
    void PauseGame() 
    { 
        PauseMenuUI.SetActive(true);
        Cursor.visible = true;
        Time.timeScale = 0f;
        GameIsPaused = true;       
    }
}
