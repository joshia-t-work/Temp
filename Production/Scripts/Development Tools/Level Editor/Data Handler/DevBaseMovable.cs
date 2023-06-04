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
    public abstract class DevBaseMovable : DevBaseDataContainer
    {
        Observable<Vector3> position = new Observable<Vector3>(Vector3.zero);
        ISerializableWorldObject WorldObjectData = null;

        protected override void Awake()
        {
            base.Awake();
            if (Data is ISerializableWorldObject worldObject)
            {
                WorldObjectData = worldObject;
            }
            position.AddObserver(PositionChangeObserver);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            position.RemoveObserver(PositionChangeObserver);
        }

        protected void PositionChangeObserver(Vector3 obj)
        {
            if (WorldObjectData != null)
            {
                WorldObjectData.WorldPosition = new SerializableVector3(obj.x, obj.y, obj.z);
            }
        }

        private void Update()
        {
            position.Value = transform.position;
        }

        public override void SetData(ISerializableData data)
        {
            if (data is ISerializableWorldObject worldObject)
            {
                WorldObjectData = worldObject;
                transform.position = new Vector3(worldObject.WorldPosition.X, worldObject.WorldPosition.Y, worldObject.WorldPosition.Z);
            }
        }
    }
}
#endif