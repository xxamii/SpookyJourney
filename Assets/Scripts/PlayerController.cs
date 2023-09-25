using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool JumpThisFrame { get; private set; }
    public bool LandThisFrame { get; private set; }
    public bool FallThisFrame { get; private set; }

    [SerializeField] private float _baseMoveVelocity;
    [SerializeField] private float _jumpVelocity;
    [SerializeField] private float _maxFallVelocity;
    [SerializeField] private float _jumpBufferTime = 0.1f;
    [SerializeField] private float _coyoteTime = 0.1f;
    [SerializeField] private float _jumpApexThreshold;
    [SerializeField] private float _jumpApexBonus;
    [SerializeField] private float _jumpEarlyFactor;

    private Rigidbody2D _rigidbody;
    private PlayerInput _playerInput;
    private PlayerCollisionDetection _collisionDetection;

    private FrameInput _frameInput;
    private float _jumpApexFactor;
    private float _lastJumpPressed = float.MinValue;
    private float _lastLeftGround;
    private bool _collisionDown;
    private bool _coyoteUsable;
    private bool _endJumpEarly;
    private bool _pressedJumpUp;

    private bool HasBufferedJump => _lastJumpPressed + _jumpBufferTime >= Time.time;
    private bool CanUseCoyote => _coyoteUsable && _lastLeftGround + _coyoteTime >= Time.time;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerInput = GetComponent<PlayerInput>();
        _collisionDetection = GetComponent<PlayerCollisionDetection>();
    }

    private void Update()
    {
        GatherInput();
    }

    private void FixedUpdate()
    {
        RunCollisionDetection();

        CalculateJumpApex();
        Move();

        CalculateIsFalling();
        ClampFallVelocity();
    }

    public void GatherInput()
    {
        _frameInput = _playerInput.Input;
        if (_frameInput.JumpDown)
        {
            _lastJumpPressed = Time.time;
        }
        if (_frameInput.JumpUp)
        {
            _pressedJumpUp = true;
        }
    }

    private void CalculateJumpApex()
    {
        if (!_collisionDown)
        {
            _jumpApexFactor = Mathf.InverseLerp(_jumpApexThreshold, 0, Mathf.Abs(_rigidbody.velocity.y));
        }
        else
        {
            _jumpApexFactor = 0;
        }
    }

    private void Move()
    {
        CalculateHorizontal();
        CalculateJump();
    }

    private void CalculateHorizontal()
    {
        float currentMoveVelocity = _baseMoveVelocity;

        float apexBonus = _jumpApexBonus * _jumpApexFactor;
        currentMoveVelocity += apexBonus;

        _rigidbody.velocity = new Vector2(_frameInput.X * currentMoveVelocity, _rigidbody.velocity.y);
    }

    private void CalculateJump()
    {
        if (HasBufferedJump && (_collisionDown || CanUseCoyote))
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpVelocity);
            _coyoteUsable = false;
            _endJumpEarly = false;
            JumpThisFrame = true;
        }
        else
        {
            JumpThisFrame = false;
        }
        
        if (!_collisionDown && _rigidbody.velocity.y > 0 && _pressedJumpUp && !_endJumpEarly)
        {
            _endJumpEarly = true;
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y / _jumpEarlyFactor);
        }

        _pressedJumpUp = false;
    }

    private void RunCollisionDetection()
    {
        bool currentCollisionDown = _collisionDetection.CollisionDown;

        LandThisFrame = false;

        if (_collisionDown && !currentCollisionDown)
        {
            _lastLeftGround = Time.time;
        }
        else if (!_collisionDown && currentCollisionDown)
        {
            _coyoteUsable = true;
            _endJumpEarly = false;
            LandThisFrame = true;
        }

        _collisionDown = currentCollisionDown;
    }

    private void CalculateIsFalling()
    {
        if (!FallThisFrame && !_collisionDown && _rigidbody.velocity.y < 0f)
        {
            FallThisFrame = true;
        }
        else if (FallThisFrame && _collisionDown)
        {
            FallThisFrame = false;
        }
    }

    private void ClampFallVelocity()
    {
        float yVelocity = Mathf.Clamp(_rigidbody.velocity.y, -_maxFallVelocity, float.MaxValue);

        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, yVelocity);
    }
}
