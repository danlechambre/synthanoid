using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private GameStats gameStats;

    private Scene currentScene;

    private void Start()
    {
        if (GameObject.Find("GameStats"))
        {
            gameStats = GameObject.Find("GameStats").GetComponent<GameStats>();
        }
        
        currentScene = SceneManager.GetActiveScene();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentScene.buildIndex == 0 || currentScene.buildIndex == 1)
            {
                QuitApp();
            }
            else
            {
                LoadMainMenu();
            }
        }
    }

    public void LoadNextScene()
    {
        int nextSceneBuildIndex = currentScene.buildIndex + 1;
        if (nextSceneBuildIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneBuildIndex);
        }
        //else
        //{
        //    LoadGameOver();
        //}
    }

    public void StartGame()
    {
        if (gameStats)
        {
            gameStats.DestroyGameStats();
        }
        SceneManager.LoadScene(2);
        Cursor.visible = false;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        Cursor.visible = true;
    }

    public void LoadGameOver()
    {
        SceneManager.LoadScene(1);
        Cursor.visible = true;
    }

    public void QuitApp()
    {
        Application.Quit();
        Debug.Log("Application quit request");
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(currentScene.name);
    }
}
