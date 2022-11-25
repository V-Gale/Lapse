using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject normalEnd;
    public GameObject prismaticEnd;
    public GameObject gameOver;

    public GameObject pause;
    public void Update()
    {
        if (gameOver.activeInHierarchy || pause.activeInHierarchy) Time.timeScale = 0f;
        else Time.timeScale = 1f;
    }
    public void GameOver() 
    {
        gameOver.SetActive(true);
        FindObjectOfType<AudioManager>().Play("PlayerDeath");
    }
    public void NormalEnding() 
    {
        normalEnd.SetActive(true);
        FindObjectOfType<AudioManager>().Stop("LevelMainTheme");
        FindObjectOfType<AudioManager>().Play("NormalEndingSong");
    }
    public void PrismaticEnding() 
    {
        prismaticEnd.SetActive(true);
        FindObjectOfType<AudioManager>().Stop("LevelMainTheme");
        FindObjectOfType<AudioManager>().Play("PrismaticEndingSong");
    }

    public void RestartLevel()
    {   
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ReturnMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetSceneByBuildIndex(0).handle);
        FindObjectOfType<AudioManager>().Stop("PrismaticEndingSong");
        FindObjectOfType<AudioManager>().Stop("NormalEndingSong");
        FindObjectOfType<AudioManager>().Play("LevelMainTheme");
    }
}
