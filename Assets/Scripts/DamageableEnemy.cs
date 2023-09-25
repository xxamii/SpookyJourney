using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableEnemy : MonoBehaviour, IDamageable
{
    private IHealth _health;
    private SimpleNightmareAI _enemyAi;

    private void Start()
    {
        _health = GetComponent<IHealth>();
        _enemyAi = GetComponent<SimpleNightmareAI>();
    }

    public void TakeDamage(float amount, Vector2 from)
    {
        _health.TakeDamage(amount);
        _enemyAi.Knockback(from);
    }
}
