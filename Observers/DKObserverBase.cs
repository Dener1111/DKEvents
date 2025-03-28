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

            if (once) observable.AddOnce(Dispatch);
            else observable.AddListener(Dispatch);
        }

        protected virtual void OnDestroy()
        {
            if(observable == null) return;

            if (once) observable.RemoveOnce(Dispatch);
            else observable.RemoveListener(Dispatch);
        }

        public virtual void Dispatch(T value) => callback?.Invoke(value);
    }
}