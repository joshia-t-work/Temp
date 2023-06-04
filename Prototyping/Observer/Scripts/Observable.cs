using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKP.ObserverSystem
{
    /// <summary>
    /// Generic Observer class that calls attached methods when the value changes.
    /// </summary>
    /// <typeparam name="T">Data value held by observer</typeparam>
    [Serializable]
    public abstract class GenericObservable<T>
    {
        [SerializeField, ReadOnly]
        protected int observerCount;
        public int ObserverCount { get { return observerCount; } }

        public abstract T Value
        {
            get;
            set;
        }
        protected GameEvent<T> OnValueChanged
        {
            get
            {
                if (onValueChanged == null)
                {
                    onValueChanged = new GameEvent<T>();
                }
                return onValueChanged;
            }
        }
        [NonSerialized]
        protected GameEvent<T> onValueChanged = new GameEvent<T>();

        /// <summary>
        /// Adds an observer, will not be called until value changes
        /// </summary>
        /// <param name="observer">Method called on value change</param>
        public void AddObserver(Action<T> observer)
        {
            OnValueChanged.AddListener(observer);
            observerCount = OnValueChanged.ListenerCount;
        }

        /// <summary>
        /// Adds an observer, will be called directly once
        /// </summary>
        /// <param name="observer">Method called on value change</param>
        public void AddObserverAndCall(Action<T> observer)
        {
            OnValueChanged.AddListener(observer);
            observerCount = OnValueChanged.ListenerCount;
            observer.Invoke(Value);
        }
        /// <summary>
        /// Removes an observer, throws an error if observer does not exist
        /// </summary>
        /// <param name="observer">Reference to the action added previously</param>
        public void RemoveObserver(Action<T> observer)
        {
            OnValueChanged.RemoveListener(observer);
            observerCount = OnValueChanged.ListenerCount;
        }
        /// <summary>
        /// Clears all observers
        /// </summary>
        public void RemoveObservers()
        {
            OnValueChanged.RemoveListeners();
            observerCount = OnValueChanged.ListenerCount;
        }
        /// <summary>
        /// Manually runs all the methods attached with the current data value
        /// </summary>
        public void CallObservers()
        {
            OnValueChanged.Invoke(Value);
        }
    }
    /// <summary>
    /// Generic Observer class that calls attached methods when the value changes.
    /// </summary>
    /// <typeparam name="T">Data value held by observer</typeparam>
    [Serializable]
    public class Observable<T> : GenericObservable<T>
        where T : IEquatable<T>
    {
        [SerializeField, ReadOnly]
        protected T value;
        public override T Value
        {
            get { return value; }
            set
            {
                if (!this.value.Equals(value))
                {
                    this.value = value;
                    OnValueChanged.Invoke(Value);
                }
            }
        }

        /// <summary>
        /// Creates a new Observable data
        /// </summary>
        /// <param name="value">Initial value</param>
        public Observable(T value)
        {
            observerCount = 0;
            this.value = value;
        }
        /// <summary>
        /// Creates a new Observable data
        /// </summary>
        public Observable()
        {
            observerCount = 0;
            value = default(T);
        }
    }
    /// <summary>
    /// Generic Observer class that calls attached methods when the value changes.
    /// </summary>
    /// <typeparam name="T">Data value held by observer</typeparam>
    /// <remarks>Does not implement IEquatable, generates garbage</remarks>
    [Serializable]
    public class UnsafeObservable<T> : GenericObservable<T>
    {
        [SerializeField, ReadOnly]
        protected T value;
        public override T Value
        {
            get { return value; }
            set
            {
                if (!this.value.Equals(value))
                {
                    this.value = value;
                    OnValueChanged.Invoke(Value);
                }
            }
        }

        /// <summary>
        /// Creates a new Observable data
        /// </summary>
        /// <param name="value">Initial value</param>
        public UnsafeObservable(T value)
        {
            observerCount = 0;
            this.value = value;
        }
        /// <summary>
        /// Creates a new Observable data
        /// </summary>
        public UnsafeObservable()
        {
            observerCount = 0;
            value = default(T);
        }
    }
}
namespace DKP.ObserverSystem.Serialization {
    /// <summary>
    /// Observer class that calls attached methods when the value changes. Serializes by Reference instead of value.
    /// </summary>
    /// <typeparam name="T">Data value held by observer</typeparam>
    [Serializable]
    public class ReferenceObservable<T> : GenericObservable<T>
        where T : IEquatable<T>
    {
        [SerializeReference, ReadOnly]
        protected T value;
        public override T Value
        {
            get { return value; }
            set
            {
                if (!Equals(this.value, value))
                {
                    this.value = value;
                    OnValueChanged.Invoke(Value);
                }
            }
        }

        /// <summary>
        /// Creates a new Observable data
        /// </summary>
        /// <param name="value">Initial value</param>
        public ReferenceObservable(T value)
        {
            observerCount = 0;
            this.value = value;
        }
        /// <summary>
        /// Creates a new Observable data
        /// </summary>
        public ReferenceObservable()
        {
            observerCount = 0;
            value = default(T);
        }
    }
}
