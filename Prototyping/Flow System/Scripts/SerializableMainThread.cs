using DKP.ObserverSystem;
using DKP.ObserverSystem.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKP.SaveSystem.Data
{
    /// <summary>
    /// Represents a script filled of actions executed by the Main Thread
    /// </summary>
    [Serializable, Obsolete("Deprecated as of 2.0")]
    public class SerializableMainThread : BaseSerializableScript<SerializableMainThreadAction>
    {
    }
}