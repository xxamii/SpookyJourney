using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearMeter : Singleton<FearMeter>
{
    public Action TooMuchFear;
    public Action OnFearChanged;

    public float MaxFear => _maxFear;
    public float CurrentFear => _fear;

    [SerializeField] private float _maxFear;
    [SerializeField] private float _tickAmount;
    [SerializeField] private float _dialogueFearAmount;
    [SerializeField] private float _tickCap;

    private float _fear;
    private float _previousFear;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        if (SceneTransitioner.Instance.FadedInThisFrame)
        {
            _fear = Mathf.Clamp(_fear, 0f, _tickCap);
            _previousFear = _fear;
            OnFearChanged?.Invoke();
        }
        if (SceneTransitioner.Instance.RestartingThisFrame)
        {
            _fear = _previousFear;
        }
    }

    public void TickFear()
    {
        return;

        if (_fear + _tickAmount <= _tickCap)
        {
            IncreaseFear(_tickAmount);
        }
    }

    public void IncreaseFear(float amount)
    {
        if (GameState.Instance.State == GameState.States.GameOn)
        {
            _fear += amount;

            if (_fear >= _maxFear)
            {
                Lose();
            }

            OnFearChanged?.Invoke();
        }
    }

    public void DecreaseFear(float amount)
    {
        _fear -= amount;
        _fear = Mathf.Clamp(_fear, 0f, _maxFear);
        OnFearChanged?.Invoke();
    }

    private void Lose()
    {
        TooMuchFear?.Invoke();
    }
}
