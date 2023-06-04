using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

// GameEvent are Event that could be listened and invoked, this one particularly used on ScriptableObjects
namespace DKP.ObserverSystem
{
    #region BaseGameEvent

    /// <inheritdoc cref="BaseGameEvent{T}"/>
    public abstract class BaseGlobalGameEvent<T1, T2> : ScriptableObject
    {
        protected List<T1> listeners = new List<T1>();
        protected List<T2> tasks = new List<T2>();

        /// <summary>
        /// Adds a task to the event
        /// </summary>
        /// <param name="task">Awaitable task to be attached</param>
        public void AddTask(T2 task)
        {
            tasks.Add(task);
        }
        /// <summary>
        /// Removes a previously attached task, throws error if task does not exist
        /// </summary>
        /// <param name="task">Awaitable task to be attached</param>
        public void RemoveTask(T2 task)
        {
            tasks.Remove(task);
        }
        /// <summary>
        /// Clears all attached tasks
        /// </summary>
        public void ClearTasks()
        {
            tasks.Clear();
        }
        /// <inheritdoc cref="BaseGameEvent{T}.AddListener(T)"/>
        public void AddListener(T1 listener)
        {
            listeners.Add(listener);
        }
        /// <inheritdoc cref="BaseGameEvent{T}.RemoveListener(T)"/>
        public void RemoveListener(T1 listener)
        {
            listeners.Remove(listener);
        }
        /// <inheritdoc cref="BaseGameEvent{T}.RemoveListeners()"/>
        public void RemoveListeners()
        {
            listeners.Clear();
        }
    }

    #endregion
    #region GameEvent

    /// <inheritdoc cref="GameEvent"/>
    public class GlobalGameEvent : BaseGlobalGameEvent<System.Action, System.Func<Task>>
    {
        /// <inheritdoc cref="GameEvent.Invoke"/>
        public virtual void Invoke()
        {
            for (int i = 0; i < listeners.Count; i++)
            {
                listeners[i].Invoke();
            }
        }
        /// <summary>
        /// Calls all the method on this event and waits for it to finish
        /// </summary>
        public Task StartTask()
        {
            return Task.WhenAll(tasks.Select(action => action()).ToArray());
        }
    }
    /// <inheritdoc cref="GameEvent{T}"/>
    public class GlobalGameEvent<T> : BaseGlobalGameEvent<System.Action<T>, System.Func<T, Task>>
    {
        /// <inheritdoc cref="GameEvent{T}.Invoke(T)"/>
        public virtual void Invoke(T arg1)
        {
            for (int i = 0; i < listeners.Count; i++)
            {
                listeners[i].Invoke(arg1);
            }
        }
        /// <summary>
        /// Calls all the method on this event and waits for it to finish
        /// </summary>
        public Task StartTask(T arg1)
        {
            return Task.WhenAll(tasks.Select(action => action(arg1)).ToArray());
        }
    }
    /// <inheritdoc cref="GameEvent{T1, T2}"/>
    public class GlobalGameEvent<T1, T2> : BaseGlobalGameEvent<System.Action<T1, T2>, System.Func<T1, T2, Task>>
    {
        /// <inheritdoc cref="GameEvent{T1, T2}.Invoke(T1, T2)"/>
        public virtual void Invoke(T1 arg1, T2 arg2)
        {
            for (int i = 0; i < listeners.Count; i++)
            {
                listeners[i].Invoke(arg1, arg2);
            }
        }
        /// <summary>
        /// Calls all the method on this event and waits for it to finish
        /// </summary>
        public Task StartTask(T1 arg1, T2 arg2)
        {
            return Task.WhenAll(tasks.Select(action => action(arg1, arg2)).ToArray());
        }
    }
    /// <inheritdoc cref="GameEvent{T1, T2, T3}"/>
    public class GlobalGameEvent<T1, T2, T3> : BaseGlobalGameEvent<System.Action<T1, T2, T3>, System.Func<T1, T2, T3, Task>>
    {
        /// <inheritdoc cref="GameEvent{T1, T2, T3}.Invoke(T1, T2, T3)"/>
        public virtual void Invoke(T1 arg1, T2 arg2, T3 arg3)
        {
            for (int i = 0; i < listeners.Count; i++)
            {
                listeners[i].Invoke(arg1, arg2, arg3);
            }
        }
        /// <summary>
        /// Calls all the method on this event and waits for it to finish
        /// </summary>
        public Task StartTask(T1 arg1, T2 arg2, T3 arg3)
        {
            return Task.WhenAll(tasks.Select(action => action(arg1, arg2, arg3)).ToArray());
        }
    }
    /// <inheritdoc cref="GameEvent{T1, T2, T3, T4}"/>
    public class GlobalGameEvent<T1, T2, T3, T4> : BaseGlobalGameEvent<System.Action<T1, T2, T3, T4>, System.Func<T1, T2, T3, T4, Task>>
    {
        /// <inheritdoc cref="GameEvent{T1, T2, T3, T4}.Invoke(T1, T2, T3, T4)"/>
        public virtual void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            for (int i = 0; i < listeners.Count; i++)
            {
                listeners[i].Invoke(arg1, arg2, arg3, arg4);
            }
        }
        /// <summary>
        /// Calls all the method on this event and waits for it to finish
        /// </summary>
        public Task StartTask(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            return Task.WhenAll(tasks.Select(action => action(arg1, arg2, arg3, arg4)).ToArray());
        }
    }

    #endregion
}
