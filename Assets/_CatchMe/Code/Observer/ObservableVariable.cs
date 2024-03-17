using System;
using UnityEngine;

namespace AlexDev.Observer
{
    public class ObservableVariable<T> : IObservable
    {
        public event Action<object> ChangedEvent;

        private T _value;

        public T Value
        {
            get { return _value; }
            set
            {
                _value = value;
                ChangedEvent?.Invoke(_value);
            }
        }

        public ObservableVariable()
        {
            _value = default;
        }

        public ObservableVariable(T defaultValue)
        {
            _value = defaultValue;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
