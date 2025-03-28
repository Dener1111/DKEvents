using UnityEngine;

namespace DKEvents
{
    public class DKObserver : MonoBehaviour
    {
        [SerializeField] bool once;

        [Space]
        [SerializeField] DKEventBase observable;
        public UnityEngine.Events.UnityEvent callback;

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

        public virtual void Dispatch() => callback?.Invoke();
    }
}