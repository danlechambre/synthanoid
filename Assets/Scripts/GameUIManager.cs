using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    private TextMeshProUGUI ballsText;
    private TextMeshProUGUI scoreText;

#pragma warning disable CS0649
    [SerializeField]
    private GameObject levelClearText;
#pragma warning restore CS0649

    void Start()
    {
        scoreText = GameObject.Find("Score Text").GetComponent<TextMeshProUGUI>();
        ballsText = GameObject.Find("Balls Text").GetComponent<TextMeshProUGUI>();

        Cursor.visible = false;
    }

    public void UpdateScoreUI(int currentScore)
    {
        scoreText.text = currentScore.ToString();
    }

    public void UpdateBallsUI(int currentBalls)
    {
        ballsText.text = "balls: " + currentBalls;
    }

    public void ShowLevelClear(bool show)
    {
        levelClearText.SetActive(show);
    }
}
