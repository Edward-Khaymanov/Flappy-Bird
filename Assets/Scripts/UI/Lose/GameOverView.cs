using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(RectTransform))]
public class GameOverView : MonoBehaviour
{
    [SerializeField] private float _appearanceDuration;
    [SerializeField] private float _shakeDuration;
    [SerializeField] private Vector2 _targetPosition;
    [SerializeField] private AnimationCurve _shakePattern;

    private Vector2 _defaultPosition;
    private Color _defaultColor;
    private Image _image;
    private RectTransform _rectTransform;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _defaultColor = _image.color;
        _rectTransform = GetComponent<RectTransform>();
        _defaultPosition = _rectTransform.anchoredPosition;
    }

    public IEnumerator Render()
    {
        var newColor = _image.color;
        newColor.a = 1;
        StartCoroutine(_image.ChangeColor(newColor, _appearanceDuration));
        yield return _rectTransform.Move(_rectTransform.anchoredPosition, _targetPosition, _shakeDuration, _shakePattern);
    }

    public void ResetView()
    {
        _image.color = _defaultColor;
        _rectTransform.anchoredPosition = _defaultPosition;
    }
}
