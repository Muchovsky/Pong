using UnityEngine;

public class BallScript : MonoBehaviour
{
    private float initialSpeed;
    private float currentSpeed;
    private float speedIncreaseFactor;   
    private int speedIncreaseAfterThisNumberOfHits;
    private float randomFactor = 0.2f;
    [SerializeField]
    private AudioClip[] ballSounds;
    private bool hasStarted;
    private bool gameOver;
    private Rigidbody2D myRigidBody;
    private AudioSource myAudioSource;
    private Vector2 BallStartingPosition;

    void Start()
    {
        hasStarted = false;
        gameOver = false;
        myRigidBody = GetComponent<Rigidbody2D>();
        myAudioSource = GetComponent<AudioSource>();
        initialSpeed = 5f;
        currentSpeed = initialSpeed;
        speedIncreaseAfterThisNumberOfHits = 100;
        speedIncreaseFactor = initialSpeed / speedIncreaseAfterThisNumberOfHits;
        BallStartingPosition = transform.position;
        GameManager.instance.eventManager.OnBallHitPaddle += IncreaseSpeed;
        GameManager.instance.eventManager.OnBallLeavePlayArea += SpawnBall;
        GameManager.instance.eventManager.OnGameOver += LockBall;
    }

    void Update()
    {
        if (!hasStarted && !gameOver)
        {
            LaunchBall();
        }
    }

    private void SpawnBall()
    {
        hasStarted = false;
        transform.position = BallStartingPosition;
        myRigidBody.velocity = Vector2.zero;
    }

    private void LaunchBall()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            float x = Random.Range(0, 2) == 0 ? -1 : 1;
            float y = Random.Range(0, 2) == 0 ? -1 : 1;
            myRigidBody.velocity = new Vector2(currentSpeed * x, currentSpeed * y);
        }
    }

    private void LockBall()
    {
        gameOver = true;
    }

    private void IncreaseSpeed()
    {
        currentSpeed += speedIncreaseFactor;
        float xsign = Mathf.Sign(myRigidBody.velocity.x);
        float ysign = Mathf.Sign(myRigidBody.velocity.y);
        var tmpVelo = new Vector2(xsign * speedIncreaseFactor + myRigidBody.velocity.x, ysign * speedIncreaseFactor + myRigidBody.velocity.y);
        myRigidBody.velocity = tmpVelo;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //ad force to prevent endless bounce on one axis
        float x = Random.Range(0, 2) == 0 ? -1 * randomFactor : randomFactor;
        float y = Random.Range(0, 2) == 0 ? -1 * randomFactor : randomFactor;
        Vector2 velocityTweak = new Vector2(x, y);

        if (hasStarted)
        {
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
            if (collision.gameObject.tag != "Paddle")
            {
                myRigidBody.AddForce(velocityTweak);
            }
        }
    }
}