using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ParentRectScaler : MonoBehaviour
{
    [SerializeField] private Vector2 _referenceSize;

    private void Start()
    {
        var currentTransform = GetComponent<RectTransform>();
        var parentRectTransform = (RectTransform)transform.parent;
        var parentSize = parentRectTransform.rect.size;

        var aspectRatio = new Vector2(parentSize.x / _referenceSize.x, parentSize.y / _referenceSize.y);
        var newScale = new Vector3(currentTransform.localScale.x * aspectRatio.x, currentTransform.localScale.y * aspectRatio.x, currentTransform.localScale.z);
        currentTransform.localScale = newScale;
    }
}
