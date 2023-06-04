using DKP.StateMachineSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKP.ObjectiveSystem
{
    /// <summary>
    /// Defines a base Mission class for all missions. A mission is set for the whole stage.
    /// </summary>
    public abstract class BaseMission : ScriptableObject
    {
        public List<BaseObjective> MissionObjectives;
    }
}