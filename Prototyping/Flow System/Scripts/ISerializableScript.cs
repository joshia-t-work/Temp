using DKP.ObserverSystem;
using DKP.ObserverSystem.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKP.SaveSystem.Data
{
    /// <summary>
    /// Represents a set of actions that can be executed
    /// </summary>
    public interface ISerializableScript
    {
        public List<BaseSerializableAction> Actions { get; }
    }
}