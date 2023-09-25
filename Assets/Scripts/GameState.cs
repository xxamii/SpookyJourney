using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : Singleton<GameState>
{
    public Action OnLose;
    public Action OnWin;

    public enum States
    {
        GameOn,
        Pause
    }

    public States State { get; private set; } = States.GameOn;

    private bool _gameOver;

    protected override void Awake()
    {
        base.Awake();

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        FearMeter.Instance.TooMuchFear += Lose;
    }

    private void OnDisable()
    {
        if (FearMeter.Instance != null)
        {
            FearMeter.Instance.TooMuchFear -= Lose;
        }
    }

    private void Update()
    {
        if (SceneTransitioner.Instance.FadedInThisFrame)
        {
            UnpauseGame();
        }
        if (SceneTransitioner.Instance.FadingOutThisFrame)
        {
            PauseGame();
        }
    }

    public void Win()
    {
        if (!_gameOver)
        {
            PauseGame();
            SceneTransitioner.Instance.StartTransitionNext();
            OnWin?.Invoke();
        }
    }

    private void Lose()
    {
        if (!_gameOver)
        {
            PauseGame();
            SceneTransitioner.Instance.Restart();
            OnLose?.Invoke();
        }
    }

    private void PauseGame()
    {
        State = States.Pause;
        _gameOver = true;
    }

    private void UnpauseGame()
    {
        State = States.GameOn;
        _gameOver = false;
    }
}
