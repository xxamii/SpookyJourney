using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCombat : MonoBehaviour
{
    [SerializeField] private float _attackRadius;
    [SerializeField] private float _attackDistance;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private float _attackDuration;
    [SerializeField] private LayerMask _toAttack;
    [SerializeField] private float _damageAmount;

    private float _lastAttacked = float.MinValue;
    
    public bool TryAttack(Vector2 attackDirection, Vector2 attackPosition)
    {
        if (GameState.Instance.State == GameState.States.GameOn && _lastAttacked + _attackCooldown < Time.time)
        {
            Attack(attackDirection, attackPosition);
            _lastAttacked = Time.time;

            return true;
        }

        return false;
    }

    private void Attack(Vector2 attackDirection, Vector2 attackPosition)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPosition + attackDirection * _attackDistance, _attackRadius, _toAttack.value);

        foreach (Collider2D hit in hits)
        {
            IDamageable damageable = hit.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(_damageAmount, attackPosition);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + Vector3.right * _attackDistance, _attackRadius);
    }
}
