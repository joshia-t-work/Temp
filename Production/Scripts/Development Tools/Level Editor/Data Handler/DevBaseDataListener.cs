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
    public abstract class DevBaseDataListener : MonoBehaviour
    {
        protected GameEvent<object> ChangeEvent => DevEditorDataContainer.ChangeEvent;

        protected virtual void Awake()
        {
            DevEditorDataContainer.ChangeEvent.AddListener(changeEventListener);
        }
        protected virtual void OnDestroy()
        {
            DevEditorDataContainer.ChangeEvent.RemoveListener(changeEventListener);
        }

        protected virtual void changeEventListener(object data) { }
    }
}
#endif