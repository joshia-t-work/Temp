using DKP.ObserverSystem.GameEvents;
using MyBox;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace DKP.CameraSystem
{
    public class CameraController : MonoBehaviour
    {[SerializeField, InitializationField]
        GameEventGameObject enemyTowerCreated;

        [SerializeField, InitializationField]
        GameEventGameObject enemyTowerDestroyed;

        [SerializeField, InitializationField]
        GameEventGameObject allyTowerCreated;

        [SerializeField, InitializationField]
        GameEventGameObject allyTowerDestroyed;

        public static Transform CameraTarget;

        List<GameObject> allyTowers = new List<GameObject>();
        public ReadOnlyCollection<GameObject> AllyTowers;
        List<GameObject> enemyTowers = new List<GameObject>();
        public ReadOnlyCollection<GameObject> EnemyTowers;
        Vector3 targetPosition;
        float targetSize;

        public bool isEnabled;

        [SerializeField, ReadOnly]
        Camera cam;
#if UNITY_EDITOR
        [ButtonMethod]
        public virtual void updateReferences()
        {
            cam = GetComponentInChildren<Camera>();
        }
#endif
        public virtual void Awake()
        {
            targetPosition = transform.position;
            targetSize = cam.orthographicSize;
            AllyTowers = allyTowers.AsReadOnly();
            EnemyTowers = enemyTowers.AsReadOnly();
            allyTowers.Clear();
            enemyTowers.Clear();
            enemyTowerCreated.AddListener(enemyTowerCreatedListener);
            enemyTowerDestroyed.AddListener(enemyTowerDestroyedListener);
            allyTowerCreated.AddListener(allyTowerCreatedListener);
            allyTowerDestroyed.AddListener(allyTowerDestroyedListener);
        }

        public virtual void OnDestroy()
        {
            enemyTowerCreated.RemoveListener(enemyTowerCreatedListener);
            enemyTowerDestroyed.RemoveListener(enemyTowerDestroyedListener);
            allyTowerCreated.RemoveListener(allyTowerCreatedListener);
            allyTowerDestroyed.RemoveListener(allyTowerDestroyedListener);
        }

        private void allyTowerDestroyedListener(GameObject obj)
        {
            allyTowers.Remove(obj);
        }

        private void allyTowerCreatedListener(GameObject obj)
        {
            allyTowers.Add(obj);
        }

        private void enemyTowerDestroyedListener(GameObject obj)
        {
            enemyTowers.Remove(obj);
        }

        private void enemyTowerCreatedListener(GameObject obj)
        {
            enemyTowers.Add(obj);
        }

        private void FixedUpdate()
        {
            if (isEnabled)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 2f);
                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, Time.deltaTime * 2f);
            }
        }

        public void SetTarget(Vector2 position, float size)
        {
            targetPosition = new Vector3(position.x, position.y, -10f);
            targetSize = size;
        }
    }
}
