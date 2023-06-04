#if DEVELOPMENT_BUILD || UNITY_EDITOR
using DKP.Development.LevelEditor.Data;
using DKP.Input;
using DKP.ObserverSystem;
using DKP.ObserverSystem.Serialization;
using DKP.SaveSystem.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace DKP.Development.LevelEditor.Data
{
    /// <summary>
    /// Deprecated as of 2.0
    /// </summary>
    [Obsolete]
    public class DevMainThreadAction : DevBaseScriptAction<SerializableMainThread, SerializableMainThreadAction>
    {
        //[Header("Deserialize")]
        //[SerializeField]
        //TMP_Dropdown _actionType;
        //[SerializeField]
        //TMP_Dropdown _triggers;

        //protected override void changeEventListener(object data)
        //{
        //    if (data == DevEditorDataContainer.GameBookData.Triggers)
        //    {
        //        TriggersListChangeObserver(DevEditorDataContainer.GameBookData.Triggers);
        //    }
        //    for (int i = 0; i < DevEditorDataContainer.GameBookData.Triggers.Count; i++)
        //    {
        //        if (data == DevEditorDataContainer.GameBookData.Triggers[i])
        //        {
        //            _triggers.options[i].text = DevEditorDataContainer.GameBookData.Triggers[i].Label;
        //        }
        //    }
        //}
        //public void OnActionChange(int val)
        //{
        //    SerializableScriptAction.ThreadActionType = (ThreadActionTypes)val;
        //}
        //public void OnTriggerChange(int val)
        //{
        //    SerializableScriptAction.ExecutedTriggerIndex = val;
        //}

        //private void TriggersListChangeObserver(List<SerializableTrigger> triggers)
        //{
        //    while (triggers.Count != _triggers.options.Count)
        //    {
        //        if (triggers.Count > _triggers.options.Count)
        //        {
        //            _triggers.options.Add(new TMP_Dropdown.OptionData(triggers[_triggers.options.Count].Label));
        //        }
        //        else if (triggers.Count < _triggers.options.Count)
        //        {
        //            _triggers.options.RemoveAt(_triggers.options.Count - 1);
        //        }
        //    }
        //}

        //public override void SetData(ISerializableData data)
        //{
        //    base.SetData(data);
        //    _actionType.value = (int)SerializableScriptAction.ThreadActionType;
        //    TriggersListChangeObserver(DevEditorDataContainer.GameBookData.Triggers);
        //    _triggers.value = SerializableScriptAction.ExecutedTriggerIndex;
        //}

        //public override void Link()
        //{
        //    base.Link();
        //}
    }
}
#endif