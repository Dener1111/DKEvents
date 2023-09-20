using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKEvents
{
    public class DKObserverBase<T> : MonoBehaviour
    {
        [SerializeField] bool once;

        [Space]
        [SerializeField] DKEventBase<T> observable;
        public UnityEngine.Events.UnityEvent<T> callback;

        protected virtual void Awake()
        {
            if(observable == null) return;

            if (once) observable.AddOnce(Raise);
            else observable.AddListener(Raise);
        }

        protected virtual void OnDestroy()
        {
            if(observable == null) return;

            if (once) observable.RemoveOnce(Raise);
            else observable.RemoveListener(Raise);
        }

        public virtual void Raise(T value) => callback?.Invoke(value);
    }
}