using DKP.CutsceneSystem;
using DKP.Game;
using DKP.ObserverSystem;
using DKP.UIInGame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace DKP.SaveSystem.Data
{
    /// <summary>
    /// Represents an Action that will be executed by a main thread in a script list
    /// </summary>
    [Serializable, Obsolete("Deprecated as of 2.0")]
    public class SerializableMainThreadAction : BaseSerializableAction<SerializableMainThread>
    {
        [SerializeField, ReadOnly]
        public ThreadActionTypes ThreadActionType = ThreadActionTypes.ExecuteTrigger;

        [SerializeField, ReadOnly]
        public int ExecutedTriggerIndex = 0;
        // TODO: Actions: Execute Trigger, Start Tutorial, Start Next Objective

        public SerializableMainThreadAction()
        {
            NextAction = new List<BaseSerializableAction>
            {
                null
            };
        }

        public override async Task<BaseSerializableAction> Execute(SerializableGameBook gameBook, CutsceneEvents events)
        {
            //switch (ThreadActionType)
            //{
            //    case ThreadActionTypes.ExecuteTrigger:
            //        await CutsceneEvents.I.InvokeExecuteTrigger(ExecutedTriggerIndex);
            //        return await gameBook.Triggers[ExecutedTriggerIndex].Execute(gameBook, events);
            //    case ThreadActionTypes.SetVNStyle:
            //        await CutsceneEvents.I.InvokeSetVNStyle();
            //        break;
            //    case ThreadActionTypes.SetInGameStyle:
            //        await CutsceneEvents.I.InvokeSetInGameStyle();
            //        break;
            //    case ThreadActionTypes.StartTutorial:
            //        await CutsceneEvents.I.InvokeStartTutorial();
            //        break;
            //    case ThreadActionTypes.StartObjective:
            //        await CutsceneEvents.I.InvokeStartObjective(null);
            //        break;
            //    default:
            //        throw new NotImplementedException();
            //}
            return await base.Execute(gameBook, events);
        }
    }

    public enum ThreadActionTypes
    {
        ExecuteTrigger,
        SetVNStyle,
        SetInGameStyle,
        StartTutorial,
        StartObjective
    }
}