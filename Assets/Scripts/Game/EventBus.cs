using System;
using System.Collections.Generic;

public static class EventBus
{
    private static readonly Dictionary<Type, Delegate> _eventTable = new Dictionary<Type, Delegate>();

    public static void Subscribe<T>(Action<T> listener)
    {
        if (_eventTable.TryGetValue(typeof(T), out var del))
        {
            _eventTable[typeof(T)] = Delegate.Combine(del, listener);
        }
        else
        {
            _eventTable[typeof(T)] = listener;
        }
    }

    public static void Unsubscribe<T>(Action<T> listener)
    {
        if (_eventTable.TryGetValue(typeof(T), out var del))
        {
            var currentDel = Delegate.Remove(del, listener);

            if (currentDel == null)
                _eventTable.Remove(typeof(T));
            else
                _eventTable[typeof(T)] = currentDel;
        }
    }

    public static void Publish<T>(T eventData)
    {
        if (_eventTable.TryGetValue(typeof(T), out var del))
        {
            var callback = del as Action<T>;
            callback?.Invoke(eventData);
        }
    }
}
