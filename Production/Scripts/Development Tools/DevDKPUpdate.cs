#if DEVELOPMENT_BUILD || UNITY_EDITOR
using DKP.SaveSystem.Data;
using Newtonsoft.Json;
using UnityEngine;

namespace DKP.Development.LevelEditor
{
    public static class DevDKPUpdate
    {
#pragma warning disable 0612
#pragma warning disable CS0618 // Type or member is obsolete
        /// <summary>
        /// Updates, if any, outdated data
        /// </summary>
        /// <param name="gameBook">Data</param>
        public static void Update(SerializableGameBook gameBook)
        {
            if (gameBook.Version < new SerializableVersion("1.5"))
            {
                Debug.LogWarning($"Attempting to update from {gameBook.Version} to 1.5");
                gameBook.Version = new SerializableVersion("1.5");
                if (gameBook.DesignerCamera == null)
                {
                    gameBook.DesignerCamera = new SerializableCameraPosition(0f, 0f, -10f);
                }
                if (gameBook.EditorCamera == null)
                {
                    gameBook.EditorCamera = new SerializableCameraPosition(gameBook.WorldPos.X, gameBook.WorldPos.Y, gameBook.WorldPos.Z);
                    gameBook.EditorCamera.Zoom = gameBook.CamZoom;
                }
                foreach (var CharacterScript in gameBook.CharacterScripts)
                {
                    if (CharacterScript.CharacterParent != null)
                    {
                        CharacterScript.Character = CharacterScript.CharacterParent.Name;
                        CharacterScript.CharacterParent = null;
                    } else
                    {
                        CharacterScript.Character = gameBook.Characters[CharacterScript.CharacterIndex].Name;
                    }
                    foreach (var ActionRef in CharacterScript.Actions.List)
                    {
                        var Action = ActionRef.Value;
                        if (Action.CharacterExpression == "")
                        {
                            for (int i = 0; i < gameBook.Characters.Count; i++)
                            {
                                if (gameBook.Characters[i].ObjectName == CharacterScript.Character)
                                {
                                    if (gameBook.Characters[i].Images.Count > Action.CharacterExpressionIndex)
                                    {
                                        Action.CharacterExpression = gameBook.Characters[i].Images[Action.CharacterExpressionIndex].Name;
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            if (gameBook.Version < new SerializableVersion("2.0"))
            {
                Debug.LogWarning($"Attempting to update from {gameBook.Version} to 2.0");
                gameBook.ActionScripts = new ActionScriptContainer();
                SerializableActionScript MainThread = gameBook.ActionScripts.AddNew();
                MainThread.SetObjectName("START");
                MainThread.WorldPosition = gameBook.MainThread.WorldPosition;
                gameBook.Images = new ImageContainerContainer();
                SerializableImageContainer sceneImages = gameBook.Images.AddNew();
                for (int ii = 0; ii < gameBook.SceneSetting.Images.Count; ii++)
                {
                    gameBook.SceneSetting.Images[ii].SetName(gameBook.SceneSetting.Images[ii].Label);
                    sceneImages.Images.Add(gameBook.SceneSetting.Images[ii]);
                }
                sceneImages.SetObjectName(SerializableImageContainer.BACKGROUND_CONTAINER_NAME);
                sceneImages.WorldPosition = gameBook.SceneSetting.WorldPos;
                for (int i = 0; i < gameBook.Characters.Count; i++)
                {
                    SerializableImageContainer imageContainer = gameBook.Images.AddNew();
                    for (int ii = 0; ii < gameBook.Characters[i].Images.Count; ii++)
                    {
                        gameBook.Characters[i].Images[ii].SetName(gameBook.Characters[i].Images[ii].Label);
                        imageContainer.Images.Add(gameBook.Characters[i].Images[ii]);
                    }
                    imageContainer.SetObjectName(gameBook.Characters[i].Name);
                    imageContainer.WorldPosition = gameBook.Characters[i].WorldPos;
                }


                gameBook.Version = new SerializableVersion("2.0");
                for (int i = 0; i < gameBook.MainThread.Actions.List.Count; i++)
                {
                    SerializableMainThreadAction threadAction = gameBook.MainThread.Actions[i];
                    ParseAction(gameBook, threadAction, gameBook.ActionScripts["START"]);
                }
                gameBook.SceneSetting.Images.Clear();
                gameBook.MainThread.Actions.List.Clear();
                gameBook.Scripts.Clear();
                gameBook.Characters.Clear();
                gameBook.CharacterScripts.Clear();
                gameBook.Triggers.Clear();
                // TODO: Update Obsolete data
            }
            gameBook.Version = DevEditorDataContainer.Instance.Version;
        }

        private static BaseSerializableAction ParseAction(SerializableGameBook gameBook, BaseSerializableAction action, SerializableActionScript actionScript)
        {
            if (action is SerializableMainThreadAction threadAction)
            {
                return ParseThreadAction(gameBook, threadAction, actionScript);
            }
            if (action is SerializableCharacterScriptAction characterAction)
            {
                return ParseCharacterAction(gameBook, characterAction, actionScript);
            }
            if (action is SerializableScriptAction scriptAction)
            {
                return ParseScriptAction(gameBook, scriptAction, actionScript);
            }
            return null;
        }
        private static BaseSerializableAction ParseThreadAction(SerializableGameBook gameBook, SerializableMainThreadAction threadAction, SerializableActionScript actionScript)
        {
            switch (threadAction.ThreadActionType)
            {
                case ThreadActionTypes.ExecuteTrigger:
                    SerializableTrigger trigger = gameBook.Triggers[threadAction.ExecutedTriggerIndex];
                    actionScript.Actions.Add(new ExecuteCommand($"Execute \"{trigger.Label}\"", trigger.Label));
                    SerializableActionScript newActionScript = gameBook.ActionScripts.AddNew();
                    newActionScript.SetObjectName(trigger.Label);
                    newActionScript.WorldPosition = trigger.WorldPos;
                    BaseSerializableAction action = trigger.NextAction[0];
                    while (action != null)
                    {
                        action = ParseAction(gameBook, action, newActionScript);
                    }
                    break;
                case ThreadActionTypes.SetVNStyle:
                    actionScript.Actions.Add(new SetStyleCommand($"SetStyle VISUALNOVEL", "VISUALNOVEL"));
                    break;
                case ThreadActionTypes.SetInGameStyle:
                    actionScript.Actions.Add(new SetStyleCommand($"SetStyle INGAME", "INGAME"));
                    break;
                default:
                    break;
            }
            return threadAction.NextAction[0];
        }
        private static BaseSerializableAction ParseCharacterAction(SerializableGameBook gameBook, SerializableCharacterScriptAction characterAction, SerializableActionScript actionScript)
        {
            switch (characterAction.CharacterActionType)
            {
                case CharacterActionTypes.Show:
                    actionScript.Actions.Add(new CharacterCommand($"[{characterAction.Parent.Character} -> SHOW]", "", characterAction.Parent.Character, "SHOW"));
                    break;
                case CharacterActionTypes.Hide:
                    actionScript.Actions.Add(new CharacterCommand($"[{characterAction.Parent.Character} -> HIDE]", "", characterAction.Parent.Character, "HIDE"));
                    break;
                case CharacterActionTypes.SetExpression:
                    actionScript.Actions.Add(new CharacterCommand($"[{characterAction.Parent.Character} -> \"{characterAction.CharacterExpression}\"]", "", characterAction.Parent.Character, characterAction.CharacterExpression));
                    break;
                case CharacterActionTypes.Dialogue:
                    actionScript.Actions.Add(new CharacterCommand($"[{characterAction.Parent.Character}] {characterAction.CharacterDialogue}", characterAction.CharacterDialogue, characterAction.Parent.Character, characterAction.CharacterExpression));
                    break;
                case CharacterActionTypes.ShowName:
                    actionScript.Actions.Add(new CharacterCommand($"[{characterAction.Parent.Character} -> NAME_KNOWN] {characterAction.CharacterDialogue}", "", characterAction.Parent.Character, "NAME_KNOWN"));
                    break;
                case CharacterActionTypes.HideName:
                    actionScript.Actions.Add(new CharacterCommand($"[{characterAction.Parent.Character} -> NAME_UNKNOWN] {characterAction.CharacterDialogue}", "", characterAction.Parent.Character, "NAME_UNKNOWN"));
                    break;
                default:
                    break;
            }
            if (characterAction.NextAction[0] == null)
            {
                int currentIndex = characterAction.Parent.Actions.IndexOf(characterAction);
                if (currentIndex < characterAction.Parent.Actions.Count - 1)
                {
                    return characterAction.Parent.Actions[currentIndex + 1];
                }
            }
            return characterAction.NextAction[0];
        }
        private static BaseSerializableAction ParseScriptAction(SerializableGameBook gameBook, SerializableScriptAction scriptAction, SerializableActionScript actionScript)
        {
            switch (scriptAction.ScriptActionType)
            {
                case ScriptActionTypes.ChangeBackground:
                    actionScript.Actions.Add(new SetBackgroundCommand($"SetBackground \"{gameBook.SceneSetting.Images[scriptAction.SceneImageIndex].Name}\"", gameBook.SceneSetting.Images[scriptAction.SceneImageIndex].Name));
                    break;
                default:
                    break;
            }
            return scriptAction.NextAction[0];
        }
#pragma warning restore 0612
#pragma warning restore CS0618 // Type or member is obsolete
    }
}
#endif