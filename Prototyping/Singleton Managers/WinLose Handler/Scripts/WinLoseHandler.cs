using DKP.ObserverSystem.GameEvents;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DKP.WinLoseSystem
{
    public class WinLoseHandler : MonoBehaviour
    {
        [SerializeField]
        GameEventNoParam enemyTowerCreated;

        [SerializeField]
        GameEventNoParam enemyTowerDestroyed;

        [SerializeField]
        GameEventNoParam allyTowerCreated;

        [SerializeField]
        GameEventNoParam allyTowerDestroyed;

        [SerializeField, ReadOnly]
        int enemyTowers;

        [SerializeField, ReadOnly]
        int allyTowers;

        private void Awake()
        {
            enemyTowers = 0;
            allyTowers = 0;
            enemyTowerCreated.AddListener(enemyTowerCreatedListener);
            enemyTowerDestroyed.AddListener(enemyTowerDestroyedListener);
            allyTowerCreated.AddListener(allyTowerCreatedListener);
            allyTowerDestroyed.AddListener(allyTowerDestroyedListener);
        }

        private void OnDestroy()
        {
            enemyTowerCreated.RemoveListener(enemyTowerCreatedListener);
            enemyTowerDestroyed.RemoveListener(enemyTowerDestroyedListener);
            allyTowerCreated.RemoveListener(allyTowerCreatedListener);
            allyTowerDestroyed.RemoveListener(allyTowerDestroyedListener);
        }

        private void enemyTowerCreatedListener()
        {
            enemyTowers += 1;
        }
        private void enemyTowerDestroyedListener()
        {
            enemyTowers -= 1;
            if (enemyTowers == 0)
            {
                SceneManager.LoadScene("Win");
            }
        }
        private void allyTowerCreatedListener()
        {
            allyTowers += 1;
        }
        private void allyTowerDestroyedListener()
        {
            allyTowers -= 1;
            if (allyTowers == 0)
            {
                SceneManager.LoadScene("Lose");
            }
        }
    }
}