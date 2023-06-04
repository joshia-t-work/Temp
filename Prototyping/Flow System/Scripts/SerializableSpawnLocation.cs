using DKP.ObserverSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace DKP.SaveSystem.Data
{
    /// <summary>
    /// Represents a Spawner Data containing world positions and spawn queue
    /// </summary>
    [Serializable]
    public class SerializableSpawnLocation : ISerializableData, ISerializableWorldObject
    {
        public SerializableVector3 WorldPosition { get => worldPos; set { worldPos = value; } }

        [SerializeField, ReadOnly, FormerlySerializedAs("WorldPos")]
        SerializableVector3 worldPos = new SerializableVector3();

        [SerializeField, ReadOnly]
        public string Label = "";
        [SerializeField, ReadOnly]
        public SpawnType SpawnType = SpawnType.Once;
    }

    public enum SpawnType
    {
        Once,
        Repeat
    }
}