using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ObjectPool : MonoBehaviour
{
    [SerializeField] protected GameObject _template;
    [SerializeField] protected Transform _container;
    [SerializeField] protected bool _autoExpand;

    protected List<GameObject> _pool = new List<GameObject>();

    protected virtual void Initialize(int capacity)
    {
        _pool = new List<GameObject>();
        for (int i = 0; i < capacity; i++)
        {
            CreateObject(false);
        }
    }

    protected virtual GameObject CreateObject(bool isActive = false)
    {
        var newObject = Instantiate(_template, _container);
        newObject.SetActive(isActive);
        _pool.Add(newObject);
        return newObject;
    }

    protected virtual bool TryGetObject(out GameObject result)
    {
        result = _pool.FirstOrDefault(o => o.activeSelf == false);

        if (result == null && _autoExpand)
            result = CreateObject(true);

        return result != null;
    }

    protected virtual void ResetPool()
    {
        foreach (var item in _pool)
        {
            item.SetActive(false);
        }
    }
}
