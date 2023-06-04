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
    /// Represents an Action that will be executed by a script in a script list
    /// </summary>
    [Obsolete("Deprecated as of 2.0")]
    [Serializable]
    public class SerializableScriptAction : BaseSerializableAction<SerializableScript>
    {
        [SerializeField, ReadOnly]
        public ScriptActionTypes ScriptActionType = ScriptActionTypes.ChangeBackground;
        // TODO: Actions: Change Background

        [SerializeField, ReadOnly]
        public int SceneImageIndex = 0;

        public SerializableScriptAction()
        {
            NextAction = new List<BaseSerializableAction>
            {
                null
            };
        }

        public override async Task<BaseSerializableAction> Execute(SerializableGameBook gameBook, CutsceneEvents events)
        {
            //switch (ScriptActionType)
            //{
            //    case ScriptActionTypes.ChangeBackground:
            //        await CutsceneEvents.I.InvokeSetBackground(SceneImageIndex);
            //        break;
            //    default:
            //        throw new NotImplementedException();
            //}
            return await base.Execute(gameBook, events);
        }
    }

    public enum ScriptActionTypes
    {
        ChangeBackground,
    }
}