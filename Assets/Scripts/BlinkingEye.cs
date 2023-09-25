using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlinkingEye : MonoBehaviour
{
    [SerializeField] private float _minBlinkCooldown;
    [SerializeField] private float _maxBlinkCooldown;

    private Animator _animator;

    private readonly string _blinkTrigger = "Blink";

    private float _lastBlinked;
    private float _nextBlink;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        _nextBlink = Random.Range(_minBlinkCooldown, _maxBlinkCooldown);
    }

    private void Update()
    {
        if (_lastBlinked + _nextBlink < Time.time)
        {
            _animator.SetTrigger(_blinkTrigger);
            _nextBlink = Random.Range(_minBlinkCooldown, _maxBlinkCooldown);
            _lastBlinked = Time.time;
        }
        else
        {
            _animator.ResetTrigger(_blinkTrigger);
        }
    }
}
