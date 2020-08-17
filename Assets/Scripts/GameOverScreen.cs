using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverScreen;
    [SerializeField]
    private TextMeshProUGUI gameOverScoreText;
    [SerializeField]
    private TextMeshProUGUI gameOverLabel;

    void Start()
    {
        gameOverScreen.SetActive(false);
        GameManager.instance.eventManager.OnGameOver += EnableGameOverScreen;
        GameManager.instance.eventManager.OnHighScoreBeat += ChangeForHighScoreBeat;
    }

    private void EnableGameOverScreen()
    {
        int currentHighScore = GameManager.instance.highScore.GetHighScoere();
        gameOverScoreText.text = "Your Score: " + GameManager.instance.currentPoints.ToString();
        gameOverLabel.text = "Sorry, but you didn't beat current high score: " + currentHighScore;
        gameOverScreen.SetActive(true);
    }

    private void ChangeForHighScoreBeat()
    {
        gameOverLabel.text = "Congratulations! \n You beat current HighScore";
    }

}