using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKP.SaveSystem.Data
{
    /// <summary>
    /// Serialized Vector3 Position and float Zoom
    /// </summary>
    [Serializable]
    public class SerializableCameraPosition : ISerializableData
    {
        public const float DEFAULT_ZOOM = 400f;

        [SerializeField, ReadOnly]
        public SerializableVector3 Position;

        [SerializeField, ReadOnly]
        public float Zoom = DEFAULT_ZOOM;

        public SerializableCameraPosition(float x, float y, float z)
        {
            Position = new SerializableVector3(x, y, z);
        }
    }
}