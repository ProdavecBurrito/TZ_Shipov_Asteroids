using System.Collections.Generic;
using UnityEngine;

public static class ResourcesLoader
{
    public static GameObject LoadPrefab(string path)
    {
        return Resources.Load<GameObject>(path);
    }

    public static T LoadObject<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    public static T InstantiateObject<T>(T prefab) where T : Object
    {
        return Object.Instantiate(prefab);
    }

    public static T LoadAndInstantiateObject<T>(string path) where T : Object
    {
        var prefab = LoadObject<T>(path);
        return InstantiateObject(prefab);
    }
}
