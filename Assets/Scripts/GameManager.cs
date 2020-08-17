using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    [SerializeField]
    private int pointsPerHit = 1;
    [SerializeField]
    private int startigHP = 3;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI hpText;  
    [SerializeField]
    private SceneController sceneController;
    [SerializeField]
    public EventsManager eventManager;
    private int currentHp;
    public int currentPoints;
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
        currentPoints = 0;
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
            eventManager.GameOver();
        }
    }

    private void GivePoints()
    {
        currentPoints += pointsPerHit;
        scoreText.text = "Score: " + currentPoints.ToString();
    }
}