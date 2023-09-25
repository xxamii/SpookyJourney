using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyAnimations : MonoBehaviour
{
    [SerializeField] private AudioClip _hitAudio;
    [SerializeField] private AudioClip _dieAudio;
    [SerializeField] private GameObject _deathParticles;

    private Trigger _trigger;
    private Animator _animator;
    private Health _health;

    private readonly string _applyTrigger = "Apply";
    private readonly string _getHitTrigger = "GetHit";

    private void Start()
    {
        _trigger = GetComponent<Trigger>();
        _animator = GetComponent<Animator>();
        _health = GetComponent<Health>();

        _trigger.OnTrigger += OnTrigger;
        _health.OnTakeDamage += PlayGetHit;
        _health.OnDie += PlayDie;
    }

    private void OnDisable()
    {
        _trigger.OnTrigger -= OnTrigger;
        _health.OnTakeDamage -= PlayGetHit;
        _health.OnDie -= PlayDie;
    }

    private void PlayGetHit()
    {
        _animator.SetTrigger(_getHitTrigger);
        AudioPlayer.Instance.PlaySound(_hitAudio, 0.5f);
    }

    private void PlayDie(Health health)
    {
        Instantiate(_deathParticles, transform.position, Quaternion.identity);
        AudioPlayer.Instance.PlaySound(_dieAudio, 0.5f);
    }

    private void OnTrigger(Collider2D collider2D)
    {
        if (collider2D.GetComponent<Chariot>() != null)
        {
            _animator.SetTrigger(_applyTrigger);
        }
    }
}
