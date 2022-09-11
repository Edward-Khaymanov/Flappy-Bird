using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class TrainingView : MonoBehaviour
{
    [SerializeField] private float _fadeDuration;

    private CanvasGroup _canvasGroup;
    private IEnumerator _fade;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        EventsHolder.GameStarted += FadeIn;
        EventsHolder.FirstJumping += FadeOut;
        EventsHolder.MenuOpened += OnMenu;
    }

    private void OnDisable()
    {
        EventsHolder.GameStarted -= FadeIn;
        EventsHolder.FirstJumping -= FadeOut;
        EventsHolder.MenuOpened -= OnMenu;
    }

    private void FadeIn()
    {
        _fade = _canvasGroup.Fade(1f, _fadeDuration);
        StartCoroutine(_fade);
    }

    private void FadeOut()
    {
        StopCoroutine(_fade);
        _fade = _canvasGroup.Fade(0f, _fadeDuration);
        StartCoroutine(_fade);
    }

    private void OnMenu()
    {
        _canvasGroup.alpha = 0f;
    }
}
