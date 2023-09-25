using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleNightmareAI : MonoBehaviour, IEnemyInput
{
    [SerializeField] private float _knockbackDuration;
    [SerializeField] private float _knockbackMultiplier;

    private EnemyFrameInput _input;
    public EnemyFrameInput Input => _input;

    private Transform _transform;
    private Chariot _chariot;

    private float _lastKnocked = float.MinValue;
    private Vector2 _directionToChariot;
    private bool _knockedThisFrame;
    private Vector2 _knockedFrom;

    private void Start()
    {
        _transform = transform;
        _chariot = Chariot.Instance;
    }

    private void Update()
    {
        LookAtChariot();
        CalculateMovementDirection();
    }

    public void Move()
    {
        _input.State = EnemyState.Fly;
    }

    public void Stop()
    {
        _input.State = EnemyState.Stop;
    }

    public void Knockback(Vector2 from)
    {
        _knockedFrom = from;
        _knockedThisFrame = true;
    }

    private void LookAtChariot()
    {
        Vector2 direction = _chariot.transform.position - _transform.position;
        _directionToChariot = direction.normalized;

        if (_transform.right.x > 0f && direction.x < 0f ||
            _transform.right.x < 0f && direction.x > 0f)
        {
            _transform.Rotate(0, 180, 0);
        }
    }

    private void CalculateMovementDirection()
    {
        if (_knockedThisFrame && _lastKnocked + _knockbackDuration < Time.time)
        {
            _input.MovementMultiplier = _knockbackMultiplier;
            _input.MovementDirection = ((Vector2)_transform.position - _knockedFrom).normalized;
            _lastKnocked = Time.time;
            _knockedThisFrame = false;
        }
        else if (_lastKnocked + _knockbackDuration < Time.time)
        {
            _input.MovementMultiplier = 1f;
            _input.MovementDirection = _directionToChariot;
        }
    }
}
