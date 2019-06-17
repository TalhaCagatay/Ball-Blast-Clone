using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static event Action<int> Score;
    public static event Action ResetGame;

    private int _currentScore;

    private void Start()
    {
        SubscribeEvents();
    }

    private void OnDestroy()
    {
        UnSubscribeEvents();
    }

    private void SubscribeEvents()
    {
        EnemyBehaviour.Die += OnDie;
        EnemyBehaviour.Split += OnSplit;
        InGamePanel.SpeedUp += OnSpeedUp;
        PlayerBehaviour.PlayerDeath += OnPlayerDeath;
    }

    private void UnSubscribeEvents()
    {
        EnemyBehaviour.Die -= OnDie;
        EnemyBehaviour.Split -= OnSplit;
        InGamePanel.SpeedUp -= OnSpeedUp;
        PlayerBehaviour.PlayerDeath -= OnPlayerDeath;
    }

    private void OnSplit(int size, int health, EnemyBehaviour enemy)
    {
        UpdateScore(health * 2);// since we dive half when we split, we multiply it again to give proper score...
    }

    private void OnPlayerDeath()
    {
        if(ResetGame != null)
        {
            ResetGame.Invoke();
        }
    }

    private void OnSpeedUp()
    {
        PlayerData.Instance.SpeedUpgradeLevel++;
    }

    // i am just giving enemy's health as score when they die
    private void OnDie(int health, EnemyBehaviour enemyBehav)
    {
        UpdateScore(health);
    }

    private void UpdateScore(int score)
    {
        _currentScore += score;
        if (_currentScore > PlayerData.Instance.HighScore)
        {
            PlayerData.Instance.HighScore = _currentScore;
        }

        if (Score != null)
        {
            Score.Invoke(_currentScore);
        }
    }
}
