using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class UIMover : MonoBehaviour
{
    [SerializeField] private float _appearanceDuration;
    [SerializeField] private Vector2 _targetPosition;
    [SerializeField] private AnimationCurve _appearancePattern;

    private Vector2 _defaultPosition;
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _defaultPosition = _rectTransform.anchoredPosition;
    }

    public IEnumerator Move()
    {
        return _rectTransform.Move(_rectTransform.anchoredPosition, _targetPosition, _appearanceDuration, _appearancePattern);
    }

    public void ResetPosition()
    {
        _rectTransform.anchoredPosition = _defaultPosition;
    }
}
