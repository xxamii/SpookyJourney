using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerCollisionDetection : MonoBehaviour
{
    public bool CollisionDown => RunDetectionDown();

    [SerializeField] private Bounds _bounds;
    [SerializeField] private float _rayLength = 0.1f;
    [SerializeField][Range(0.1f, 0.3f)] private float _rayBuffer = 0.1f;
    [SerializeField] private int _detectionCount = 3;
    [SerializeField] private LayerMask _groundLayer;

    private Transform _transform;

    private RayRange _rayRangeDown;

    private void Start()
    {
        _transform = transform;
    }

    private bool RunDetectionDown()
    {
        CalculateRayRanges();
        return RunDetection(_rayRangeDown);
    }

    private void CalculateRayRanges()
    {
        Bounds currentBounds = new Bounds(transform.position + _bounds.center, _bounds.size);
        _rayRangeDown = new RayRange(currentBounds.min.x + _rayBuffer, currentBounds.min.y, currentBounds.max.x - _rayBuffer, currentBounds.min.y, Vector2.down);
    }

    private bool RunDetection(RayRange rayRange)
    {
        return CalculateRayPositions(rayRange).Any(point => Physics2D.Raycast(point, rayRange.Dir, _rayLength, _groundLayer.value));
    }

    private IEnumerable<Vector2> CalculateRayPositions(RayRange rayRange)
    {
        for (int i = 0; i < _detectionCount; i++)
        {
            float t = (float)i / (_detectionCount - 1);
            yield return Vector2.Lerp(rayRange.Start, rayRange.End, t);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Bounds
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + _bounds.center, _bounds.size);

        // Rays
        if (!Application.isPlaying)
        {
            CalculateRayRanges();
            Gizmos.color = Color.blue;
            foreach (var point in CalculateRayPositions(_rayRangeDown))
            {
                Gizmos.DrawRay(point, _rayRangeDown.Dir * _rayLength);
            }
        }
    }
}
