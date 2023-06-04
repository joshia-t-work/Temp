using DKP.ObserverSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKP.SaveSystem.Data
{
    /// <summary>
    /// Represents an image that is labeled
    /// </summary>
    [Serializable]
    public class SerializableImageText : ISerializableData
    {
        /// <summary>
        /// The label of the image
        /// </summary>
        [SerializeField, ReadOnly]
        private string ImageLabel = "";

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
        
        public string Name { get { return ImageLabel; } }
        /// <summary>
        /// Called when the name changes
        /// </summary>
        [field: NonSerialized]
        public event Action<string, string> OnNameChanged;
        public void SetName(string name)
        {
            if (ImageLabel != name)
            {
                string oldName = ImageLabel;
                ImageLabel = name;
                OnNameChanged?.Invoke(oldName, name);
            }
        }

        /// <summary>
        /// The image
        /// </summary>
        [SerializeField, ReadOnly]
        public SerializableImage Image = new SerializableImage();
        
        [Obsolete("Deprecated since 2.0, use Name instead"), HideInInspector]
        public string Label = "";
        
        /// <summary>
        /// Deserializes image
        /// </summary>
        /// <remarks>Must be run in Unity thread</remarks>
        public void Deserialize()
        {
            Image.Deserialize();
        }
    }
}