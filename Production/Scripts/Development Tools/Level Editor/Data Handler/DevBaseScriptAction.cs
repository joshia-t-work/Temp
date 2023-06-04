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
using UnityEngine.UI;

namespace DKP.Development.LevelEditor.Data
{
    [Obsolete("Deprecated as of 2.0")]
    public abstract class DevBaseScriptAction<TScriptData, TActionData> : DevBaseMovable
        where TScriptData : BaseSerializableScript<TActionData>
        where TActionData : BaseSerializableAction<TScriptData>, new()
    {
        public override ISerializableData Data => SerializableScriptAction;
        [Header("Deserialize")]
        [SerializeField]
        DevLinker[] _links;

        public TActionData SerializableScriptAction;

        protected override void OnDestroy()
        {
            base.OnDestroy();
            SerializableScriptAction?.Parent?.Actions.Remove(SerializableScriptAction);
        }

        public void OnFlip()
        {
            SerializableScriptAction.Flipped = (SerializableScriptAction.Flipped + 1) % 5;
            for (int i = 0; i < _links.Length; i++)
            {
                _links[i].SetFlip(SerializableScriptAction.Flipped);
            }
        }

        public void OnMoveUp()
        {
            int index = SerializableScriptAction.Parent.Actions.IndexOf(SerializableScriptAction);
            if (index > 0)
            {
                transform.SetSiblingIndex(transform.GetSiblingIndex() - 1);
                TActionData temp = SerializableScriptAction.Parent.Actions[index];
                SerializableScriptAction.Parent.Actions[index] = SerializableScriptAction.Parent.Actions[index - 1];
                SerializableScriptAction.Parent.Actions[index - 1] = temp;
            }
        }

        public void OnMoveDown()
        {
            int index = SerializableScriptAction.Parent.Actions.IndexOf(SerializableScriptAction);
            if (index != -1)
            {
                if (index < SerializableScriptAction.Parent.Actions.Count - 1)
                {
                    transform.SetSiblingIndex(transform.GetSiblingIndex() + 1);
                    TActionData temp = SerializableScriptAction.Parent.Actions[index];
                    SerializableScriptAction.Parent.Actions[index] = SerializableScriptAction.Parent.Actions[index + 1];
                    SerializableScriptAction.Parent.Actions[index + 1] = temp;
                }
            }
        }

        public override void SetData(ISerializableData data)
        {
            TActionData scriptAction = (TActionData)data;
            SerializableScriptAction = scriptAction;
            for (int i = 0; i < _links.Length; i++)
            {
                _links[i].SetData(scriptAction);
                ChildData.Add(_links[i]);
            }
        }
    }
}
#endif