using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    public int currentScore = 0;
    [SerializeField]
    public int ballsRemaining = 6;

    private void Awake()
    {
        int gamesStatsCount = FindObjectsOfType<GameStats>().Length;

        if (gamesStatsCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void DestroyGameStats()
    {
        Destroy(gameObject);
    }

}
