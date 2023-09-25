using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearIncreaser : MonoBehaviour
{
    [SerializeField] private float _amount;

    private Trigger _trigger;
    private Destroyer _destroyer;

    private void Start()
    {
        _trigger = GetComponent<Trigger>();

        _trigger.OnTrigger += OnTrigger;
    }

    private void OnDisable()
    {
        _trigger.OnTrigger -= OnTrigger;
    }

    private void OnTrigger(Collider2D other)
    {
        if (GameState.Instance.State == GameState.States.GameOn && other.GetComponent<Chariot>() != null)
        {
            FearMeter.Instance.IncreaseFear(_amount);
        }
    }
}
