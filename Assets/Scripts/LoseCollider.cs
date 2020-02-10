using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCollider : MonoBehaviour
{
    private SceneLoader sceneLoader;
    private GameStats gameStats;
    private LevelManager lvlMgr;

    private void Start()
    {
        sceneLoader = GameObject.Find("Scene Loader").GetComponent<SceneLoader>();
        gameStats = GameObject.Find("GameStats").GetComponent<GameStats>();
        lvlMgr = GameObject.Find("Level Manager").GetComponent<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ball")
        {
            if (gameStats.ballsRemaining > 0)
            {
                lvlMgr.BallLost();
                Ball ball = collision.gameObject.GetComponent<Ball>();
                ball.launched = false;
            }
            else
            {
                sceneLoader.LoadGameOver();
            }
            
        }
    }
}
