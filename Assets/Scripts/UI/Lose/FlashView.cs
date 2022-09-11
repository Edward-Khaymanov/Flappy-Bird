using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FlashView : MonoBehaviour
{
    [SerializeField] private float _duration;

    private Color _defaultColor;
    private Image _image;


    private void Awake()
    {
        _image = GetComponent<Image>();
        _defaultColor = _image.color;
    }

    public IEnumerator Render()
    {
        var newColor = _image.color;
        newColor.a = 0f;
        yield return StartCoroutine(_image.ChangeColor(newColor, _duration));
    }

    public void ResetView()
    {
        _image.color = _defaultColor;
    }
}
