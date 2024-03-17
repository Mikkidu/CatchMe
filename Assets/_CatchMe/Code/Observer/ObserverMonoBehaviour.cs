using System.Collections.Generic;
using UnityEngine;

namespace AlexDev.Observer
{
    public abstract class ObserverMonoBehaviour : MonoBehaviour, IObserver
    {
        private List<IObservable> _observables = new List<IObservable>();

        public void AddObservable(IObservable observable)
        {
            _observables.Add(observable);
            observable.ChangedEvent += OnChanged;
        }

        public void Dispose()
        {
            foreach (var observable in _observables)
            {
                observable.ChangedEvent -= OnChanged;
            }
        }

        protected abstract void OnChanged(object o);

    }
}
