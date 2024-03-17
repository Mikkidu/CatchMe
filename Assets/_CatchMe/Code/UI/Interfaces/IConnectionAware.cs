using UnityEngine;

namespace AlexDev.CatchMe
{
    public interface IConnectionAware
    {
        void OnConnectionChanged(bool isConnected);
    }
}
