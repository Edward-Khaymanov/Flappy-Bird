using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextAnimator : MonoBehaviour
{
    [SerializeField] private float _countSpeed;
    [SerializeField] private TMP_Text _currentScore;
    [SerializeField] private AnimationCurve _countPattern;

    public IEnumerator Count(int from, int to)
    {
        yield return _currentScore.StartCount(from, to, _countSpeed, _countPattern);
    }

    public void ResetText()
    {
        _currentScore.text = $"{0}";
    }
}
