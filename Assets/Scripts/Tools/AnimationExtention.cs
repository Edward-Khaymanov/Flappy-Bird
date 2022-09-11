using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class AnimationExtention
{
    public static IEnumerator Move(this RectTransform original, Vector2 from, Vector2 to, float duration, AnimationCurve pattern)
    {
        var expiredSeconds = 0f;
        var progress = 0f;
        var difference = to - from;
        var curvePosition = 0f;
        var newPosition = Vector2.zero;

        while (progress < 1)
        {
            progress = expiredSeconds / duration;
            curvePosition = pattern.Evaluate(progress);
            newPosition = from + curvePosition * difference;
            original.anchoredPosition = newPosition;

            expiredSeconds += Time.deltaTime;
            yield return null;
        }
    }

    public static IEnumerator Move(this Transform original, Vector3 from, Vector3 to, float duration, AnimationCurve pattern)
    {
        var expiredSeconds = 0f;
        var progress = 0f;
        var difference = to - from;
        var curvePosition = 0f;
        var newPosition = Vector3.zero;

        while (progress < 1)
        {
            progress = expiredSeconds / duration;
            curvePosition = pattern.Evaluate(progress);
            newPosition = from + curvePosition * difference;
            original.position = newPosition;

            expiredSeconds += Time.deltaTime;
            yield return null;
        }
    }

    public static IEnumerator ChangeColor(this Image original, Color targetColor, float time)
    {
        var startColor = original.color;
        var expiredSeconds = 0f;
        var progress = 0f;

        while (progress < 1)
        {
            progress = expiredSeconds / time;
            original.color = Color.Lerp(startColor, targetColor, progress);

            expiredSeconds += Time.unscaledDeltaTime;
            yield return null;
        }
    }

    public static IEnumerator Fade(this CanvasGroup original, float targetAlpha, float duration)
    {
        var expiredSeconds = 0f;
        var progress = 0f;
        var startAlpha = original.alpha;

        while (progress < 1)
        {
            progress = expiredSeconds / duration;
            original.alpha = Mathf.MoveTowards(startAlpha, targetAlpha, progress);

            expiredSeconds += Time.deltaTime;
            yield return null;
        }
    }

    public static IEnumerator StartCount(this TMP_Text original, int from, int to, float duration, AnimationCurve pattern)
    {
        var expiredSeconds = 0f;
        var progress = 0f;
        var difference = to - from;
        var curvePosition = 0f;
        var newValue = 0;

        while (progress < 1)
        {
            progress = expiredSeconds / duration;
            curvePosition = pattern.Evaluate(progress);
            newValue = from + (int)(curvePosition * difference);
            original.text = $"{newValue}";

            expiredSeconds += Time.deltaTime;
            yield return null;
        }
    }
}
