using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearScale : MonoBehaviour
{
    [SerializeField] private RectTransform _scale;
    [SerializeField] private float _minWidth;
    [SerializeField] private float _maxWidth;
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;

    private void Start()
    {
        FearMeter.Instance.OnFearChanged += UpdateScale;

        UpdateScale();
    }

    private void OnDisable()
    {
        if (FearMeter.Instance != null)
        {
            FearMeter.Instance.OnFearChanged -= UpdateScale;
        }
    }

    private void UpdateScale()
    {
        float maxFear = FearMeter.Instance.MaxFear;
        float fear = FearMeter.Instance.CurrentFear;

        float t = fear / maxFear;
        float width = Mathf.Lerp(_minWidth, _maxWidth, t);
        float x = Mathf.Lerp(_minX, _maxX, t);
        _scale.anchoredPosition = new Vector2(x, _scale.anchoredPosition.y);
        _scale.sizeDelta = new Vector2(width, _scale.sizeDelta.y);
    }
}
