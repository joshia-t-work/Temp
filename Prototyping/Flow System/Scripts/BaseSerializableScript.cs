using DKP.ObserverSystem;
using DKP.ObserverSystem.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace DKP.SaveSystem.Data
{
    /// <summary>
    /// Represents a set of actions executed by the script
    /// </summary>
    [Serializable]
    public abstract class BaseSerializableScript<TScriptAction> : BaseSerializableScript
        where TScriptAction : new()
    {
        [SerializeField, ReadOnly]
        public SerializableReferenceList<TScriptAction> Actions = new SerializableReferenceList<TScriptAction>();
    }

    [Serializable]
    public abstract class BaseSerializableScript : BaseSerializableWorldObject, ISerializableData
    {
    }
}