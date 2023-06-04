using DKP.FlowSystem;
using DKP.ObserverSystem.GameEvents;
using DKP.SaveSystem.Data;
using DKP.Singletonmanager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace DKP.CutsceneSystem.VisualNovel
{
    /// <summary>
    /// Handles commands related to switching background
    /// </summary>
    public class VNBackgroundHandler : MonoBehaviour
    {
        [SerializeField]
        Image background;

        private void OnEnable()
        {
            CutsceneEvents.I.SetBackground += SetBackgroundListener;
        }

        private void OnDisable()
        {
            CutsceneEvents.I.SetBackground -= SetBackgroundListener;
        }

        protected Task SetBackgroundListener(CancellationToken ct, string bgName)
        {
            ChangeBackground(ct, Flow.BookData.Backgrounds[bgName].Image.Sprite);
            return Task.CompletedTask;
        }

        public void ChangeBackground(CancellationToken ct, Sprite sprite)
        {
            Singleton.MainThread(() => background.sprite = sprite);
        }
    }
}