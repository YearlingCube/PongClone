using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI leftScoreText;
    [SerializeField] private TextMeshProUGUI rightScoreText;

    [SerializeField] private FadeUI menuUI;
    [SerializeField] private FadeUI GameOverUI;
    [SerializeField] private FadeUI PausedUI;

    [SerializeField] private TextMeshProUGUI WinnerText;

    private AudioManager Audio;

    private void Start()
    {
        Audio = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
        menuUI.FadeIn(true);
        GameOverUI.FadeOut(true);
        PausedUI.FadeOut(true);
    }
    public void UpdateScoreText(int leftScore, int rightScore)
    {
        leftScoreText.text = leftScore.ToString();
        rightScoreText.text = rightScore.ToString();
    }

    public void OnGameStart()
    {
        menuUI.FadeOut(false);
        GameOverUI.FadeOut(false);
        PausedUI.FadeOut(false);
    }
    public void ShowMenu()
    {
        PausedUI.FadeOut(false);
        GameOverUI.FadeOut(false);
        menuUI.FadeIn(false);
    }

    public void ShowGameOver(Paddle.Side side)
    {
        GameOverUI.FadeIn(false);
        if (side == Paddle.Side.Left)
            WinnerText.text = "Player 1";
        else if (side == Paddle.Side.Right)
            WinnerText.text = "Player 2";
    }
    public void Paused(bool pause)
    {
        if (pause)
        {
            PausedUI.FadeIn(true);
            Time.timeScale = 0;
        }
        else if (!pause)
        {
            PausedUI.FadeOut(false);
                Time.timeScale = 1;
        }
;    }
    #region UI SOUND
    public void OnMouuseOverSound()
    {
        Audio.PlaySound("Select");
    }
    public void OnMouseClick()
    {
        Audio.PlaySound("Click");
    }
    #endregion
}
