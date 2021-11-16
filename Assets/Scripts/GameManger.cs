using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    public static GameManger instance { get; private set; }

    [SerializeField] private UIManager UIManager;

    [SerializeField] private int ScoreToWin = 3;
    [SerializeField] private int leftScore;
    [SerializeField] private int rightScore;

    [SerializeField] private bool inMenu;

    private BallMovement ball;

    [SerializeField] private Paddle leftPaddle;
    [SerializeField] private Paddle rightPaddle;

    private Paddle.Side serveSide;

    private void Awake()
    {
        instance = this;
        ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<BallMovement>();
    }
    public void Scored(Paddle.Side side)
    {
        if (side == Paddle.Side.Left)
            leftScore++;
        else if (side == Paddle.Side.Right)
            rightScore++;

        UIManager.UpdateScoreText(leftScore, rightScore);
        serveSide = side;

        if (IsGameOver())
        {
            if (inMenu)
            {
                ResetGame();
                leftScore = rightScore = 0;
            }
            else if (!inMenu)
                ball.gameObject.SetActive(false);
        }
        else
        {
            ResetGame();
        }
    }

    private bool IsGameOver()
    {
        bool result = false;

        if (leftScore >= ScoreToWin || rightScore >= ScoreToWin)
            result = true;
        
        return result;
    }
    private void ResetGame()
    {
        ball.ResetBall(serveSide);
        leftPaddle.Reset();
        rightPaddle.Reset();
    }
    private void IntitializeGame()
    {
        inMenu = false;
        leftScore = rightScore = 0;
        ResetGame();
    }
}
