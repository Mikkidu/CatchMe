using System;

namespace AlexDev.Observer
{
    public interface IObserver : IDisposable
    {
        void AddObservableVariable(IObservable observable);
    }
}
