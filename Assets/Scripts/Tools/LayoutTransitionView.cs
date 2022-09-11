using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LayoutTransitionView : MonoBehaviour
{
    [SerializeField] private float _duration;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public IEnumerator Render(Action transitionAction = null, Action endAction = null)
    {
        var newColor = _image.color;

        newColor.a = 1f;
        yield return StartCoroutine(_image.ChangeColor(newColor, _duration / 2f));

        transitionAction?.Invoke();

        newColor.a = 0f;
        yield return StartCoroutine(_image.ChangeColor(newColor, _duration / 2f));

        endAction?.Invoke();
    }
}
