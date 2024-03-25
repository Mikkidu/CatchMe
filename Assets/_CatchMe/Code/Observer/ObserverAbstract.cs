using System.Collections.Generic;

namespace AlexDev.Observer
{
    public abstract class ObserverAbstract : IObserver
    {

        #region Private Fields

        private List<IObservable> _observables;

        #endregion

        #region Public Fields

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

        public void AddObservableVariable(IObservable observable)
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

        #endregion

        #region Protected Fields

        protected abstract void OnChanged(object o);

        #endregion
    }
}
