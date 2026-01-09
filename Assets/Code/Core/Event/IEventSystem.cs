using System;

namespace Code.Core.Event
{
    public interface IEventSystem
    {
        public void Publish<T>(T t);
        public IDisposable Subscribe<T>(Action<T> action);
        public IDisposable Subscribe<T>(IEvent<T> subscriber);

        public void UnSubscribe<T>(Action<T> action);
        public void UnSubscribe<T>(IEvent<T> subscriber);
    }
}