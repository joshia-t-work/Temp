using DKP.ObserverSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace DKP.SaveSystem.Data
{
    /// <summary>
    /// Represents a Data that is able to be dragged
    /// </summary>
    [Serializable]
    public class BaseSerializableWorldObject : ISerializableData, ISerializableWorldObject
    {
        /// <summary>
        /// Name to be referred to, MUST be unique on most cases
        /// </summary>
        public string ObjectName
        {
            get
            {
                return name;
            }
        }
        [SerializeField]
        private string name;

        /// <summary>
        /// Triggers when the name is changed (oldName, newName)
        /// </summary>
        [field: NonSerialized]
        public event Action<string, string> OnNameChanged;
        public void SetObjectName(string name)
        {
            OnNameChanged?.Invoke(this.name, name);
            this.name = name;
        }

        /// <summary>
        /// Triggered when the object is destroyed
        /// </summary>
        [field: NonSerialized]
        public event Action OnDestroy;
        public void Destroy()
        {
            OnDestroy?.Invoke();
            OnNameChanged = null;
            OnDestroy = null;
        }

        /// <summary>
        /// Gets the world position of this script
        /// </summary>
        public SerializableVector3 WorldPosition { get => worldPosition; set { worldPosition = value; } }

        [SerializeField, ReadOnly]
        private SerializableVector3 worldPosition = new SerializableVector3();
    }
}