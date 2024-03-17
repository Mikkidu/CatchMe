using System.Collections.Generic;

namespace AlexDev.Observer
{
    public abstract class ObserverAbstract : IObserver
    {
        private List<IObservable> _observables;

        public ObserverAbstract()
        {
            _observables = new List<IObservable>();
        }

        public ObserverAbstract(IObservable observable)
        {
            _observables = new List<IObservable> { observable };
            observable.ChangedEvent += OnChanged;
        }

        public ObserverAbstract(IObservable[] observables)
        {
            _observables = new List<IObservable>(observables);
            foreach (var observable in _observables)
            {
                observable.ChangedEvent += OnChanged;
            }
        }

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
