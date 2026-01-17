using System;
using System.Collections.Generic;

namespace Code.Core.Event
{
    public class EventSystem : Singleton<EventSystem>, IEventSystem
    {
        private readonly Dictionary<Type, object> _handlers = new();
        public Action<string> OnError;

        public void Publish<T>(T t)
        {
            var type = typeof(T);
            if (!_handlers.TryGetValue(type, out var listObj) || listObj is not List<object> handlers)
            {
                return;
            }

            foreach (var handler in handlers)
            {
                try
                {
                    if (handler is IEvent<T> eventHandler)
                    {
                        eventHandler.Process(t);
                    }
                }
                catch (Exception e)
                {
                    OnError?.Invoke(e.ToString());
                }
            }
        }

        public IDisposable Subscribe<T>(Action<T> action)
        {
            var type = typeof(T);
            if (!_handlers.ContainsKey(type))
            {
                _handlers.Add(type, new List<object>());
            }

            var eventSubscriber = new EventSubscriber<T>(this, action);
            ((List<object>)_handlers[type]).Add(eventSubscriber);
            return eventSubscriber;
        }

        public IDisposable Subscribe<T>(IEvent<T> subscriber)
        {
            var type = typeof(T);
            if (!_handlers.ContainsKey(type))
            {
                _handlers.Add(type, new List<object>());
            }

            ((List<object>)_handlers[type]).Add(subscriber);
            return subscriber;
        }

        public void UnSubscribe<T>(Action<T> action)
        {
            var type = typeof(T);
            if (_handlers.TryGetValue(type, out var handlers))
            {
                ((List<object>)handlers).RemoveAll(x =>
                {
                    if (x is EventSubscriber<T> subscriber)
                    {
                        return subscriber.Action == action;
                    }

                    return false;
                });
            }
        }

        public void UnSubscribe<T>(IEvent<T> subscriber)
        {
            var type = typeof(T);
            if (_handlers.TryGetValue(type, out var handlers))
            {
                ((List<object>)handlers).Remove(subscriber);
            }
        }

        private class EventSubscriber<T> : IEvent<T>
        {
            private readonly IEventSystem _system;
            public readonly Action<T> Action;

            public EventSubscriber(IEventSystem system, Action<T> action)
            {
                _system = system;
                Action = action;
            }

            public void Process(T t)
            {
                Action?.Invoke(t);
            }

            public void Dispose()
            {
                _system.UnSubscribe(this);
            }
        }
    }
}