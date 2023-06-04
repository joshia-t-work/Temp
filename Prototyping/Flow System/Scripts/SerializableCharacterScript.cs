using DKP.ObserverSystem;
using DKP.ObserverSystem.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKP.SaveSystem.Data
{
    /// <summary>
    /// Represents a set of actions executed by a Character
    /// </summary>
    [Serializable, Obsolete("Deprecated as of 2.0")]
    public class SerializableCharacterScript : BaseSerializableScript<SerializableCharacterScriptAction>
    {
        /// <summary>
        /// The Character data parent of this script, will be the executor of the actions
        /// </summary>
        [Obsolete, SerializeReference, HideInInspector]
        public SerializableCharacter CharacterParent;

        [SerializeReference, HideInInspector]
        public string Character = "";

        [Obsolete("Use Character instead, deprecated as of 1.4"), SerializeField, ReadOnly]
        public int CharacterIndex = 0;
    }
}