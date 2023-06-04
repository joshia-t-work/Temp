using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

// GameEvent are Event that could be listened and invoked, this one particularly used on Scripting
namespace DKP.ObserverSystem
{
    #region BaseGameEvent

    /// <summary>
    /// A Base GameEvent that is able to hold a list of listeners of any type, used for scripting.
    /// </summary>
    /// <typeparam name="T">Parameter of the event</typeparam>
    public abstract class BaseGameEvent<T>
    {
        protected List<T> listeners = new List<T>();
        public int ListenerCount => listeners.Count;
        /// <summary>
        /// Adds a listener to the event
        /// </summary>
        /// <param name="listener">Method to be attached</param>
        public void AddListener(T listener)
        {
            listeners.Add(listener);
        }
        /// <summary>
        /// Removes a previously attached method, throws error if method does not exist
        /// </summary>
        /// <param name="listener">Method reference to be removed</param>
        public void RemoveListener(T listener)
        {
            listeners.Remove(listener);
        }
        /// <summary>
        /// Clears all attached method
        /// </summary>
        public void RemoveListeners()
        {
            listeners.Clear();
        }
    }

    #endregion
    #region GameEvent

    /// <summary>
    /// An GameEvent that holds no parameter and can be invoked.
    /// </summary>
    public class GameEvent : BaseGameEvent<System.Action>
    {
        /// <summary>
        /// Calls all the method on this event
        /// </summary>
        public void Invoke()
        {
            for (int i = 0; i < listeners.Count; i++)
            {
                listeners[i].Invoke();
            }
        }
    }
    /// <summary>
    /// An GameEvent that holds 1 parameter and can be invoked.
    /// </summary>
    /// <typeparam name="T">Parameter of the event</typeparam>
    public class GameEvent<T> : BaseGameEvent<System.Action<T>>
    {
        /// <summary>
        /// Calls all the method on this event with the parameter value
        /// </summary>
        public void Invoke(T arg1)
        {
            for (int i = 0; i < listeners.Count; i++)
            {
                listeners[i].Invoke(arg1);
            }
        }
    }
    /// <summary>
    /// An GameEvent that holds 2 parameter and can be invoked.
    /// </summary>
    /// <typeparam name="T1">Parameter 1</typeparam>
    /// <typeparam name="T2">Parameter 2</typeparam>
    public class GameEvent<T1, T2> : BaseGameEvent<System.Action<T1, T2>>
    {
        /// <summary>
        /// Calls all the method on this event with the parameter value
        /// </summary>
        public void Invoke(T1 arg1, T2 arg2)
        {
            for (int i = 0; i < listeners.Count; i++)
            {
                listeners[i].Invoke(arg1, arg2);
            }
        }
    }
    /// <summary>
    /// An GameEvent that holds 3 parameter and can be invoked.
    /// </summary>
    /// <typeparam name="T1">Parameter 1</typeparam>
    /// <typeparam name="T2">Parameter 2</typeparam>
    /// <typeparam name="T3">Parameter 3</typeparam>
    public class GameEvent<T1, T2, T3> : BaseGameEvent<System.Action<T1, T2, T3>>
    {
        /// <summary>
        /// Calls all the method on this event with the parameter value
        /// </summary>
        public void Invoke(T1 arg1, T2 arg2, T3 arg3)
        {
            for (int i = 0; i < listeners.Count; i++)
            {
                listeners[i].Invoke(arg1, arg2, arg3);
            }
        }
    }
    /// <summary>
    /// An GameEvent that holds 4 parameter and can be invoked.
    /// </summary>
    /// <typeparam name="T1">Parameter 1</typeparam>
    /// <typeparam name="T2">Parameter 2</typeparam>
    /// <typeparam name="T3">Parameter 3</typeparam>
    /// <typeparam name="T4">Parameter 4</typeparam>
    public class GameEvent<T1, T2, T3, T4> : BaseGameEvent<System.Action<T1, T2, T3, T4>>
    {
        /// <summary>
        /// Calls all the method on this event with the parameter value
        /// </summary>
        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            for (int i = 0; i < listeners.Count; i++)
            {
                listeners[i].Invoke(arg1, arg2, arg3, arg4);
            }
        }
    }

    #endregion
}
