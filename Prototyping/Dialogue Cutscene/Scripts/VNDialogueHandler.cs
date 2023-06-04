using DKP.FlowSystem;
using DKP.Input;
using DKP.ObserverSystem.GameEvents;
using DKP.SaveSystem.Data;
using DKP.Singletonmanager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using RichTextSubstringHelper;
using System.Threading;

namespace DKP.CutsceneSystem.VisualNovel
{
    /// <summary>
    /// Handles commands related to displaying dialogue
    /// </summary>
    public class VNDialogueHandler : MonoBehaviour
    {
        [SerializeField]
        Text screenName;
        [SerializeField]
        Text dialogue;

        bool skip = false;
        bool typing = false;
        TaskCompletionSource<bool> DialogueClicked;
        VNCharacter currentChar = null;

        protected void Awake()
        {
            InputManager.LeftClickInput.Value1.AddObserver(LeftClickListener);
        }

        protected void OnDestroy()
        {
            InputManager.LeftClickInput.Value1.RemoveObserver(LeftClickListener);
        }

        private void OnEnable()
        {
            CutsceneEvents.I.DialogueShowText += DialogueShowTextListener;
            CutsceneEvents.I.DialogueSetCharacter += DialogueSetCharacterListener;
        }

        private void OnDisable()
        {
            CutsceneEvents.I.DialogueShowText -= DialogueShowTextListener;
            CutsceneEvents.I.DialogueSetCharacter -= DialogueSetCharacterListener;
        }

        private void LeftClickListener(bool obj)
        {
            if (obj)
            {
                if (!typing)
                {
                    DialogueClicked?.TrySetResult(true);
                }
                else
                {
                    skip = true;
                }
            }
        }

        protected Task DialogueSetCharacterListener(CancellationToken ct, string charRef)
        {
            currentChar = VNCharacterHandler.Characters[charRef];
            return Task.CompletedTask;
        }

        protected async Task DialogueShowTextListener(CancellationToken ct, string text, bool shouldContinue)
        {
            skip = false;
            DialogueClicked = new TaskCompletionSource<bool>();
            if (currentChar != null)
            {
                if (currentChar.ShowName)
                {
                    await CutsceneEvents.I.InvokeSetCharacterName(ct, currentChar.Name, currentChar.Name);
                }
                else
                {
                    await CutsceneEvents.I.InvokeSetCharacterName(ct, currentChar.Name, "???");
                }
            }

            TaskCompletionSource<bool> waitMainThread = new TaskCompletionSource<bool>(false);
            Singleton.MainThread(() =>
            {
                if (currentChar != null)
                {
                    screenName.text = currentChar.DisplayName;
                }
                if (!shouldContinue)
                {
                    dialogue.text = "";
                }
                waitMainThread.SetResult(true);
            });
            await waitMainThread.Task;

            if (text != "")
            {
                typing = true;
                RichTextSubStringMaker maker = new RichTextSubStringMaker(text);
                // Start typewriter
                bool isTag = false;
                while (maker.IsConsumable())
                {
                    maker.Consume();
                    Singleton.MainThread(() => dialogue.text = maker.GetRichText());
                    if (!isTag && !skip)
                    {
                        await Task.Delay(Flow.FlowTypingDelay, ct);
                    }
                }
                typing = false;
                await DialogueClicked.Task;
                await Task.Delay(50, ct);
            }
        }
    }
}