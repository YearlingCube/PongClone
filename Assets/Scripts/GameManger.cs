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

    public bool inMenu;

    private BallMovement ball;

    [SerializeField] private Paddle leftPaddle;
    [SerializeField] private Paddle rightPaddle;

    private Paddle.Side serveSide;

    private void Awake()
    {
        instance = this;
        ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<BallMovement>();

        DoMenu();
    }

    private void DoMenu()
    {
        inMenu = true;
        leftPaddle.isAi = rightPaddle.isAi = true;
        leftScore = rightScore = 0;
        UIManager.UpdateScoreText(leftScore, rightScore);
        ResetGame();
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
            {
                ball.gameObject.SetActive(false);
                UIManager.ShowGameOver(side);
            }
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
        ball.gameObject.SetActive(true);
        ball.ResetBall(serveSide);
        leftPaddle.Reset();
        rightPaddle.Reset();
    }
    private void IntitializeGame()
    {
        inMenu = false;
        leftScore = rightScore = 0;
        UIManager.UpdateScoreText(leftScore, rightScore);
        ResetGame();
    }
    #region Buttons
    public void Start1Player()
    {
        IntitializeGame();
        rightPaddle.isAi = true;
        leftPaddle.isAi = false;
        UIManager.OnGameStart();
    }
    public void Start2Player()
    {
        IntitializeGame();
        rightPaddle.isAi = false;
        leftPaddle.isAi = false;
        UIManager.OnGameStart();
    }
    public void StartAIvsAI()
    {
        IntitializeGame();
        rightPaddle.isAi = true;
        leftPaddle.isAi = true;
        UIManager.OnGameStart();
    }
    public void GoToMenu()
    {
        UIManager.ShowMenu();
        DoMenu();
    }
    public void Replay()
    {
        IntitializeGame();
        UIManager.OnGameStart();
    }
    public void Quit()
    {
        Debug.Log("QUIT!");
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    #endregion
}
