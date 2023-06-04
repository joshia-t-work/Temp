using DKP.CameraSystem;
using DKP.CutsceneSystem;
using DKP.Game;
using DKP.ObjectiveSystem;
using DKP.SaveSystem;
using DKP.SaveSystem.Data;
using DKP.SceneSystem;
using DKP.Singletonmanager;
using DKP.UIInGame;
using DKP.UnitSystem;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace DKP.FlowSystem
{
    /// <summary>
    /// Handles main execution of the game flow including player input, dialogue and events.
    /// </summary>
    /// <remarks>Does nothing by itself, only calls methods and events</remarks>
    public class Flow : MonoBehaviour
    {
        public static Flow Instance = null;
        public static SerializableGameBook BookData = null;
        public static Dictionary<string, string> VariableDictionary = new Dictionary<string, string>();
        public static CancellationTokenSource cts;
        public static CancellationToken ct;

        public static int FlowTypingDelay = 50;

        [SerializeField]
        CutsceneEvents CutsceneEvents;

        [SerializeField]
        GameEvents GameEvents;

        [SerializeField, ReadOnly]
        SerializableGameBook _gameBook;

        [SerializeField]
        bool DevSkipCutscene;

        private void Awake()
        {
            Instance = this;

            if (!DevSkipCutscene)
            {
                _gameBook = new SerializableGameBook();
                _gameBook = SaveManager.LoadGameData<SerializableGameBook>(_gameBook.DataFileName);
                BookData = _gameBook;

                CutsceneEvents.I.ExecuteScript += I_ExecuteScript;
            }

            // Load Images before threading
            _gameBook.Deserialize();

            cts = new CancellationTokenSource();
            ct = cts.Token;
        }

        private void OnDestroy()
        {
            CutsceneEvents.I.ExecuteScript -= I_ExecuteScript;
            cts.Cancel();
        }

        async void Start()
        {
            //await Task.Delay(1000);
            if (!DevSkipCutscene)
            {
                await CutsceneEvents.I.InvokeInitialize(ct);
                await CutsceneEvents.I.InvokeSetVNStyle(ct);
                if (_gameBook != null)
                {
                    Task newTask = ExecuteGameBook(ct);
                    try
                    {
                        await newTask;
                    }
                    catch (TaskCanceledException) { }
                }
                else
                {
                    Debug.LogWarning("No gamebook found");
                }
            }
            await CutsceneEvents.I.InvokeStartObjective(ct, new ObjectiveCommand(""));
        }

        async Task ExecuteGameBook(CancellationToken ct)
        {
            //await Task.Delay(1000);

            SerializableActionScript script = _gameBook.Start;
            if (script == null)
            {
                Debug.LogWarning("No start script found");
            } else
            {
                BaseCommand command;
                for (int i = 0; i < script.Actions.Count; i++)
                {
                    command = await script.Actions[i].Execute(_gameBook, ct);
                    while (command != null)
                    {
                        command = await command.Execute(_gameBook, ct);
                    }
                }
            }
        }

        private async Task I_ExecuteScript(CancellationToken ct, string scriptName)
        {
            SerializableActionScript script = _gameBook.ActionScripts[scriptName];
            BaseCommand command;
            for (int i = 0; i < script.Actions.Count; i++)
            {
                command = await script.Actions[i].Execute(_gameBook, ct);
                while (command != null)
                {
                    command = await command.Execute(_gameBook, ct);
                }
            }
        }

        //private void onObjectiveUpdateListener()
        //{
        //    _objectivePanel.UpdateObjectiveText(_currentObjective.GetDisplayText());
        //}

        //async Task<BaseSerializableAction> ExecuteAction(BaseSerializableAction action)
        //{
        //    if (action is SerializableCharacterScriptAction)
        //    {
        //        return await ExecuteCharacterAction((SerializableCharacterScriptAction)action);
        //    }
        //    if (action is SerializableScriptAction)
        //    {
        //        return await ExecuteScriptAction((SerializableScriptAction)action);
        //    }
        //    return null;
        //}

        //async Task<BaseSerializableAction> ExecuteCharacterAction(SerializableCharacterScriptAction action)
        //{
        //    switch (action.CharacterActionType)
        //    {
        //        case CharacterActionTypes.Show:
        //            CutsceneEvents.ShowCharacter.Invoke(action.Parent.Character, action.Position);
        //            break;
        //        case CharacterActionTypes.Hide:
        //            CutsceneEvents.HideCharacter.Invoke(action.Parent.Character);
        //            break;
        //        case CharacterActionTypes.SetExpression:
        //            CutsceneEvents.SetExpression.Invoke(action.Parent.Character, action.CharacterExpression);
        //            break;
        //        case CharacterActionTypes.Dialogue:
        //            await CutsceneEvents.DialogueShowText.StartTask(action.Parent.Character, action.CharacterDialogue);
        //            break;
        //        case CharacterActionTypes.ShowName:
        //            CutsceneEvents.ShowCharacterName.Invoke(action.Parent.Character);
        //            break;
        //        case CharacterActionTypes.HideName:
        //            CutsceneEvents.HideCharacterName.Invoke(action.Parent.Character);
        //            break;
        //    }
        //    if (action.NextAction[0] == null)
        //    {
        //        int index = action.Parent.Actions.IndexOf(action);
        //        if (index > -1)
        //        {
        //            if (index < action.Parent.Actions.Count - 1)
        //            {
        //                return action.Parent.Actions[index + 1];
        //            }
        //        }
        //    }
        //    else
        //    {
        //        //SerializableCharacterScriptAction next = (SerializableCharacterScriptAction)action.NextAction[0];
        //        //Debug.Log($"{action.Parent.CharacterParent.Name} {action.Parent.Actions.IndexOf(action)} => {((SerializableCharacterScriptAction)action.NextAction[0]).Parent.CharacterParent.Name} {((SerializableCharacterScriptAction)action.NextAction[0]).Parent.Actions.IndexOf(((SerializableCharacterScriptAction)action.NextAction[0]))}");
        //        return action.NextAction[0];
        //    }
        //    return null;
        //}

        //async Task<BaseSerializableAction> ExecuteScriptAction(SerializableScriptAction action)
        //{
        //    switch (action.ScriptActionType)
        //    {
        //        case ScriptActionTypes.ChangeBackground:
        //            CutsceneEvents.SetBackground.Invoke(action.SceneImageIndex);
        //            break;
        //    }
        //    if (action.NextAction[0] == null)
        //    {
        //        int index = action.Parent.Actions.IndexOf(action);
        //        if (index < action.Parent.Actions.Count - 2)
        //        {
        //            return action.Parent.Actions[index + 1];
        //        }
        //    }
        //    else
        //    {
        //        return action.NextAction[0];
        //    }
        //    return null;
        //}

        public void EditScene()
        {
            cts.Cancel();
            Singleton.instance.GetComponentInChildren<CameraSTM>().SetState(CameraSTM.NONE_STATE);
            SceneLoader.LoadScene("Dev Level Editor");
        }

        public static void UnitKill(object killSource, Unit killed)
        {
            Instance?.GameEvents.UnitDeath.Invoke(killSource, killed);
        }

        /// <summary>
        /// Sets a variable in the Flow database
        /// </summary>
        /// <param name="varName"></param>
        /// <param name="varValue"></param>
        public static void SetVariable(string varName, string varValue)
        {
            if (VariableDictionary.ContainsKey(varName))
            {
                VariableDictionary[varName] = varValue;
            } else
            {
                VariableDictionary.Add(varName, varValue);
            }
        }

        /// <summary>
        /// Gets a variable in the Flow database
        /// </summary>
        /// <param name="varName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string GetVariable(string varName, string defaultValue = "")
        {
            if (VariableDictionary.TryGetValue(varName, out string value))
            {
                return value;
            } else
            {
                return defaultValue;
            }
        }
    }
    public class Character
    {
        public SerializableImageContainer Data;
        public GameObject GameObject;
        public bool ShowName = true;
        public Image Image;
    }
}
