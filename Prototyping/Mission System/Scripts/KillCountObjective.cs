using DKP.Game;
using DKP.ObserverSystem.GameEvents;
using DKP.StateMachineSystem;
using DKP.UnitSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace DKP.ObjectiveSystem
{
    /// <summary>
    /// Represents a kill-type objective where the player has to kill an amount of enemy
    /// </summary>
    [CreateAssetMenu(fileName = "Kill Count Objective", menuName = "SO/Missions/Objectives/Kill Count Objective")]
    public class KillCountObjective : BaseObjective
    {
        public int KillCount;
        GameEventObjectObject UnitDeath = null;

        protected override void startObjective(GameEvents gameEvents)
        {
            UnitDeath = gameEvents.UnitDeath;
            UnitDeath.AddListener(unitDeathListener);
        }

        public override void EndObjective()
        {
            UnitDeath?.RemoveListener(unitDeathListener);
            UnitDeath = null;
        }

        private void unitDeathListener(object killSource, object unitKilled)
        {
            if (killSource is Unit)
            {
                Unit killer = (Unit)killSource;
                if (killer.UnitTeam == "Ally")
                {
                    KillCount -= 1;
                    ObjectiveUpdateEvent.Invoke();
                    if (KillCount == 0)
                    {
                        ObjectiveCompleted.TrySetResult(true);
                    }
                }
            }
        }

        public override string GetDisplayText()
        {
            return $"{KillCount} more kills required.";
        }
    }
}