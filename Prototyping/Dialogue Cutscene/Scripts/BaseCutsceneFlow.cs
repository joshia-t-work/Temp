using DKP.ObjectiveSystem;
using DKP.ObserverSystem.GameEvents;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace DKP.CutsceneSystem
{
    /// <summary>
    /// Abstract monobehaviour script that can listen to cutscene events
    /// </summary>
    [Obsolete]
    public abstract class BaseCutsceneFlow : MonoBehaviour
    {
        [SerializeField]
        protected CutsceneEvents CutsceneEvents;

        protected virtual void OnEnable()
        {
            //CutsceneEvents.Initialize.AddListener(InitializeListener);
            //CutsceneEvents.DialogueShowText.AddTask(DialogueShowTextListener);
            //CutsceneEvents.ShowCharacter.AddListener(ShowCharacterListener);
            //CutsceneEvents.HideCharacter.AddListener(HideCharacterListener);
            //CutsceneEvents.SetExpression.AddListener(SetExpressionListener);
            //CutsceneEvents.SetCharacterName.AddListener(SetCharacterNameListener);
            //CutsceneEvents.ShowCharacterName.AddListener(ShowCharacterNameListener);
            //CutsceneEvents.HideCharacterName.AddListener(HideCharacterNameListener);
            //CutsceneEvents.SetBackground.AddListener(SetBackgroundListener);
            //CutsceneEvents.SetVNStyle.AddListener(SetVNStyleListener);
            //CutsceneEvents.SetInGameStyle.AddListener(SetInGameStyleListener);
            //CutsceneEvents.StartTutorial.AddListener(StartTutorialListener);
            //CutsceneEvents.StartObjective.AddListener(StartObjectiveListener);
        }

        protected virtual void OnDisable()
        {
            //CutsceneEvents.Initialize.RemoveListener(InitializeListener);
            //CutsceneEvents.DialogueShowText.RemoveTask(DialogueShowTextListener);
            //CutsceneEvents.ShowCharacter.RemoveListener(ShowCharacterListener);
            //CutsceneEvents.HideCharacter.RemoveListener(HideCharacterListener);
            //CutsceneEvents.SetExpression.RemoveListener(SetExpressionListener);
            //CutsceneEvents.SetCharacterName.RemoveListener(SetCharacterNameListener);
            //CutsceneEvents.ShowCharacterName.RemoveListener(ShowCharacterNameListener);
            //CutsceneEvents.HideCharacterName.RemoveListener(HideCharacterNameListener);
            //CutsceneEvents.SetBackground.RemoveListener(SetBackgroundListener);
            //CutsceneEvents.SetVNStyle.RemoveListener(SetVNStyleListener);
            //CutsceneEvents.SetInGameStyle.RemoveListener(SetInGameStyleListener);
            //CutsceneEvents.StartTutorial.RemoveListener(StartTutorialListener);
            //CutsceneEvents.StartObjective.RemoveListener(StartObjectiveListener);
        }
        protected virtual void InitializeListener() { }
        protected virtual void SetInGameStyleListener() { }
        protected virtual void SetVNStyleListener() { }
        protected virtual void SetBackgroundListener(int backgroundIndex) { }
        protected virtual void HideCharacterNameListener(string charRef) { }
        protected virtual void ShowCharacterNameListener(string charRef) { }
        protected virtual void SetCharacterNameListener(string charRef, string name) { }
        protected virtual void SetExpressionListener(string charRef, string expressionName) { }
        protected virtual void HideCharacterListener(string charRef) { }
        protected virtual void ShowCharacterListener(string charRef, float position) { }
        protected virtual Task DialogueShowTextListener(string charRef, string text) { return Task.CompletedTask; }
        protected virtual void StartTutorialListener() {}
        protected virtual void StartObjectiveListener(BaseObjective objective) {}
    }
}