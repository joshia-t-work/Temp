using DKP.CutsceneSystem;
using DKP.ObjectiveSystem;
using DKP.SaveSystem.Data;
using DKP.Singletonmanager;
using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DKP.UIInGame
{
    public class ObjectivePanel : MonoBehaviour
    {
        [SerializeField]
        KillObjectivePanel killObjective;
        [Serializable]
        public class KillObjectivePanel
        {
            public GameObject panel;
            public TMP_Text displayText;
            public TMP_Text progressText;
            public Slider progressSlider;
        }

        private void Awake()
        {
            CutsceneEvents.I.StartObjective += StartObjective;
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            CutsceneEvents.I.StartObjective -= StartObjective;
        }

        private async Task StartObjective(System.Threading.CancellationToken ct, IObjective objective)
        {
            Singleton.MainThread(() =>
            {
                gameObject.SetActive(true);
            });
            if (objective is ObjectiveCommand objectiveCommand)
            {
                Singleton.MainThread(() =>
                {
                    killObjective.panel.SetActive(true);
                });
                await objectiveCommand.AwaitObjectiveCompletion(ct, progressCallback);
                Singleton.MainThread(() =>
                {
                    killObjective.panel.SetActive(false);
                });
            }
        }

        private void progressCallback(ObjectiveParams obj)
        {
            if (obj is ObjectiveKillParams objKill)
            {
                killObjective.displayText.text = $"Kill {objKill.Unit}";
                killObjective.progressSlider.value = (float)objKill.Killed / objKill.Count;
                killObjective.progressText.text = $"{objKill.Killed} / {objKill.Count}";
            }
        }
    }
}