using System;
using System.Collections.Generic;
using UnityEngine;


public static class EventBus
{
    private static Dictionary<string, List<object>> _signalCallbacks = new Dictionary<string, List<object>>();


    public static void Subscribe<T>(Action<T> callback)
    {
        string key = typeof(T).Name;
        if (_signalCallbacks.ContainsKey(key))
            _signalCallbacks[key].Add(callback);
        else
            _signalCallbacks.Add(key, new List<object>() { callback });
    }


    public static void Invoke<T>(T signal)
    {
        string key = typeof(T).Name;
        if (_signalCallbacks.ContainsKey(key)) {
            foreach (var obj in _signalCallbacks[key]) {
                var callback = obj as Action<T>;
                callback?.Invoke(signal);
            }
        }
    }


    public static void Unsubscribe<T>(Action<T> callback)
    {
        string key = typeof(T).Name;
        if (_signalCallbacks.ContainsKey(key))
            _signalCallbacks[key].Remove(callback);
        else
            Debug.LogErrorFormat("Trying to unsubscribe for not existing key! {0} ", key);
    }
}
