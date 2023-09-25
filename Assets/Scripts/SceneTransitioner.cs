using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitioner : Singleton<SceneTransitioner>
{
    public bool FadedInThisFrame { get; set; }
    public bool FadingOutThisFrame { get; set; }
    public bool RestartingThisFrame { get; set; }

    [SerializeField] private string _nextScene;
    [SerializeField] private float _fadeOutDuration;
    [SerializeField] private float _fadeInDuration;
    [SerializeField] private float _transitionDelay;
    [SerializeField] private RectTransform _endPosition;
    [SerializeField] private RectTransform _startPosition;
    [SerializeField] private RectTransform _wipe;

    private bool _inTransition;
    private bool _fadedInLastFrame;
    private float _fadeOutProgress;
    private float _fadeInProgress = 0f;
    private string _transitionTo;

    private void Start()
    {
        FadeIn();
    }

    private void Update()
    {
        if (!_fadedInLastFrame && FadedInThisFrame)
        {
            _fadedInLastFrame = true;
        }
        else if (_fadedInLastFrame && FadedInThisFrame)
        {
            FadedInThisFrame = false;
        }
    }

    public void StartTransitionNext()
    {
        if (!_inTransition)
        {
            _transitionTo = _nextScene;
            StartTransition();
        }
    }

    public void Restart()
    {
        if (!_inTransition)
        {
            RestartingThisFrame = true;
            _transitionTo = SceneManager.GetActiveScene().name;
            StartTransition();
        }
    }

    private void StartTransition()
    {
        _inTransition = true;
        FadingOutThisFrame = true;
        FadeOut();
    }

    private void FadeOut()
    {
        _wipe.position = _endPosition.position;
        _wipe.localScale = _endPosition.localScale;
        _wipe.rotation = _endPosition.rotation;
        _wipe.sizeDelta = _endPosition.sizeDelta;
        _wipe.gameObject.SetActive(true);
        _fadeOutProgress = 0f;
        FadingOutThisFrame = true;
        StartCoroutine(nameof(FadeOutRoutine));
    }

    private IEnumerator FadeOutRoutine()
    {
        yield return new WaitForSeconds(_transitionDelay);

        while (_fadeOutProgress < _fadeOutDuration)
        {
            float t = _fadeOutProgress / _fadeOutDuration;
            LerpWipe(_endPosition, _startPosition, t);
            _fadeOutProgress += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        RestartingThisFrame = false;
        SceneManager.LoadScene(_transitionTo);
    }

    private void FadeIn()
    {
        _inTransition = true;
        _fadeInProgress = 0f;
        _wipe.gameObject.SetActive(true);
        _wipe.position = _startPosition.position;
        _wipe.localScale = _startPosition.localScale;
        _wipe.rotation = _startPosition.rotation;
        _wipe.sizeDelta = _startPosition.sizeDelta;
        StartCoroutine(nameof(FadeInRoutine));
    }

    private IEnumerator FadeInRoutine()
    {
        while (_fadeInProgress < _fadeInDuration)
        {
            float t = _fadeInProgress / _fadeInDuration;
            LerpWipe(_startPosition, _endPosition, t);
            _fadeInProgress += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        _wipe.gameObject.SetActive(false);
        FadedInThisFrame = true;
        _inTransition = false;
    }

    private void LerpWipe(RectTransform from, RectTransform to, float t)
    {
        _wipe.position = Vector3.Lerp(from.position, to.position, t);
        _wipe.localScale = Vector3.Lerp(from.localScale, to.localScale, t);
        _wipe.rotation = Quaternion.Lerp(from.rotation, to.rotation, t);
        _wipe.sizeDelta = Vector2.Lerp(from.sizeDelta, to.sizeDelta, t);
    }
}
