using UnityEngine;

public class PaddleScript : MonoBehaviour
{
    [SerializeField]
    private float minY;
    [SerializeField]
    private float maxY;
    [SerializeField]
    [Range(2f, 10f)]
    private float speed;

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (!GameManager.instance.gameOver)
        {
            float pos = Input.GetAxis("Vertical") * Time.deltaTime * speed;
            transform.Translate(pos * Vector2.up);
            Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
            paddlePos.y = Mathf.Clamp(paddlePos.y, minY, maxY);
            transform.position = paddlePos;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            GameManager.instance.eventManager.BallHitPaddle();
        }
    }
}