using DKP.ObserverSystem;
using DKP.ObserverSystem.GameEvents;
using DKP.StateMachineSystem;
using MyBox;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DKP.ObjectiveSystem
{
    /// <summary>
    /// Represents State Machine for a Win Lose Condition System
    /// </summary>
    public class WinLoseSTM : StateMachineMono<WinLoseSTM>
    {
        public override WinLoseSTM data => this;
        public const string INIT_STATE = "Init";
        // win if all enemy tower destroyed, lose if our tower destroyed
        public const string OFFENSIVE_STATE = "Offensive";
        // lose if our tower destroyed
        public const string DEFENSIVE_STATE = "Defensive";
        // lose if die
        public const string SURVIVAL_STATE = "Survival";

        [SerializeField, InitializationField]
        GameEventGameObject enemyTowerCreated;

        [SerializeField, InitializationField]
        GameEventGameObject enemyTowerDestroyed;

        [SerializeField, InitializationField]
        GameEventGameObject allyTowerCreated;

        [SerializeField, InitializationField]
        GameEventGameObject allyTowerDestroyed;

        [SerializeField, ReadOnly]
        Observable<int> enemyTowers;
        public Observable<int> EnemyTowers => enemyTowers;

        [SerializeField, ReadOnly]
        Observable<int> allyTowers;
        public Observable<int> AllyTowers => allyTowers;

        public override void Awake()
        {
            enemyTowers = new Observable<int>(0);
            allyTowers = new Observable<int>(0);
            base.Awake();
            enemyTowerCreated.AddListener(enemyTowerCreatedListener);
            enemyTowerDestroyed.AddListener(enemyTowerDestroyedListener);
            allyTowerCreated.AddListener(allyTowerCreatedListener);
            allyTowerDestroyed.AddListener(allyTowerDestroyedListener);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            enemyTowerCreated.RemoveListener(enemyTowerCreatedListener);
            enemyTowerDestroyed.RemoveListener(enemyTowerDestroyedListener);
            allyTowerCreated.RemoveListener(allyTowerCreatedListener);
            allyTowerDestroyed.RemoveListener(allyTowerDestroyedListener);
        }

        private void enemyTowerCreatedListener(GameObject tower)
        {
            enemyTowers.Value += 1;
        }
        private void enemyTowerDestroyedListener(GameObject tower)
        {
            enemyTowers.Value -= 1;
        }
        private void allyTowerCreatedListener(GameObject tower)
        {
            allyTowers.Value += 1;
        }
        private void allyTowerDestroyedListener(GameObject tower)
        {
            allyTowers.Value -= 1;
        }
        public void Win()
        {
            SceneManager.LoadScene("Win");
        }
        public void Lose()
        {
            SceneManager.LoadScene("Lose");
        }
    }
}