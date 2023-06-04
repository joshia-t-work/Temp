using DKP.CameraSystem;
using DKP.CutsceneSystem.VisualNovel;
using DKP.DialogueSystem;
using DKP.FlowSystem;
using DKP.ObjectiveSystem;
using DKP.ObserverSystem.GameEvents;
using DKP.SaveSystem.Data;
using DKP.Singletonmanager;
using DKP.UnitSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

namespace DKP.CutsceneSystem.InGame
{
    /// <summary>
    /// Handles commands related to characters and used to contain characters
    /// </summary>
    public class IGCharacterHandler : MonoBehaviour
    {
        public static Dictionary<string, IGCharacter> Characters = new Dictionary<string, IGCharacter>();
        [SerializeField]
        GameObject characterPrefab;
        IGCharacter currentChar = null;
        protected void Awake()
        {
            Characters = new Dictionary<string, IGCharacter>();
        }
        private void OnEnable()
        {
            CutsceneEvents.I.Initialize += InitializeListener;
            CutsceneEvents.I.ShowCharacter += ShowCharacterListener;
            CutsceneEvents.I.HideCharacter += HideCharacterListener;
            CutsceneEvents.I.SetExpression += SetExpressionListener;
            CutsceneEvents.I.StartObjective += StartObjectiveListener;
            CutsceneEvents.I.DialogueShowText += DialogueShowTextListener;
            CutsceneEvents.I.DialogueSetCharacter += DialogueSetCharacterListener;
            CutsceneEvents.I.CharacterMoveCommand += CharacterMoveCommandListener;
        }

        private void OnDisable()
        {
            CutsceneEvents.I.Initialize -= InitializeListener;
            CutsceneEvents.I.ShowCharacter -= ShowCharacterListener;
            CutsceneEvents.I.HideCharacter -= HideCharacterListener;
            CutsceneEvents.I.SetExpression -= SetExpressionListener;
            CutsceneEvents.I.StartObjective -= StartObjectiveListener;
            CutsceneEvents.I.DialogueShowText -= DialogueShowTextListener;
            CutsceneEvents.I.DialogueSetCharacter -= DialogueSetCharacterListener;
            CutsceneEvents.I.CharacterMoveCommand -= CharacterMoveCommandListener;
        }
        protected Task InitializeListener(CancellationToken ct)
        {
            Dictionary<string, Transform> tempDict = new Dictionary<string, Transform>();
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform t = transform.GetChild(i);
                tempDict.Add(t.name, t);
            }
            for (int i = 0; i < Flow.BookData.Images.Count; i++)
            {
                SerializableImageContainer data = Flow.BookData.Images[i];
                IGCharacter character = new IGCharacter();
                character.Data = data;
                if (tempDict.TryGetValue(Flow.BookData.Images[i].ObjectName, out Transform t))
                {
                    character.GameObject = t.gameObject;
                } else
                {
                    character.GameObject = Instantiate(characterPrefab, Vector3.zero, Quaternion.identity, transform);
                    character.GameObject.name = Flow.BookData.Images[i].ObjectName;
                    character.GameObject.SetActive(false);
                }
                character.STM = character.GameObject.GetComponentInChildren<GenericMainCharacterSTM>();
                character.Dialogue = character.GameObject.GetComponentInChildren<InGameDialogue>();
                Characters.Add(Flow.BookData.Images[i].ObjectName, character);
            }
            return Task.CompletedTask;
        }
        protected Task ShowCharacterListener(CancellationToken ct, string charRef)
        {
            TaskCompletionSource<bool> waitMainThread = new TaskCompletionSource<bool>(false);
            Singleton.MainThread(() =>
            {
                Characters[charRef].GameObject.SetActive(true);
                Characters[charRef].Dialogue.gameObject.SetActive(false);
                waitMainThread.SetResult(true);
                //Characters[charRef].GameObject.transform.position = new Vector3(Screen.width * position, Screen.height / 2, 0);
            });
            return waitMainThread.Task;
        }

        protected Task HideCharacterListener(CancellationToken ct, string charRef)
        {
            TaskCompletionSource<bool> waitMainThread = new TaskCompletionSource<bool>(false);
            Singleton.MainThread(() => {
                Characters[charRef].GameObject.SetActive(false);
                waitMainThread.SetResult(true);
            });
            return Task.CompletedTask;
        }

