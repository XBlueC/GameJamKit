using System;

namespace Code.Core.Event
{
    public interface IEvent<T> : IDisposable
    {
        void Process(T t);
    }
}