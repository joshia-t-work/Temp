using DKP.ObserverSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKP.SaveSystem.Data
{
    [Serializable, Obsolete("Use SerializableImageContainer instead, deprecated as of 2.0")]
    public class SerializableCharacter : SerializableImageContainer
    {
        [Obsolete("Use ObjectName instead, deprecated as of 2.0")]
        public string Name;
    }
}