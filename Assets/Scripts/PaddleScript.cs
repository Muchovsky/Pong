using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour
{

    Rigidbody2D myRigidBody;
    float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        //simple ship movement
        myRigidBody.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, 0);
        //keep spaceship in boundaries of camera 
        Vector2 shipPosition = new Vector2(transform.position.x, transform.position.y);
        shipPosition.x = Mathf.Clamp(myRigidBody.transform.position.x, speed, speed);
        myRigidBody.transform.position = shipPosition;
    }
}
