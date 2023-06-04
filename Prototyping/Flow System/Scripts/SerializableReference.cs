using DKP.ObserverSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKP.SaveSystem.Data
{
    /// <summary>
    /// Represents a reference
    /// </summary>
    [Serializable]
    public class SerializableReference<T> : ISerializableData
    {
        [SerializeReference, ReadOnly]
        public T Value;

        public SerializableReference()
        {
            Value = default(T);
        }
        public SerializableReference(T val)
        {
            Value = val;
        }
    }
}