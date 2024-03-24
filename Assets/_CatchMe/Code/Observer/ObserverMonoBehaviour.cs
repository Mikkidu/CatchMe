using System.Collections.Generic;
using UnityEngine;

namespace AlexDev.Observer
{
    public abstract class ObserverMonoBehaviour : MonoBehaviour, IObserver
    {

        #region Private Fields

        private List<IObservable> _observables = new List<IObservable>();

        #endregion


        #region Public Methods

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
