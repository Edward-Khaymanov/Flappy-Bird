using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformAnimator : MonoBehaviour
{
    [SerializeField] private bool _useOffsetPosition;
    [SerializeField] private bool _isLooped;
    [SerializeField] private float _duration;
    [SerializeField] private Vector3 _targetPosition;
    [SerializeField] private AnimationCurve _pattern;

    private Vector3 _correctedTargetPosition;
    private IEnumerator _animation;
    private IEnumerator _action;
    
    public void StartAnimation()
    {
        SetTargetPosition();

        if (_isLooped)
            _action = LoopMove();
        else
            _action = Move();

        StartCoroutine(_action);
    }

    public void StopAnimation()
    {
        StopCoroutine(_action);
        StopCoroutine(_animation);
    }

    private IEnumerator LoopMove()
    {
        while (true)
        {
            SetMoveAnimation();
            yield return StartCoroutine(_animation);
        }
    }

    private IEnumerator Move()
    {
        SetMoveAnimation();
        yield return StartCoroutine(_animation);
    }

    private void SetTargetPosition()
    {
        _correctedTargetPosition = _targetPosition;

        if (_useOffsetPosition)
            _correctedTargetPosition += transform.position;
    }

    private void SetMoveAnimation()
    {
        _animation = transform.Move(transform.position, _correctedTargetPosition, _duration, _pattern);
    }
}
