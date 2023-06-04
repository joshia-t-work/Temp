using DKP.ObserverSystem;
using DKP.SaveSystem.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DKP.Singletonmanager
{
    public class Singleton : MonoBehaviour
    {
        public static Singleton instance;
        Queue<Action> jobs = new Queue<Action>();
        [SerializeField]
        GameObject[] gameObjects;
        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
            } else
            {
                instance = this;
                for (int i = 0; i < gameObjects.Length; i++)
                {
                    gameObjects[i].SetActive(true);
                }
            }
            DontDestroyOnLoad(this);
        }
        private void Update()
        {
            while (jobs.Count > 0)
                jobs.Dequeue().Invoke();
        }

        public static void MainThread(Action newJob)
        {
            instance.jobs.Enqueue(newJob);
        }
    }
}