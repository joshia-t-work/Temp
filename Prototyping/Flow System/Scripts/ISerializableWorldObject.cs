using DKP.ObserverSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKP.SaveSystem.Data
{
    /// <summary>
    /// Represents a Data that has a world position
    /// </summary>
    public interface ISerializableWorldObject
    {
        public SerializableVector3 WorldPosition { get; set; }
    }
}