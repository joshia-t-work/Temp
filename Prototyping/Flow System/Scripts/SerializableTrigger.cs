using DKP.CutsceneSystem;
using DKP.ObserverSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

namespace DKP.SaveSystem.Data
{
    [Serializable, Obsolete("Deprecated as of 2.0")]
    public class SerializableTrigger : BaseSerializableAction, ISerializableData, ISerializableWorldObject
    {
        public SerializableVector3 WorldPosition { get => worldPosition; set { worldPosition = value; } }

        [Obsolete("Use WorldPosition instead, deprecated as of 1.5"), SerializeField, ReadOnly]
        public SerializableVector3 WorldPos = new SerializableVector3();

        [SerializeField, ReadOnly, FormerlySerializedAs("WorldPos")]
        private SerializableVector3 worldPosition = new SerializableVector3();

        [SerializeField, ReadOnly]
        public string Label = "";

        public SerializableTrigger()
        {
            NextAction = new List<BaseSerializableAction>
            {
                null
            };
        }

        public override async Task<BaseSerializableAction> Execute(SerializableGameBook gameBook, CutsceneEvents events)
        {
            return await NextAction[0].Execute(gameBook, events);
        }
    }
}