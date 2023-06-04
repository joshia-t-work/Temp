using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

// A GlobalGameEvent that can hold data according to its previous invoke
namespace DKP.ObserverSystem
{
    #region GameEvent

    /// <inheritdoc cref="GlobalGameEvent"/>
    public class CachedGlobalGameEvent : GlobalGameEvent
    {
    }
    /// <inheritdoc cref="GlobalGameEvent{T}"/>
    public class CachedGlobalGameEvent<T> : GlobalGameEvent<T>
        where T : IEquatable<T>
    {
        public Observable<T> Value1 { get; private set; } = new Observable<T>();
        /// <inheritdoc cref="GlobalGameEvent{T}.Invoke(T)"/>
        /// <remarks>Also saves it</remarks>
        public override void Invoke(T arg1)
        {
            Value1.Value = arg1;
            base.Invoke(arg1);
        }
    }
    /// <inheritdoc cref="GlobalGameEvent{T1, T2}"/>
    public class CachedGlobalGameEvent<T1, T2> : GlobalGameEvent<T1, T2>
        where T1 : IEquatable<T1>
        where T2 : IEquatable<T2>
    {
        public Observable<T1> Value1 { get; private set; } = new Observable<T1>();
        public Observable<T2> Value2 { get; private set; } = new Observable<T2>();
        /// <inheritdoc cref="GlobalGameEvent{T1, T2}.Invoke(T1, T2)"/>
        /// <remarks>Also saves it</remarks>
        public override void Invoke(T1 arg1, T2 arg2)
        {
            Value1.Value = arg1;
            Value2.Value = arg2;
            base.Invoke(arg1, arg2);
        }
    }
    /// <inheritdoc cref="GlobalGameEvent{T1, T2, T3}"/>
    public class CachedGlobalGameEvent<T1, T2, T3> : GlobalGameEvent<T1, T2, T3>
        where T1 : IEquatable<T1>
        where T2 : IEquatable<T2>
        where T3 : IEquatable<T3>
    {
        public Observable<T1> Value1 { get; private set; } = new Observable<T1>();
        public Observable<T2> Value2 { get; private set; } = new Observable<T2>();
        public Observable<T3> Value3 { get; private set; } = new Observable<T3>();
        /// <inheritdoc cref="GlobalGameEvent{T1, T2, T3}.Invoke(T1, T2, T3)"/>
        /// <remarks>Also saves it</remarks>
        public override void Invoke(T1 arg1, T2 arg2, T3 arg3)
        {
            Value1.Value = arg1;
            Value2.Value = arg2;
            Value3.Value = arg3;
            base.Invoke(arg1, arg2, arg3);
        }
    }
    /// <inheritdoc cref="GlobalGameEvent{T1, T2, T3, T4}"/>
    public class CachedGlobalGameEvent<T1, T2, T3, T4> : GlobalGameEvent<T1, T2, T3, T4>
        where T1 : IEquatable<T1>
        where T2 : IEquatable<T2>
        where T3 : IEquatable<T3>
        where T4 : IEquatable<T4>
    {
        public Observable<T1> Value1 { get; private set; } = new Observable<T1>();
        public Observable<T2> Value2 { get; private set; } = new Observable<T2>();
        public Observable<T3> Value3 { get; private set; } = new Observable<T3>();
        public Observable<T4> Value4 { get; private set; } = new Observable<T4>();
        /// <inheritdoc cref="GlobalGameEvent{T1, T2, T3, T4}.Invoke(T1, T2, T3, T4)"/>
        /// <remarks>Also saves it</remarks>
        public override void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            Value1.Value = arg1;
            Value2.Value = arg2;
            Value3.Value = arg3;
            Value4.Value = arg4;
            base.Invoke(arg1, arg2, arg3, arg4);
        }
    }

    #endregion
}
