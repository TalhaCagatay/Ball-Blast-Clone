using System;
using UnityEngine;
using UnityEngine.UI;

public class InGamePanel : MonoBehaviour
{
    public static event Action FingerDown;
    public static event Action FingerUp;
    public static event Action SpeedUp;

    [SerializeField] private Text _currentText;
    [SerializeField] private Text _highScoreText;

    private void Start()
    {
        GameController.Score += OnScore;
        GameController.ResetGame += OnResetGame;

        UpdateCurrentScore(0);
        _highScoreText.text = "BEST : " + PlayerData.Instance.HighScore;
    }

    private void OnDestroy()
    {
        GameController.Score -= OnScore;
        GameController.ResetGame -= OnResetGame;
    }

    private void OnResetGame()
    {
        UpdateCurrentScore(0);
        _highScoreText.text = "BEST : " + PlayerData.Instance.HighScore;
    }

    private void UpdateCurrentScore(int score)
    {
        _currentText.text = "CURRENT : " + score;
    }

    private void OnScore(int score)
    {
        UpdateCurrentScore(score);
    }

    public void PointerDown()
    {
        if(FingerDown != null)
        {
            FingerDown.Invoke();
        }
    }

    public void PointerUp()
    {
        if (FingerUp != null)
        {
            FingerUp.Invoke();
        }
    }

    public void SpeedUpgradeClicked()
    {
        if(SpeedUp != null)
        {
            SpeedUp.Invoke();
        }
    }
}