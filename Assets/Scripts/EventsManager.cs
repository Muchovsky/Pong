using System;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    public event Action OnBallHitPaddle;
    public event Action OnBallLeavePlayArea;
    public event Action OnGameOver;
    public event Action OnHighScoreBeat;

    public void BallHitPaddle() => OnBallHitPaddle?.Invoke();
    public void BallLeavePlayArea() => OnBallLeavePlayArea?.Invoke();
    public void GameOver() => OnGameOver?.Invoke();
    public void HighScoreBeat() => OnHighScoreBeat?.Invoke();
}