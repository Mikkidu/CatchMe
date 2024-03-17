using System;

namespace AlexDev.Observer
{
    public interface IObserver : IDisposable
    {
        void AddObservable(IObservable observable);
    }
}
