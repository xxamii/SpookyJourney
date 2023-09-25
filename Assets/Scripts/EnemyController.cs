using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _velocity;

    private IEnemyInput _input;
    private Rigidbody2D _rigidbody;

    private EnemyFrameInput _frameInput;
    private Vector2 _direction;

    private void Start()
    {
        _input = GetComponent<IEnemyInput>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GatherInput();
    }

    private void FixedUpdate()
    {
        if (GameState.Instance.State == GameState.States.Pause)
        {
            _rigidbody.velocity = Vector2.zero;
            return;
        }

        CalculateDirection();
        Move();
    }

    private void GatherInput()
    {
        _frameInput = _input.Input;
    }

    private void CalculateDirection()
    {
        _direction = _frameInput.MovementDirection;
    }

    private void Move()
    {
        if (_frameInput.State == EnemyState.Fly)
        {
            _rigidbody.velocity = _direction * _velocity * _frameInput.MovementMultiplier;
        }
        else
        {
            _rigidbody.velocity = Vector2.zero;
        }
    }
}
