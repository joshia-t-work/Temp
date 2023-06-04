using DKP.ObserverSystem;
using DKP.StateMachineSystem;
using DKP.UnitSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DKP.CameraSystem
{
    /// <summary>
    /// Represents State for a GenericGroundUnit that can move and attack
    /// </summary>
    [RequireComponent(typeof(CameraController))]
    public class CameraSTM : StateMachineMono<CameraController>
    {
        public const string NONE_STATE = "None";
        public const string FOCUS_TARGET_STATE = "Focus Target";
        public const string FOCUS_TOWER_STATE = "Focus Tower";
        public override void Awake()
        {
            cam = GetComponent<CameraController>();
            SceneManager.sceneLoaded += OnSceneLoaded;
            base.Awake();
        }

        public override void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            base.OnDestroy();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadMode)
        {
            cam.transform.position = Vector3.zero;
        }

        private CameraController cam;
        public override CameraController data => cam;
    }
}