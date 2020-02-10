using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private SceneLoader sceneLoader;
    private GameUIManager uiManager;
    private GameStats gameStats;
    private GameObject ball;
    private GameObject paddle;

    private int breakableBlockCount;

    private void Start()
    {
        sceneLoader = GameObject.Find("Scene Loader").GetComponent<SceneLoader>();
        uiManager = GameObject.Find("GameUI").GetComponent<GameUIManager>();
        gameStats = GameObject.Find("GameStats").GetComponent<GameStats>();
        ball = GameObject.Find("Ball");
        paddle = GameObject.Find("Paddle");

        breakableBlockCount = GameObject.FindGameObjectsWithTag("Breakable Block").Length;

        UpdateScore();
        UpdateBalls();

        if (gameStats == null)
        {
            Debug.LogError("Game stats missing");
        }
    }

    public void BlockDestroyed(int blockValue)
    {
        gameStats.currentScore += blockValue;
        UpdateScore();

        breakableBlockCount -= 1;

        if (breakableBlockCount < 1)
        {

            StartCoroutine(LevelClear());

        }
    }
    public void BallLost()
    {
        gameStats.ballsRemaining -= 1;
        UpdateBalls();
    }

    private void UpdateScore()
    {
        uiManager.UpdateScoreUI(gameStats.currentScore);
    }

    private void UpdateBalls()
    {
        uiManager.UpdateBallsUI(gameStats.ballsRemaining);
    }


    IEnumerator LevelClear()
    {
        Destroy(ball);
        Destroy(paddle);
        uiManager.ShowLevelClear(true);

        yield return new WaitForSeconds(3.0f);

        uiManager.ShowLevelClear(false);
        sceneLoader.LoadNextScene();
    }
}
