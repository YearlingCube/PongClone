using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{

    [SerializeField] private Paddle.Side paddleThatScored;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BallMovement Ball = collision.GetComponent<BallMovement>();
        if (Ball)
        {
            GameManger.instance.Scored(paddleThatScored);
        }
    }
}