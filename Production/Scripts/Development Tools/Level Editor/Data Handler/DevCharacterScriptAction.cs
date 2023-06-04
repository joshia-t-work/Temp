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
    [Obsolete("Deprecated as of 2.0")]
    public class DevCharacterScriptAction : DevBaseScriptAction<SerializableCharacterScript, SerializableCharacterScriptAction>
    {
        //[Header("Deserialize")]
        //[SerializeField]
        //TMP_Dropdown _actionType;
        //[SerializeField]
        //TMP_InputField _position;
        //[SerializeField]
        //TMP_InputField _expression;
        //[SerializeField]
        //TMP_InputField _dialogue;

        ////protected override void changeEventListener(object data)
        ////{
        ////    if (SerializableScriptAction.Parent != null)
        ////    {
        ////        if (SerializableScriptAction.Parent.CharacterParent != null)
        ////        {
        ////            if (data == SerializableScriptAction.Parent.CharacterParent.Images)
        ////            {
        ////                ExpressionListChangeObserver(SerializableScriptAction.Parent.CharacterParent.Images);
        ////            }
        ////            for (int i = 0; i < SerializableScriptAction.Parent.CharacterParent.Images.Count; i++)
        ////            {
        ////                if (data == SerializableScriptAction.Parent.CharacterParent.Images[i])
        ////                {
        ////                    _expression.options[i].text = SerializableScriptAction.Parent.CharacterParent.Images[i].Label;
        ////                }
        ////            }
        ////        }
        ////    }
        ////}
        //public void OnActionChange(int val)
        //{
        //    SerializableScriptAction.CharacterActionType = (CharacterActionTypes)val;
        //}
        //public void OnExpressionChange(string val)
        //{
        //    SerializableScriptAction.CharacterExpression = val;
        //}
        //public void OnDialogueChange(string val)
        //{
        //    SerializableScriptAction.CharacterDialogue = val;
        //}
        //public void OnPositionChange(string val)
        //{
        //    try
        //    {
        //        SerializableScriptAction.Position = float.Parse(val);
        //    }
        //    catch (Exception)
        //    {
        //        SerializableScriptAction.Position = 0f;
        //    }
        //    _position.text = SerializableScriptAction.Position.ToString();
        //}

        ////private void ExpressionListChangeObserver(List<SerializableImageText> characterExpressions)
        ////{
        ////    while (characterExpressions.Count != _expression.options.Count)
        ////    {
        ////        if (characterExpressions.Count > _expression.options.Count)
        ////        {
        ////            _expression.options.Add(new TMP_Dropdown.OptionData(characterExpressions[_expression.options.Count].Label));
        ////        }
        ////        else if (characterExpressions.Count < _expression.options.Count)
        ////        {
        ////            _expression.options.RemoveAt(_expression.options.Count - 1);
        ////        }
        ////    }
        ////}

        //public override void SetData(ISerializableData data)
        //{
        //    base.SetData(data);
        //    _actionType.value = (int)SerializableScriptAction.CharacterActionType;
        //    //if (SerializableScriptAction.Parent?.CharacterParent?.Images != null)
        //    //{
        //    //    ExpressionListChangeObserver(SerializableScriptAction.Parent.CharacterParent.Images);
        //    //}
        //    _expression.text = SerializableScriptAction.CharacterExpression;
        //    _dialogue.text = SerializableScriptAction.CharacterDialogue;
        //    _position.text = SerializableScriptAction.Position.ToString();
        //}

        //public override void Link()
        //{
        //    base.Link();
        //}
    }
}
#endif