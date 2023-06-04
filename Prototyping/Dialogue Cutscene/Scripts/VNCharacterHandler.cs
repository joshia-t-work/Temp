using DKP.FlowSystem;
using DKP.SaveSystem.Data;
using DKP.Singletonmanager;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace DKP.CutsceneSystem.VisualNovel
{
    /// <summary>
    /// Handles commands related to characters and used to contain characters
    /// </summary>
    public class VNCharacterHandler : MonoBehaviour
    {
        public static Dictionary<string, VNCharacter> Characters = new Dictionary<string, VNCharacter>();
        [SerializeField]
        GameObject characterPrefab;
        protected void Awake()
        {
            Characters = new Dictionary<string, VNCharacter>();
        }
        private void OnEnable()
        {
            CutsceneEvents.I.Initialize += InitializeListener;
            CutsceneEvents.I.ShowCharacter += ShowCharacterListener;
            CutsceneEvents.I.HideCharacter += HideCharacterListener;
            CutsceneEvents.I.ShowCharacterName += ShowCharacterNameListener;
            CutsceneEvents.I.HideCharacterName += HideCharacterNameListener;
            CutsceneEvents.I.SetExpression += SetExpressionListener;
            CutsceneEvents.I.SetCharacterName += SetCharacterNameListener;
            CutsceneEvents.I.StartObjective += StartObjectiveListener;
            CutsceneEvents.I.CharacterMoveCommand += CharacterMoveCommandListener;
        }

        private void OnDisable()
        {
            CutsceneEvents.I.Initialize -= InitializeListener;
            CutsceneEvents.I.ShowCharacter -= ShowCharacterListener;
            CutsceneEvents.I.HideCharacter -= HideCharacterListener;
            CutsceneEvents.I.ShowCharacterName -= ShowCharacterNameListener;
            CutsceneEvents.I.HideCharacterName -= HideCharacterNameListener;
            CutsceneEvents.I.SetExpression -= SetExpressionListener;
            CutsceneEvents.I.SetCharacterName -= SetCharacterNameListener;
            CutsceneEvents.I.StartObjective -= StartObjectiveListener;
            CutsceneEvents.I.CharacterMoveCommand -= CharacterMoveCommandListener;
        }
        protected Task InitializeListener(CancellationToken ct)
        {
            for (int i = 0; i < Flow.BookData.Images.Count; i++)
            {
                SerializableImageContainer data = Flow.BookData.Images[i];
                VNCharacter character = new VNCharacter();
                character.Name = data.ObjectName;
                character.Data = data;
                character.GameObject = Instantiate(characterPrefab, Vector3.zero, Quaternion.identity, characterPrefab.transform.parent);
                character.GameObject.transform.position = new Vector3(character.GameObject.transform.position.x, Screen.height / 2, 0);
                character.Image = character.GameObject.GetComponent<Image>();
                if (data.Images.Count > 0)
                {
                    character.Image.sprite = data.Images[0].Image.Sprite;
                }
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
                waitMainThread.SetResult(true);
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
            return waitMainThread.Task;
        }
        protected Task HideCharacterNameListener(CancellationToken ct, string charRef)
        {
            Characters[charRef].ShowName = false;
            return Task.CompletedTask;
        }

        protected Task ShowCharacterNameListener(CancellationToken ct, string charRef)
        {
            Characters[charRef].ShowName = true;
            return Task.CompletedTask;
        }

        protected async Task SetExpressionListener(CancellationToken ct, string charRef, string expressionName)
        {
            SerializableImageContainer character = Characters[charRef].Data;
            for (int i = 0; i < character.Images.Count; i++)
            {
                SerializableImageText currentImage = character.Images[i];
                if (currentImage.Name == expressionName)
                {
                    TaskCompletionSource<bool> waitMainThread = new TaskCompletionSource<bool>(false);
                    Singleton.MainThread(() =>
                    {
                        try
                        {
                            Characters[charRef].Image.sprite = currentImage.Image.Sprite;
                            waitMainThread.SetResult(true);
                        }
                        catch (Exception ex)
                        {
                            if (ex is IndexOutOfRangeException || ex is ArgumentOutOfRangeException)
                            {
                                Debug.LogWarning($"Missing expression {charRef}'s expression: {expressionName} expressions:");
                                for (int ii = 0; ii < character.Images.Count; ii++)
                                {
                                    Debug.LogWarning($" - {character.Images[ii].Name}");
                                }
                            }
                            else
                            {
                                throw;
                            }
                        }
                    });
                    await waitMainThread.Task;
                }
            }
        }
        protected Task SetCharacterNameListener(CancellationToken ct, string charRef, string name)
        {
            Characters[charRef].DisplayName = name;
            return Task.CompletedTask;
        }

        protected Task StartObjectiveListener(CancellationToken ct, IObjective objective)
        {
            TaskCompletionSource<bool> waitMainThread = new TaskCompletionSource<bool>(false);
            Singleton.MainThread(() => {
                foreach (VNCharacter character in Characters.Values)
                {
                    character.GameObject.SetActive(false);
                }
                waitMainThread.SetResult(true);
            });
            return waitMainThread.Task;
        }

        private Task CharacterMoveCommandListener(CancellationToken ct, string charRef, float position, float speed)
        {
            Characters[charRef].GameObject.transform.position = new Vector3(Screen.width * position, Screen.height / 2, 0);
            return Task.CompletedTask;
        }
    }
    public class VNCharacter
    {
        public SerializableImageContainer Data;
        public GameObject GameObject;
        public bool ShowName = true;
        public string Name;
        public string DisplayName;
        public Image Image;
    }
}