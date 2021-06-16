using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class BasePool<T> : IUpdate, IDisposable
{
    protected List<T> _poolObjects = new List<T>();
    protected int _currentObjectIndex;

    public List<T> PollObjects => _poolObjects;

    public BasePool(int count)
    {
        _currentObjectIndex = 0;
    }

    public void AddToPool(T poolObject)
    {
        if(!_poolObjects.Contains(poolObject))
        {
            _poolObjects.Add(poolObject);
        }
    }

    private void RemoveFromPool(int index)
    {
        if (_poolObjects.Contains(_poolObjects[index]))
        {
            _poolObjects.Remove(_poolObjects[index]);
        }
    }

    public void Dispose()
    {
        _poolObjects.Clear();
    }

    public abstract void TryToAct(Transform startPosition);

    public abstract void UpdateTick();
}
