using System;

namespace AlexDev.Observer
{
    public interface IObservable
    {
        event Action<object> ChangedEvent;
    }
}
