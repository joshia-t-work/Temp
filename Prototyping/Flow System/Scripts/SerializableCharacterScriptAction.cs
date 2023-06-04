using DKP.CutsceneSystem;
using DKP.ObserverSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace DKP.SaveSystem.Data
{
    /// <summary>
    /// Represents an Action that will be executed by a character in a script list
    /// </summary>
    [Serializable, Obsolete("Deprecated as of 2.0")]
    public class SerializableCharacterScriptAction : BaseSerializableAction<SerializableCharacterScript>
    {
        [SerializeField, ReadOnly]
        public CharacterActionTypes CharacterActionType = CharacterActionTypes.Show;

        /// <summary>
        /// Use CharacterExpression instead, deprecated as of 1.4
        /// </summary>
        [Obsolete, SerializeField, ReadOnly]
        public int CharacterExpressionIndex = 0;

        /// <summary>
        /// The character expression to be used
        /// </summary>
        [SerializeField, ReadOnly]
        public string CharacterExpression = "";

        /// <summary>
        /// The dialogue the character will say
        /// </summary>
        [SerializeField, ReadOnly]
        public string CharacterDialogue;

        /// <summary>
        /// The position the character will move to
        /// </summary>
        [SerializeField, ReadOnly]
        public float Position;

        public SerializableCharacterScriptAction()
        {
            NextAction = new List<BaseSerializableAction>
            {
                null
            };
        }

        public override async Task<BaseSerializableAction> Execute(SerializableGameBook gameBook, CutsceneEvents events)
        {
            //switch (CharacterActionType)
            //{
            //    case CharacterActionTypes.Show:
            //        await CutsceneEvents.I.InvokeShowCharacter(Parent.Character, Position);
            //        break;
            //    case CharacterActionTypes.Hide:
            //        await CutsceneEvents.I.InvokeHideCharacter(Parent.Character);
            //        break;
            //    case CharacterActionTypes.SetExpression:
            //        await CutsceneEvents.I.InvokeSetExpression(Parent.Character, CharacterExpression);
            //        break;
            //    case CharacterActionTypes.Dialogue:
            //        await CutsceneEvents.I.InvokeDialogueShowText(Parent.Character, CharacterDialogue);
            //        break;
            //    case CharacterActionTypes.ShowName:
            //        await CutsceneEvents.I.InvokeShowCharacterName(Parent.Character);
            //        break;
            //    case CharacterActionTypes.HideName:
            //        await CutsceneEvents.I.InvokeHideCharacterName(Parent.Character);
            //        break;
            //    default:
            //        throw new NotImplementedException();
            //}
            return await base.Execute(gameBook, events);
        }
    }

    public enum CharacterActionTypes
    {
        Show,
        Hide,
        SetExpression,
        Dialogue,
        ShowName,
        HideName,
    }
}