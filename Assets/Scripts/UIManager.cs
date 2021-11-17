using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI leftScoreText;
    [SerializeField] private TextMeshProUGUI rightScoreText;

    [SerializeField] private FadeUI menuUI;
    [SerializeField] private FadeUI GameOverUI;

    [SerializeField] private TextMeshProUGUI WinnerText;

    private AudioManager Audio;

    private void Start()
    {
        Audio = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
        menuUI.FadeIn(true);
        GameOverUI.FadeOut(true);
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
    }
    public void ShowMenu()
    {
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

    #region UI SOUND
    public void OnMouuseOverSound()
    {
        Audio.Play("Select");
    }
    public void OnMouseClick()
    {
        Audio.Play("Click");
    }
    #endregion
}
