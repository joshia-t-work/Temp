using DKP.CutsceneSystem;
using DKP.ObserverSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace DKP.SaveSystem.Data
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TScript"></typeparam>
    [Serializable, Obsolete("Deprecated as of 2.0")]
    public abstract class BaseSerializableAction<TScript> : BaseSerializableAction
        where TScript: BaseSerializableScript
    {
        /// <summary>
        /// Parent of the script object
        /// </summary>
        [SerializeReference, HideInInspector]
        public TScript Parent;

        ////[SerializeField, ReadOnly]
        ////public SerializablePointer NextPointer = new SerializablePointer();
        public override Task<BaseSerializableAction> Execute(SerializableGameBook gameBook, CutsceneEvents events)
        {
            if (NextAction[0] == null)
            {
                SerializableReferenceList<BaseSerializableAction> actions = new SerializableReferenceList<BaseSerializableAction>();
                if (Parent is SerializableMainThread mt)
                {
                    for (int i = 0; i < mt.Actions.Count; i++)
                    {
                        actions.Add(mt.Actions[i]);
                    }
                }
                if (Parent is SerializableScript ss)
                {
                    for (int i = 0; i < ss.Actions.Count; i++)
                    {
                        actions.Add(ss.Actions[i]);
                    }
                }
                if (Parent is SerializableCharacterScript scs)
                {
                    for (int i = 0; i < scs.Actions.Count; i++)
                    {
                        actions.Add(scs.Actions[i]);
                    }
                }
                int index = -1;
                for (int i = 0; i < actions.List.Count; i++)
                {
                    if (actions.List[i].Value.GetHashCode() == GetHashCode())
                    {
                        index = i;
                        break;
                    }
                }
                if (index > -1)
                {
                    if (index < actions.Count - 1)
                    {
                        return Task.FromResult(actions[index + 1]);
                    }
                }
            }
            return Task.FromResult(NextAction[0]);
        }
    }

    /// <summary>
    /// Base class for all actions
    /// </summary>
    [Serializable]
    public class BaseSerializableAction : ISerializableData
    {
        /// <summary>
        /// Pointed reference to the next action that will be executed, list in case there will be branched choices
        /// </summary>
        [SerializeReference, HideInInspector]
        public List<BaseSerializableAction> NextAction = new List<BaseSerializableAction>();

        [SerializeReference, HideInInspector]
        public int Flipped;
        public BaseSerializableAction()
        {

        }

        public virtual Task<BaseSerializableAction> Execute(SerializableGameBook gameBook, CutsceneEvents events)
        {
            return Task.FromResult(NextAction[0]);
        }
    }
}