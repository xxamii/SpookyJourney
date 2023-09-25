using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearTicker : MonoBehaviour
{
    [SerializeField] private float _timeBetweenTicks;

    private float _lastTick;

    private void Update()
    {
        if (GameState.Instance.State == GameState.States.Pause)
        {
            return;
        }

        TickFear();
    }

    private void TickFear()
    {
        if (_lastTick + _timeBetweenTicks < Time.time)
        {
            FearMeter.Instance.TickFear();
            _lastTick = Time.time;
        }
    }
}
