using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private RectangleArea _clamp;
    [SerializeField] private Transform _toFollow;
    [SerializeField] private float _moveThreshold;
    [SerializeField] private float _smoothFactor;

    private Transform _transform;

    private void Start()
    {
        _transform = transform;
    }

    private void FixedUpdate()
    {
        CalculateNextPosition();
    }

    private void CalculateNextPosition()
    {
        float x = Mathf.Clamp(_toFollow.position.x, _clamp.TopLeft.x, _clamp.BottomRight.x);
        float y = Mathf.Clamp(_toFollow.position.y, _clamp.BottomRight.y, _clamp.TopLeft.y);
        Vector3 nextPosition = new Vector3(x, y, -10);

        if (Vector3.Distance(_transform.position, nextPosition) >= _moveThreshold)
        {
            _transform.position = Vector3.Lerp(_transform.position, nextPosition, _smoothFactor * 10 * Time.fixedDeltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 TopRight = new Vector3(_clamp.BottomRight.x, _clamp.TopLeft.y);
        Vector3 BottomLeft = new Vector3(_clamp.TopLeft.x, _clamp.BottomRight.y);
        Vector3 TopLeft = (Vector3)_clamp.TopLeft;
        Vector3 BottomRight = (Vector3)_clamp.BottomRight;
        Gizmos.DrawLine(TopLeft, TopRight);
        Gizmos.DrawLine(TopRight, BottomRight);
        Gizmos.DrawLine(BottomRight, BottomLeft);
        Gizmos.DrawLine(BottomLeft, TopLeft);
    }
}
