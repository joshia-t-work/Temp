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
    public abstract class DevBaseDataContainer : DevBaseDataListener, IDevDataContainer
    {
        public abstract ISerializableData Data { get; }

        protected List<IDevDataContainer> ChildData = new List<IDevDataContainer>();

        public virtual void Delete()
        {
            for (int i = 0; i < ChildData.Count; i++)
            {
                ChildData[i].Delete();
            }
            Destroy(gameObject);
        }

        public virtual void Link()
        {
            for (int i = 0; i < ChildData.Count; i++)
            {
                ChildData[i].Link();
            }
        }
        public abstract void SetData(ISerializableData data);
    }
}
#endif