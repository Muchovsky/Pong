using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    public event Action OnBallHitPaddle;
    public void BallHitPaddle()
    {
        OnBallHitPaddle();
    }

    public event Action OnBallLeavePlayArea;
    public void BallLeavePlayArea()
    {
        OnBallLeavePlayArea();
    }

    public event Action OnGameOver;
    public void GameOver()
    {
        OnGameOver();
    }
}
