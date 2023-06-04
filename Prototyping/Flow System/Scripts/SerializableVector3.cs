using DKP.ObserverSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKP.SaveSystem.Data
{
    /// <summary>
    /// Vector3 that can be serialized
    /// </summary>
    [Serializable]
    public struct SerializableVector3 : ISerializableData
    {
        public static SerializableVector3 Zero = new SerializableVector3(0, 0, 0);

        public float X;
        public float Y;
        public float Z;

        public SerializableVector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override bool Equals(object obj)
        {
            if (obj is SerializableVector3)
            {
                SerializableVector3 other = (SerializableVector3)obj;
                return X == other.X && Y == other.Y && Z == other.Z;
            }
            return false;
        }

        // Equality check
        public static bool operator ==(SerializableVector3 a, SerializableVector3 b)
        {
            return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
        }

        // Inequality check
        public static bool operator !=(SerializableVector3 a, SerializableVector3 b)
        {
            return !(a == b);
        }

        // Hashcode
        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
        }
    }
}