using DKP.StateMachineSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKP.ObjectiveSystem
{
    [CreateAssetMenu(fileName = "WinLose Defensive State", menuName = "SO/States/WinLose/Defensive")]
    public class WinLoseDefensive : State<WinLoseSTM>
    {
        public override string Name => WinLoseSTM.DEFENSIVE_STATE;
        public override void OnStateEnter(string stateFrom)
        {
            Data.AllyTowers.AddObserverAndCall(allyTowersChangedListener);
        }
        public override void OnStateExit(string stateFrom)
        {
            Data.AllyTowers.RemoveObserver(allyTowersChangedListener);
        }
        private void allyTowersChangedListener(int towerCount)
        {
            if (towerCount == 0)
            {
                Data.Lose();
            }
        }
    }
}