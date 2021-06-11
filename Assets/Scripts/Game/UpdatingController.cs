﻿using System.Collections.Generic;

public static class UpdatingController
{
    private static List<IUpdate> _updatingObjects = new List<IUpdate>();
    private static List<IFixedUpdate> _fixedUpdates = new List<IFixedUpdate>();

    public static void UpdateAll()
    {
        for (int i = 0; i < _updatingObjects.Count; i++)
        {
            _updatingObjects[i].UpdateTick();
        }
    }

    public static void FixedUpdateAll()
    {
        for (int i = 0; i < _fixedUpdates.Count; i++)
        {
            _fixedUpdates[i].FixedUpdateTick();
        }
    }

    public static void AddToFixedUpdate(IFixedUpdate update)
    {
        if (!_fixedUpdates.Contains(update))
        {
            _fixedUpdates.Add(update);
        }
    }

    public static void RemoveFromFixedUpdate(IFixedUpdate update)
    {
        if (_fixedUpdates.Contains(update))
        {
            _fixedUpdates.Remove(update);
        }
    }

    public static void AddToUpdate(IUpdate update)
    {
        if (!_updatingObjects.Contains(update))
        {
            _updatingObjects.Add(update);
        }
    }

    public static void RemoveFromUpdate(IUpdate update)
    {
        if (_updatingObjects.Contains(update))
        {
            _updatingObjects.Remove(update);
        }
    }
}