        protected Task SetExpressionListener(CancellationToken ct, string charRef, string expressionName)
        {
            try
            {
                SerializableImageContainer character = Characters[charRef].Data;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogWarning($"{charRef} not found! Characters:");
                foreach (KeyValuePair<string, IGCharacter> character in Characters)
                {
                    Debug.LogWarning($" - {character.Value.Data.ObjectName}");
                }
                throw;
            }
            TaskCompletionSource<bool> waitMainThread = new TaskCompletionSource<bool>(false);
            Singleton.MainThread(() =>
            {
                Characters[charRef].STM.data.CurrentExpression = expressionName;
                Characters[charRef].STM.SetState(GenericMainCharacterSTM.EXPRESSION_STATE);
                waitMainThread.SetResult(true);
            });
            return waitMainThread.Task;
        }

        protected Task StartObjectiveListener(CancellationToken ct, IObjective objective)
        {
            TaskCompletionSource<bool> waitMainThread = new TaskCompletionSource<bool>(false);
            Singleton.MainThread(() =>
            {
                foreach (IGCharacter character in Characters.Values)
                {
                    character.GameObject.SetActive(false);
                }
                waitMainThread.SetResult(true);
            });
            return waitMainThread.Task;
        }

        private Task DialogueSetCharacterListener(CancellationToken ct, string charRef)
        {
            currentChar = Characters[charRef];
            return Task.CompletedTask;
        }


        // Dialogue Handler
        bool skip = false;
        protected async Task DialogueShowTextListener(CancellationToken ct, string text, bool shouldContinue)
        {
            bool shouldEnd = !text.EndsWith("->");
            skip = false;
            string displayedText = text;
            if (!shouldEnd)
            {
                displayedText = text.Substring(0, text.Length - 2);
            }
            if (currentChar != null && displayedText != "")
            {
                CameraController.CameraTarget = currentChar.STM.transform;
                IGCharacter tempChar = currentChar;
                TaskCompletionSource<bool> waitMainThread = new TaskCompletionSource<bool>(false);
                Singleton.MainThread(() =>
                {
                    try
                    {
                        tempChar.Dialogue.gameObject.SetActive(true);
                        waitMainThread.SetResult(true);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                });
                await waitMainThread.Task;

                await currentChar.Dialogue.TypeText(displayedText, shouldEnd, ct);

                if (shouldEnd)
                {
                    waitMainThread = new TaskCompletionSource<bool>(false);
                    Singleton.MainThread(() =>
                    {
                        try
                        {
                            tempChar.Dialogue.gameObject.SetActive(false);
                            waitMainThread.SetResult(true);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    });
                    await waitMainThread.Task;
                }
            }
        }

        private async Task CharacterMoveCommandListener(CancellationToken ct, string charRef, float position, float speed)
        {
            IGCharacter character = Characters[charRef];
            if (speed >= 99999f)
            {
                character.STM.transform.position = new Vector3(position, character.STM.transform.position.y, character.STM.transform.position.z);
            } else
            {
                character.STM.data.moveSpeed = character.STM.data.moveSpeed * speed;
                character.STM.data.acceleration = character.STM.data.acceleration * speed;
                float moveSpeedThreshold = character.STM.data.moveSpeed / 15f;
                float accelerationThreshold = character.STM.data.acceleration / 6f;
                while (Mathf.Abs(position - character.STM.transform.position.x) > 0.5f * Mathf.Max(accelerationThreshold, moveSpeedThreshold))
                {
                    if (position > character.STM.transform.position.x)
                    {
                        character.STM.HorizontalMoveEvent.Invoke(1f);
                    }
                    else
                    {
                        character.STM.HorizontalMoveEvent.Invoke(-1f);
                    }
                    await Task.Delay(100, ct);
                }
                character.STM.data.moveSpeed = character.STM.data.moveSpeed / speed;
                character.STM.data.acceleration = character.STM.data.acceleration / speed;
                character.STM.HorizontalMoveEvent.Invoke(0f);
            }
        }
    }
    public class IGCharacter
    {
        public SerializableImageContainer Data;
        public GameObject GameObject;
        public GenericMainCharacterSTM STM;
        public InGameDialogue Dialogue;
    }
}