using DKP.ObserverSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace DKP.SaveSystem.Data
{
    /// <summary>
    /// Represents a Data containing a List of ImageTexts
    /// </summary>
    [Serializable]
    public class SerializableImageContainer : BaseSerializableWorldObject, ISerializableData
    {
        public static string BACKGROUND_CONTAINER_NAME = "BACKGROUND";
        /// <summary>
        /// The images of contained
        /// </summary>
        [SerializeField, ReadOnly]
        public List<SerializableImageText> Images = new List<SerializableImageText>();

        [Obsolete("Use WorldPosition instead, deprecated as of 1.5"), SerializeField, ReadOnly]
        public SerializableVector3 WorldPos = new SerializableVector3();

        /// <summary>
        /// Get Image with this name
        /// </summary>
        /// <param name="value">Image name</param>
        /// <returns>ImageText</returns>
        /// <remarks>Can be null</remarks>
        public SerializableImageText this[string value]
        {
            get
            {
                for (int i = 0; i < Images.Count; i++)
                {
                    if (Images[i].Name == value)
                    {
                        return Images[i];
                    }
                }
                return null;
            }
        }

        public SerializableImageText this[int index]
        {
            get
            {
                return Images[index];
            }
            set
            {
                Images[index] = value;
            }
        }

        /// <summary>
        /// Deserializes all sprites in the Images list
        /// </summary>
        /// <remarks>Must be run in Unity thread</remarks>
        public void Deserialize()
        {
            for (int i = 0; i < Images.Count; i++)
            {
                Images[i].Deserialize();
            }
        }
    }
}