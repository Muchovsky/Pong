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

    void Start()
    {
        gameOverScreen.SetActive(false);
        GameManager.instance.eventManager.OnGameOver += EnableGameOverScreen;
    }

    private void EnableGameOverScreen()
    {
        gameOverScoreText.text = "Score: " + GameManager.instance.currentPoints.ToString();
        gameOverScreen.SetActive(true);
    }
}