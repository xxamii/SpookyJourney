using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Health : MonoBehaviour, IHealth
{
    public Action OnTakeDamage;
    public Action<Health> OnDie;

    [SerializeField] private float _maxHealth;
    [SerializeField] private float _safeTime;

    private Destroyer _destroyer;

    private float _health;
    private float _lastHit;

    private void Start()
    {
        _destroyer = GetComponent<Destroyer>();

        _health = _maxHealth;
    }

    public void TakeDamage(float amount)
    {
        if (GameState.Instance.State == GameState.States.GameOn && _lastHit + _safeTime < Time.time)
        {
            _health -= amount;

            if (_health <= 0f)
            {
                Die();
                return;
            }

            OnTakeDamage?.Invoke();
            _lastHit = Time.time;
        }
    }

    private void Die()
    {
        OnDie?.Invoke(this);
        _destroyer.Destroy();
    }
}
