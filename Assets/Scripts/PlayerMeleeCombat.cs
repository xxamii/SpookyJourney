using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeCombat : MonoBehaviour
{
    public bool AttackingThisFrame { get; private set; }

    private PlayerInput _input;
    private MeleeCombat _combat;
    private Transform _transform;

    private FrameInput _frameInput;
    private Vector2 _attackDirection = Vector2.right;

    private void Start()
    {
        _input = GetComponent<PlayerInput>();
        _combat = GetComponent<MeleeCombat>();
        _transform = transform;
    }

    private void Update()
    {
        GatherInput();
        CalculateDirection();
        TryAttack();
    }

    private void GatherInput()
    {
        _frameInput = _input.Input;
    }

    private void CalculateDirection()
    {
        if (_attackDirection.x > 0f && _frameInput.X < 0f ||
            _attackDirection.x < 0f && _frameInput.X > 0f)
        {
            _attackDirection.x *= -1;
        }
    }

    private void TryAttack()
    {
        AttackingThisFrame = false;

        if (_frameInput.Attack && _combat.TryAttack(_attackDirection, _transform.position))
        {
            AttackingThisFrame = true;
        }
    }
}
