using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    [SerializeField] int pointsPerHit = 1;
    [SerializeField] int startigHP = 3;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] public EventsManager eventManager;

    int currentHp;
    int currentPoints;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHp = startigHP;
        eventManager.OnBallHitPaddle += GivePoints;
        eventManager.OnBallLeavePlayArea += TakeLive;
        scoreText.text = "Score: " + currentPoints.ToString();
        hpText.text = "Lives: " + currentHp.ToString();
    }

    private void TakeLive()
    {
        currentHp--;
        hpText.text = "Lives: " + currentHp.ToString();
        if (currentHp <= 0)
        {
            Debug.Log("GameOver");
        }
    }

    private void GivePoints()
    {
        currentPoints += pointsPerHit;
        scoreText.text = "Score: " + currentPoints.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
