using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Transform _sprite;
    [SerializeField] private ParticleSystem _landParticles;
    [SerializeField] private ParticleSystem _walkParticles;
    [SerializeField] private ParticleSystem _jumpParticles;
    [SerializeField] private AudioClip _jumpAudio, _landAudio, _stepAudio, _slashAudio;

    private Animator _animator;
    private PlayerController _playerController;
    private PlayerCollisionDetection _collision;
    private PlayerInput _playerInput;
    private PlayerMeleeCombat _combat;

    private int _previousDirection = 1;

    private readonly string _runBool = "IsRunning";
    private readonly string _jumpTrigger = "Jump";
    private readonly string _landTrigger = "Land";
    private readonly string _hitTrigger = "Hit";

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
        _collision = GetComponent<PlayerCollisionDetection>();
        _playerInput = GetComponent<PlayerInput>();
        _combat = GetComponent<PlayerMeleeCombat>();
    }

    private void Update()
    {
        CalculateDirection();
        TryRunAnimations();
    }

    private void FixedUpdate()
    {
        TryLandAnimation();
        TryJumpAnimation();
    }

    public void PlayStepAudio()
    {
        AudioPlayer.Instance.PlaySound(_stepAudio, 0.4f);
    }

    public void PlayLandAudio()
    {
        AudioPlayer.Instance.PlaySound(_landAudio, 0.35f);
    }

    public void PlayJumpAudio()
    {
        AudioPlayer.Instance.PlaySound(_jumpAudio);
    }

    public void PlaySlashAudio()
    {
        AudioPlayer.Instance.PlaySound(_slashAudio);
    }

    private void CalculateDirection()
    {
        FrameInput frameInput = _playerInput.Input;

        if (frameInput.X > 0f && _previousDirection == -1 ||
            frameInput.X < 0f && _previousDirection == 1)
        {
            FlipSprite();
        }
    }

    private void FlipSprite()
    {
        _sprite.localScale = new Vector3(_sprite.localScale.x * -1, _sprite.localScale.y, _sprite.localScale.z);
        _previousDirection *= -1;
    }

    private void TryRunAnimations()
    {
        TryRunAnimation();
        TryHitAnimation();
    }

    private void TryRunAnimation()
    {
        bool isRunning = Mathf.FloorToInt(_playerInput.Input.X) != 0;
        _animator.SetBool(_runBool, isRunning);

        if (isRunning && _collision.CollisionDown)
        {
            _walkParticles.Play();
        }
        else
        {
            _walkParticles.Stop();
        }
    }

    private void TryJumpAnimation()
    {
        ToggleTrigger(_jumpTrigger, _playerController.JumpThisFrame);

        if (_playerController.JumpThisFrame)
        {
            ToggleTrigger(_landTrigger, false);
            _jumpParticles.Play();
            PlayJumpAudio();
        }
    }

    private void TryLandAnimation()
    {
        ToggleTrigger(_landTrigger, _playerController.LandThisFrame);

        if (_playerController.LandThisFrame)
        {
            _landParticles.Play();
            PlayLandAudio();
        }
    }

    private void TryHitAnimation()
    {
        ToggleTrigger(_hitTrigger, _combat.AttackingThisFrame);
    }

    private void ToggleTrigger(string trigger, bool condition)
    {
        if (condition)
        {
            _animator.SetTrigger(trigger);
        }
        else
        {
            _animator.ResetTrigger(trigger);
        }
    }
}
