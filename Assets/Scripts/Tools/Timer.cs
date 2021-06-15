using System;
using UnityEngine;

public class Timer : IDisposable
{
    public event Action OnStartCountDown = delegate () { };
    public event Action OnEndCountDown = delegate () { };
    public event Action OnFrequencyStarted = delegate () { };
    public event Action OnFrequencyEnd = delegate () { };

    public bool IsOn;
    public bool IsFrequencyStarted;
    private int _currentfrequency;
    private int _frequenceCount;
    public float EndTime;

    public float CurrentTime { get; private set; }

    public void Init(float endtime)
    {
        EndTime = endtime;
        IsOn = true;
        OnStartCountDown?.Invoke();
    }

    public void Reset()
    {
        CurrentTime = 0.0f;
        IsOn = false;
    }

    public bool IsTimeOver()
    {
        if (CurrentTime < EndTime)
        {
            return false;
        }
        return true;
    }

    public void CountTime()
    {
        if (IsOn)
        {
            if (CurrentTime < EndTime)
            {
                CurrentTime += Time.deltaTime;
            }
            else
            {
                OnEndCountDown?.Invoke();
                Reset();
            }
        }
    }

    public void InitFrequency(int count, float time)
    {
        _currentfrequency = 0;
        _frequenceCount = count;
        Init(time);
        _currentfrequency++;
        OnFrequencyStarted.Invoke();
    }

    public void CheckFrequency()
    {
        if (!IsOn)
        {
            if (_currentfrequency <= _frequenceCount)
            {
                Init(EndTime);
                _currentfrequency++;
            }
            else
            {
                OnFrequencyEnd.Invoke();
            }
        }
        else
        {
            CountTime();
        }
    }

    public void Dispose()
    {
        CurrentTime = 0;
    }
}
