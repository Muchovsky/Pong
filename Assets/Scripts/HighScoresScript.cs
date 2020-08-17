using UnityEngine;

public class HighScoresScript : MonoBehaviour
{
    void Start()
    {
        GameManager.instance.eventManager.OnGameOver += ChceckIfHighScoreBeat;
    }

    public int GetHighScoere()
    {
        return PlayerPrefs.GetInt("HighestScore", 0);
    }

    public void SetHighScoere(int val)
    {
        PlayerPrefs.SetInt("HighestScore", val);
    }

    private void ChceckIfHighScoreBeat()
    {
        int currentHighScore = GetHighScoere();
        int currentScore = GameManager.instance.currentPoints;
        if (currentScore > currentHighScore)
        {
            SetHighScoere(currentScore);
            GameManager.instance.eventManager.HighScoreBeat();
        }
    }

}
