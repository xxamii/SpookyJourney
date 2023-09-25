using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private RectangleArea[] _areas;
    [SerializeField] private float _spawnCooldown;
    [SerializeField] private float _spawnCooldownModifier;
    [SerializeField] private Destroyer _toSpawn;
    [SerializeField] private int _toKill;

    private Transform _transform;

    private float _currentCooldown;
    private float _lastSpawned;
    private bool _isSpawning = true;

    private void Start()
    {
        _transform = transform;
        _currentCooldown = _spawnCooldown;
    }

    private void Update()
    {
        if (GameState.Instance.State == GameState.States.Pause || !_isSpawning)
        {
            return;
        }

        TrySpawn();
    }

    private void TrySpawn()
    {
        if (_lastSpawned + _currentCooldown < Time.time)
        {
            Spawn();
            _lastSpawned = Time.time;
            _currentCooldown = _spawnCooldown - Random.Range(0, _spawnCooldownModifier);
        }
    }

    private void Spawn()
    {
        Vector2 position = PickRandomPoint();
        Destroyer enemy = Instantiate(_toSpawn, position, Quaternion.identity);
        enemy.OnDestroyed += OnEnemyDie;
    }

    private Vector2 PickRandomPoint()
    {
        RectangleArea area = _areas[Random.Range(0, _areas.Length)];
        float x = Random.Range(area.TopLeft.x, area.BottomRight.x);
        float y = Random.Range(area.BottomRight.y, area.TopLeft.y);
        Vector2 point = new Vector2(x, y) + (Vector2)_transform.position;
        return point;
    }

    private void OnEnemyDie(Destroyer enemy)
    {
        _toKill--;

        if (_toKill <= 0)
        {
            Win();
        }

        enemy.OnDestroyed -= OnEnemyDie;
    }

    private void Win()
    {
        _isSpawning = false;
        GameState.Instance.Win();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        foreach(RectangleArea area in _areas)
        {
            Vector3 TopRight = new Vector3(area.BottomRight.x, area.TopLeft.y) + transform.position;
            Vector3 BottomLeft = new Vector3(area.TopLeft.x, area.BottomRight.y) + transform.position;
            Vector3 TopLeft = (Vector3)area.TopLeft + transform.position;
            Vector3 BottomRight = (Vector3) area.BottomRight + transform.position;
            Gizmos.DrawLine(TopLeft, TopRight);
            Gizmos.DrawLine(TopRight, BottomRight);
            Gizmos.DrawLine(BottomRight, BottomLeft);
            Gizmos.DrawLine(BottomLeft, TopLeft);
        }
    }
}
