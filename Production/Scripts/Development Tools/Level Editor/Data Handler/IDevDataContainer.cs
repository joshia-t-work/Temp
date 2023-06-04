#if DEVELOPMENT_BUILD || UNITY_EDITOR
using DKP.Input;
using DKP.ObserverSystem;
using DKP.SaveSystem.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace DKP.Development.LevelEditor.Data
{
    public interface IDevDataContainer
    {
        public ISerializableData Data { get; }
        public void SetData(ISerializableData data);
        public void Link();
        public void Delete();
    }
}
#endif