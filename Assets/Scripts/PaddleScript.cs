using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour
{
    [SerializeField] float minY;
    [SerializeField] float maxY;
    float speed = 8f;
    // Start is called before the first frame update
    void Start()
    {       
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        float pos = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        transform.Translate(pos * Vector2.up);
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
        paddlePos.y = Mathf.Clamp(paddlePos.y, minY, maxY);
        transform.position = paddlePos;
    }
  
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            GameManager.instance.eventManager.BallHitPaddle();
        }
    }
}
