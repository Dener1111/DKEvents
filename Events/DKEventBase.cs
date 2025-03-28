using System;
using System.Collections;
using System.Collections.Generic;
using Sigtrap.Relays;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DKEvents
{
    public class DKEventBase : ScriptableObject
    {
        Relay change;

        [InfoBox("@\"Active Listeners: \" + change.listenerCount", InfoMessageType.None)]
        [FoldoutGroup("Debug")]
        [PropertyOrder(100)]
        [LabelWidth(100f)]
        [SerializeField]
        bool logEvent;

        void OnEnable()
        {
            if (change == null)
                change = new Relay();
        }

        [FoldoutGroup("Debug")]
        [PropertyOrder(100)]
        [Button]
        public virtual void Invoke()
        {
            change.Dispatch();
#if UNITY_EDITOR
            Log();
#endif
        }

        public void AddOnce(Action action, bool allowDuplicates = false) => change.AddOnce(action, allowDuplicates);
        public void RemoveOnce(Action action) => change.RemoveOnce(action);
        public void AddListener(Action action, bool allowDuplicates = false) => change.AddListener(action, allowDuplicates);
        public void RemoveListener(Action action) => change.RemoveListener(action);

        [FoldoutGroup("Debug")]
        [PropertyOrder(110)]
        [Button("Remove All Listeners")]
        public void RemoveAll() => change.RemoveAll();

        protected void Log()
        {
            if (!logEvent) return;
            Debug.Log($"invoked: {name} ({this.GetType()})");
        }
    }

    public class DKEventBase<T> : ScriptableObject
    {
        Relay<T> change;
        
        [InfoBox("@\"Active Listeners: \" + change.listenerCount", InfoMessageType.None)]
        [FoldoutGroup("Debug")]
        [PropertyOrder(100)]
        [LabelWidth(100f)]
        [SerializeField]
        bool logEvent;

        [SerializeField] public T defaultValue;
        T value;

        [ShowInInspector]
        public T Value
        {
            get => value;
            set
            {
                this.value = value;
                change.Dispatch(value);
#if UNITY_EDITOR
                Log($", whith value: {value}");
#endif
            }
        }

        void OnEnable()
        {
            if (change == null)
                change = new Relay<T>();
            Init();
        }

        protected virtual void Init() => value = defaultValue;

        public virtual void Clear()
        {
            value = default(T);
        }

        [FoldoutGroup("Debug")]
        [PropertyOrder(100)]
        [Button]
        public virtual void Invoke() => Value = value;
        
        public virtual void Invoke(T value) => Value = value;

        public void AddOnce(Action<T> action, bool allowDuplicates = false) => change.AddOnce(action, allowDuplicates);
        public void RemoveOnce(Action<T> action) => change.RemoveOnce(action);

        public void AddListener(Action<T> action, bool allowDuplicates = false) =>
            change.AddListener(action, allowDuplicates);

        public void RemoveListener(Action<T> action) => change.RemoveListener(action);

        [FoldoutGroup("Debug")]
        [PropertyOrder(110)]
        [Button("Remove All Listeners")]
        public void RemoveAll() => change.RemoveAll();

        protected void Log(string addionalInfo = null)
        {
            if (!logEvent) return;
            Debug.Log($"invoked: {name} ({this.GetType()}){addionalInfo}");
        }
    }}
