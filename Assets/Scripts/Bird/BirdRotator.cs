using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Bird))]
public class BirdRotator : MonoBehaviour
{
    [SerializeField] private Quaternion _jumpAngle;
    [SerializeField] private Quaternion _fallAngle;
    [SerializeField] private AnimationCurve _rotationCurve;
    [SerializeField] private float _duration;

    private IEnumerator _rotateCoroutine;

    private void Awake()
    {
        _rotateCoroutine = Rotate(_duration);
    }

    public void StartActions()
    {
        ResetRotator();
    }

    public void FirstJumpActions()
    {
        Restart();
    }

    public void MenuActions()
    {
        StopCoroutine(_rotateCoroutine);
        ResetRotator();
    }

    public void JumpActions()
    {
        StopCoroutine(_rotateCoroutine);
        transform.rotation = _jumpAngle;
        Restart();
    }

    public void FellActions()
    {
        StopCoroutine(_rotateCoroutine);
    }

    private IEnumerator Rotate(float duration)
    {
        var startAngle = transform.rotation.eulerAngles;
        var angleDifference = Quaternion.Angle(transform.rotation, _fallAngle);
        var expiredSeconds = 0f;
        var progress = 0f;
        var curveZ = 0f;
        var newRotationZ = 0f;
        var newRotation = Quaternion.identity;

        while (progress < 1)
        {
            progress = expiredSeconds / duration;
            curveZ = _rotationCurve.Evaluate(progress);
            newRotationZ = startAngle.z - curveZ * angleDifference;
            newRotation = Quaternion.Euler(startAngle.x, startAngle.y, newRotationZ);
            transform.rotation = newRotation;

            expiredSeconds += Time.deltaTime;
            yield return null;
        }

        yield break;
    }

    private void ResetRotator()
    {
        transform.rotation = Quaternion.identity;
    }

    private void Restart()
    {
        _rotateCoroutine = Rotate(_duration);
        StartCoroutine(_rotateCoroutine);
    }
}
