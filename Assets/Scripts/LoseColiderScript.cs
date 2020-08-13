using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseColiderScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ball")
        {
            GameManager.instance.eventManager.BallLeavePlayArea();
        }
    }
}
