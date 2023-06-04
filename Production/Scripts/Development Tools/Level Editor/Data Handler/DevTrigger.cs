#if DEVELOPMENT_BUILD || UNITY_EDITOR
using DKP.Input;
using DKP.ObserverSystem;
using DKP.ObserverSystem.Serialization;
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
    [Obsolete("Deprecated as of 2.0")]
    public class DevTrigger : DevBaseMovable
    {
        //protected override void OnDestroy()
        //{
        //    base.OnDestroy();
        //    DevEditorDataContainer.GameBookData.Triggers.Remove(SerializableTrigger);
        //}

        //public override ISerializableData Data => SerializableTrigger;
        //public SerializableTrigger SerializableTrigger;

        //[Header("Deserialize")]
        //[SerializeField]
        //DevLinker _outPoint;
        //[SerializeField]
        //TMP_InputField _label;

        //protected override void PositionChangeObserver(Vector3 obj)
        //{
        //    SerializableTrigger.WorldPosition = new SerializableVector3(obj.x, obj.y, obj.z);
        //}

        //public void OnChangeLabel(string text)
        //{
        //    SerializableTrigger.Label = text;
        //    ChangeEvent.Invoke(SerializableTrigger);
        //}

        //public override void SetData(ISerializableData data)
        //{
        //    SerializableTrigger trigger = (SerializableTrigger)data;
        //    SerializableTrigger = trigger;
        //    _outPoint.SetData(trigger);
        //    ChildData.Add(_outPoint);
        //    _label.text = trigger.Label;
        //    transform.position = new Vector3(trigger.WorldPosition.X, trigger.WorldPosition.Y, trigger.WorldPosition.Z);
        //}
        public override ISerializableData Data => throw new NotImplementedException();

        public override void SetData(ISerializableData data)
        {
            throw new NotImplementedException();
        }
    }
}
#endif