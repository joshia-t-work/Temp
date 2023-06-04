using DKP.StateMachineSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKP.ObjectiveSystem
{
    [CreateAssetMenu(fileName = "WinLose Offensive State", menuName = "SO/States/WinLose/Offensive")]
    public class WinLoseOffensive : State<WinLoseSTM>
    {
        public override string Name => WinLoseSTM.OFFENSIVE_STATE;
        public override void OnStateEnter(string stateFrom)
        {
            Data.EnemyTowers.AddObserverAndCall(enemyTowersChangedListener);
            Data.AllyTowers.AddObserverAndCall(allyTowersChangedListener);
        }
        public override void OnStateExit(string stateFrom)
        {
            Data.EnemyTowers.RemoveObserver(enemyTowersChangedListener);
            Data.AllyTowers.RemoveObserver(allyTowersChangedListener);
        }
        private void enemyTowersChangedListener(int towerCount)
        {
            if (towerCount == 0)
            {
                Data.Win();
            }
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