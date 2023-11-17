using System;
using System.Collections;
using System.Collections.Generic;
using Sigtrap.Relays;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DKEvents
{
    public class DKEventBase<T> : ScriptableObject
    {
        Relay<T> change;

        [SerializeField] public T defaultValue;
        T value;
        [ShowInInspector]
        public T Value
        {
            get => value;
            set
            {
                this.value = value;
                OnChange();
                change.Invoke(value);
                Log($", whith value: {value}");
            }
        }

        public static implicit operator T(DKEventBase<T> DKEvent) => DKEvent.Value;

        [FoldoutGroup("Debug")]
        [PropertyOrder(100)][SerializeField][LabelWidth(100f)] bool logEvent;

        void OnEnable()
        {
            if (change == null)
                change = new Relay<T>();
            Init();
        }

        protected virtual void Init() => value = defaultValue;

        [FoldoutGroup("Debug")]
        [PropertyOrder(100)]
        [Button]
        public virtual void Invoke() => Value = value;

        protected virtual void OnChange() { }

        public void AddOnce(Action<T> action, bool allowDuplicates = false) => change.AddOnce(action, allowDuplicates);
        public void RemoveOnce(Action<T> action) => change.RemoveOnce(action);
        public void AddListener(Action<T> action, bool allowDuplicates = false) => change.AddListener(action, allowDuplicates);
        public void RemoveListener(Action<T> action) => change.RemoveListener(action);

        [FoldoutGroup("Debug")]
        [PropertyOrder(101)]
        [Button("Remove All Listeners")]
        public void RemoveAll() => change.RemoveAll();

        protected void Log(string addionalInfo = null)
        {
            if (!logEvent) return;
            Debug.Log($"invoked: {name} ({this.GetType()}){addionalInfo}");
        }
    }
}
