using DKP.ObserverSystem;
using DKP.ObserverSystem.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKP.SaveSystem.Data
{
    /// <summary>
    /// Represents a set of actions executed by the script
    /// </summary>
    [Obsolete("Deprecated as of 2.0")]
    [Serializable]
    public class SerializableScript : BaseSerializableScript<SerializableScriptAction>
    {
    }
}