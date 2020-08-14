﻿using System.Collections;
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
    Vector2 BallStartingPosition;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAudioSource = GetComponent<AudioSource>();
        initialSpeed = 5f;
        currentSpeed = initialSpeed;
        speedIncreaseAfterThisNumberOfHits = 100;
        speedIncreaseFactor = initialSpeed / speedIncreaseAfterThisNumberOfHits;
        BallStartingPosition = transform.position;
        GameManager.instance.eventManager.OnBallHitPaddle += IncreaseSpeed;
        GameManager.instance.eventManager.OnBallLeavePlayArea += SpawnBall;
    }

    void Update()
    {
        if (!hasStarted)
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
