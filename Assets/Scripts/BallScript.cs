using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    [SerializeField]
    float initialSpeed;
    [SerializeField]
    float currentSpeed;
    float speedIncreaseFactor;
    [SerializeField]
    int speedIncreaseAfterThisNumberOfHits;
    float randomFactor = 0.2f;
    [SerializeField]
    AudioClip[] ballSounds;
    bool hasStarted = false;
    Rigidbody2D myRigidBody;
    AudioSource myAudioSource;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAudioSource = GetComponent<AudioSource>();
        initialSpeed = 5f;
        currentSpeed = initialSpeed;
        speedIncreaseAfterThisNumberOfHits = 100;
        speedIncreaseFactor = initialSpeed / speedIncreaseAfterThisNumberOfHits;

        GameManager.instance.eventManager.OnBallHitPaddle += IncreaseSpeed;
    }

    void Update()
    {
        if (!hasStarted)
        {
            LaunchBall();
        }
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

    private void IncreaseSpeed()
    {
        Debug.Log("velocity  przed" + myRigidBody.velocity);
        currentSpeed += speedIncreaseFactor;
       // Mathf.Sign(myRigidBody.velocity.x)
        myRigidBody.velocity = new Vector2(Mathf.Sign(myRigidBody.velocity.x) * currentSpeed, Mathf.Sign(myRigidBody.velocity.y) * currentSpeed );

        Debug.Log("velocity po" + myRigidBody.velocity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float x = Random.Range(0, 2) == 0 ? -1 * randomFactor : randomFactor;
        float y = Random.Range(0, 2) == 0 ? -1 * randomFactor : randomFactor;
        Vector2 velocityTweak = new Vector2(x, y);

        if (hasStarted)
        {
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
            myRigidBody.AddForce(velocityTweak);
            Debug.Log("velocity " + myRigidBody.velocity);

        }
    }
}
