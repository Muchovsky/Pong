using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    [SerializeField]
    [Range(1, 5)]
    private int pointsPerHit = 1;
    [SerializeField]
    [Range(1, 10)]
    private int startigHP = 3;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI hpText;
    private SceneController sceneController;
    [HideInInspector]
    public EventsManager eventManager;
    [HideInInspector]
    public HighScoresScript highScore;
    [HideInInspector]
    private int currentHp;
    [HideInInspector]
    public int currentPoints;
    [HideInInspector]
    public bool gameOver;

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

        sceneController = GetComponentInChildren<SceneController>();
        eventManager = GetComponentInChildren<EventsManager>();
        highScore = GetComponentInChildren<HighScoresScript>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHp = startigHP;
        currentPoints = 0;
        gameOver = false;
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
            gameOver = true;
            eventManager.GameOver();
        }
    }

    private void GivePoints()
    {
        currentPoints += pointsPerHit;
        scoreText.text = "Score: " + currentPoints.ToString();
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}