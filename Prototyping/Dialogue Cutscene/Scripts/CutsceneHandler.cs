using DKP.CameraSystem;
using DKP.CutsceneSystem.VisualNovel;
using DKP.FlowSystem;
using DKP.Input;
using DKP.ObserverSystem.GameEvents;
using DKP.SaveSystem;
using DKP.SaveSystem.Data;
using DKP.SceneSystem;
using DKP.Singletonmanager;
using DKP.UnitSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace DKP.CutsceneSystem
{
    /// <summary>
    /// Handles commands related to handling main flow events
    /// </summary>
    public class CutsceneHandler : MonoBehaviour
    {
        [SerializeField]
        GameObject VNGameObject;
        [SerializeField]
        GameObject IGGameObject;
        [SerializeField]
        GenericMainCharacterSTM Player;
        CameraSTM camSTM;

        private void OnEnable()
        {
            VNGameObject.SetActive(true);
            IGGameObject.SetActive(true);

            if (camSTM == null)
            {
                camSTM = Singleton.instance.GetComponentInChildren<CameraSTM>();
            }
            if (Player == null)
            {
                Debug.LogError("Player prefab not linked", gameObject);
            }
            CutsceneEvents.I.SetInGameStyle += SetInGameStyleListener;
            CutsceneEvents.I.SetVNStyle += SetVNStyleListener;
            CutsceneEvents.I.StartObjective += StartObjectiveListener;
        }

        private void OnDisable()
        {
            CutsceneEvents.I.SetInGameStyle -= SetInGameStyleListener;
            CutsceneEvents.I.SetVNStyle -= SetVNStyleListener;
            CutsceneEvents.I.StartObjective -= StartObjectiveListener;
        }

        private Task StartObjectiveListener(CancellationToken arg, IObjective objective)
        {
            VNGameObject.SetActive(false);
            IGGameObject.SetActive(false);
            camSTM.SetState(CameraSTM.FOCUS_TARGET_STATE);
            CameraController.CameraTarget = Player.transform;
            return Task.CompletedTask;
        }

        protected Task SetInGameStyleListener(CancellationToken ct)
        {
            VNGameObject.SetActive(false);
            IGGameObject.SetActive(true);
            camSTM.SetState(CameraSTM.FOCUS_TARGET_STATE);
            return Task.CompletedTask;
        }

        protected Task SetVNStyleListener(CancellationToken ct)
        {
            VNGameObject.SetActive(true);
            IGGameObject.SetActive(false);
            camSTM.SetState(CameraSTM.NONE_STATE);
            return Task.CompletedTask;
        }
    }
}
