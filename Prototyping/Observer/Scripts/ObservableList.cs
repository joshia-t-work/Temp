using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKP.ObserverSystem
{
    /// <summary>
    /// List Observer class that calls attached methods when the list is .
    /// </summary>
    /// <typeparam name="T">Data value held by observer</typeparam>
    [Serializable]
    public class ObservableList<T> : UnsafeObservable<T[]>, ISerializationCallbackReceiver
    {
        private List<T> _list = new List<T>();
        /// <summary>
        /// Creates a new observer that observes the list
        /// </summary>
        /// <param name="value">The list referenced</param>
        public ObservableList(List<T> value) : base(value.ToArray())
        {
            _list = value;
        }
        /// <summary>
        /// Creates a new observer that copies the array
        /// </summary>
        /// <param name="value">Copied array</param>
        public ObservableList(T[] value) : base(value)
        {
            _list = new List<T>(value);
        }
        /// <summary>
        /// Creates a new observer
        /// </summary>
        public ObservableList() : base(new T[0])
        {
        }
        bool isDirty = true;

        public override T[] Value
        {
            get
            {
                if (isDirty)
                {
                    value = _list.ToArray();
                    isDirty = false;
                }
                return value;
            }
            set
            {
                Debug.LogWarning("Use Set() or Link() instead!");
                this.value = value;
                _list = new List<T>(value);
                OnValueChanged.Invoke(Value);
            }
        }

        #region List Operations
        public T this[int index]
        {
            get
            {
                return _list[index];
            }
        }
        public int Count => _list.Count;
        public int IndexOf(T val)
        {
            return _list.IndexOf(val);
        }
        public void Add(T val)
        {
            _list.Add(val);
            isDirty = true;
            OnValueChanged.Invoke(Value);
        }
        public void Remove(T val)
        {
            _list.Remove(val);
            isDirty = true;
            OnValueChanged.Invoke(Value);
        }
        public void Clear()
        {
            _list.Clear();
            isDirty = true;
            OnValueChanged.Invoke(Value);
        }
        public void Set(T[] vals)
        {
            _list = new List<T>(vals);
            isDirty = true;
            OnValueChanged.Invoke(Value);
        }
        public void Set(List<T> vals)
        {
            _list = new List<T>(vals);
            isDirty = true;
            OnValueChanged.Invoke(Value);
        }
        public void Link(List<T> vals)
        {
            _list = vals;
            isDirty = true;
            OnValueChanged.Invoke(Value);
        }
        #endregion

        public void OnBeforeSerialize()
        {
            value = _list.ToArray();
        }

        public void OnAfterDeserialize()
        {
            _list = new List<T>(value);
        }
    }
}
