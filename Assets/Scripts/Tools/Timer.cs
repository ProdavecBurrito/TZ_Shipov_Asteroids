using System;
using UnityEngine;

public class Timer
{
    public event Action StartCountDown = delegate () { };
    public event Action EndCountDown = delegate () { };

    public bool IsOn;
    public float EndTime;

    public float CurrentTime { get; private set; }

    public void Init(float endtime)
    {
        EndTime = endtime;
        IsOn = true;
        UpdatingController.SubscribeToTUpdate(CountTime);
        StartCountDown?.Invoke();
    }

    public void Reset()
    {
        CurrentTime = 0.0f;
        IsOn = false;
        UpdatingController.UnsubscribeFromUpdate(CountTime);
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
            //Debug.Log(CurrentTime);
            if (CurrentTime < EndTime)
            {
                CurrentTime += Time.deltaTime;
            }
            else
            {
                EndCountDown?.Invoke();
                Reset();
            }
        }
    }
}
