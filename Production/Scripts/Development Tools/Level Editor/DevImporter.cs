#if DEVELOPMENT_BUILD || UNITY_EDITOR
using DKP.Development.LevelEditor.Data;
using DKP.Input;
using DKP.SaveSystem.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace DKP.Development.LevelEditor
{
    /// <summary>
    /// No longer usable
    /// </summary>
    [Obsolete]
    public class DevImporter : MonoBehaviour
    {
        //string importText;

        //SerializableCharacter currentCharacter = null;
        //BaseSerializableScript currentScript = null;
        //SerializableImageText currentBackgrond = null;
        //SerializableImageText currentExpression = null;
        //BaseSerializableAction currentExecutedAction = null;
        //SerializableCharacter prevCharacter = null;
        //BaseSerializableScript prevScript = null;

        //Dictionary<string, SerializableCharacter> characters = new Dictionary<string, SerializableCharacter>();
        //List<SerializableCharacter> newCharacters = new List<SerializableCharacter>();
        //List<SerializableScript> scripts = new List<SerializableScript>();
        //List<SerializableCharacterScript> characterScripts = new List<SerializableCharacterScript>();
        //List<SerializableTrigger> triggers = new List<SerializableTrigger>();
        //HashSet<SerializableCharacter> hiddenCharacter = new HashSet<SerializableCharacter>();

        //SerializableVector3 spawnPos;

        //ImportSetting setting;

        //public void SetString(string text)
        //{
        //    importText = text;
        //}
        //public void Import()
        //{
        //    setting = new ImportSetting();
        //    Queue<IDevDataContainer> _toLink = new Queue<IDevDataContainer>();

        //    Vector2 pos = (Vector2)DevLevelEditorControl.rightClickPos;
        //    spawnPos = new SerializableVector3(pos.x, pos.y, 0f);
        //    characters.Clear();
        //    newCharacters.Clear();
        //    scripts.Clear();
        //    characterScripts.Clear();
        //    triggers.Clear();
        //    hiddenCharacter.Clear();

        //    for (int i = 0; i < DevEditorDataContainer.GameBookData.Characters.Count; i++)
        //    {
        //        characters.Add(DevEditorDataContainer.GameBookData.Characters[i].Name, DevEditorDataContainer.GameBookData.Characters[i]);
        //    }

        //    currentCharacter = null;
        //    currentScript = null;
        //    currentExpression = null;
        //    currentExecutedAction = null;
        //    prevCharacter = null;
        //    prevScript = null;
        //    currentBackgrond = null;

        //    string[] lines = importText.Split(System.Environment.NewLine.ToCharArray());
        //    //if (lines[0].StartsWith("[ImportSetting]"))
        //    //{
        //    //    string firstLine = lines[0];
        //    //    lines = lines.Skip(1).ToArray();
        //    //    void ImportSettingIterator(string key, string value)
        //    //    {
        //    //        switch (key)
        //    //        {
        //    //            default:
        //    //                break;
        //    //        }
        //    //    }
        //    //    IterateKeyValuePair(firstLine, ImportSettingIterator);
        //    //}

        //    foreach (string line in lines)
        //    {
        //        if (!string.IsNullOrEmpty(line) && !line.StartsWith("//"))
        //        {
        //            if (line.StartsWith("["))
        //            {
        //                void CharVisualIterator(string key, string value)
        //                {
        //                    //Debug.Log($"SET {key} to {value}");
        //                    switch (key)
        //                    {
        //                        case "TYPE":
        //                            switch (value)
        //                            {
        //                                case "VisualNovel":
        //                                    setting.Type = ImportSetting.Types.VisualNovel;
        //                                    break;
        //                                case "InGame":
        //                                    setting.Type = ImportSetting.Types.InGame;
        //                                    break;
        //                                default:
        //                                    break;
        //                            }
        //                            break;
        //                        case "TRIGGER":
        //                            SerializableTrigger newTrigger = DevEditorDataContainer.AddTrigger();
        //                            newTrigger.Label = value;
        //                            newTrigger.WorldPosition = getNextWorldPos(110f);
        //                            currentExecutedAction = newTrigger;
        //                            triggers.Add(newTrigger);
        //                            break;
        //                        case "SETTING":
        //                            List<SerializableImageText> images = DevEditorDataContainer.GameBookData.SceneSetting.Images;
        //                            int imageIndex = -1;
        //                            for (int i = 0; i < images.Count; i++)
        //                            {
        //                                if (images[i].Label == value)
        //                                {
        //                                    imageIndex = i;
        //                                }
        //                            }
        //                            if (imageIndex < 0)
        //                            {
        //                                SerializableImageText image = new SerializableImageText();
        //                                image.Owner = DevEditorDataContainer.GameBookData.SceneSetting;
        //                                image.Label = value;
        //                                DevEditorDataContainer.GameBookData.SceneSetting.Images.Add(image);
        //                                imageIndex = DevEditorDataContainer.GameBookData.SceneSetting.Images.Count - 1;
        //                            }
        //                            if (currentBackgrond == null || currentBackgrond.Label != value)
        //                            {
        //                                currentBackgrond = DevEditorDataContainer.GameBookData.SceneSetting.Images[imageIndex];
        //                                // TODO: [SETTING] and alignment
        //                                //AddChangeBackgroundAction(currentBackgrond);
        //                            }
        //                            ChangeSceneAction(imageIndex);
        //                            break;
        //                        case "NAME":
        //                            //if (value == "???")
        //                            //{
        //                            //    hideNextName = true;
        //                            //}
        //                            break;
        //                        case "CHAR":
        //                            string[] characterData = value.Split('_');
        //                            if (characterData[0] == "null")
        //                            {
        //                                characterData = "narrator".Split('_');
        //                            }
        //                            if (currentCharacter != null)
        //                            {
        //                                if (prevCharacter != currentCharacter)
        //                                {
        //                                    prevCharacter = currentCharacter;
        //                                }
        //                            }
        //                            if (!characters.TryGetValue(characterData[0], out currentCharacter))
        //                            {
        //                                currentCharacter = DevEditorDataContainer.AddCharacter();
        //                                currentCharacter.Name = characterData[0];
        //                                currentCharacter.WorldPosition = getNextWorldPos(210f);
        //                                characters.Add(characterData[0], currentCharacter);
        //                                newCharacters.Add(currentCharacter);
        //                            }
        //                            if (characterData.Length > 1)
        //                            {
        //                                List<SerializableImageText> expressions = currentCharacter.Images;
        //                                int expressionIndex = -1;
        //                                for (int i = 0; i < expressions.Count; i++)
        //                                {
        //                                    if (expressions[i].Label == characterData[1])
        //                                    {
        //                                        expressionIndex = i;
        //                                    }
        //                                }
        //                                if (expressionIndex < 0)
        //                                {
        //                                    SerializableImageText expression = new SerializableImageText();
        //                                    expression.Owner = currentCharacter;
        //                                    expression.Label = characterData[1];
        //                                    currentCharacter.Images.Add(expression);
        //                                    expressionIndex = currentCharacter.Images.Count - 1;
        //                                }
        //                                if (currentExpression == null || currentExpression.Label != characterData[1])
        //                                {
        //                                    currentExpression = currentCharacter.Images[expressionIndex];
        //                                    AddExpressionAction(currentExpression);
        //                                }
        //                            }
        //                            break;
        //                        default:
        //                            break;
        //                    }
        //                }
        //                IterateKeyValuePair(line, CharVisualIterator);
        //            }
        //            string dialogueString = Regex.Replace(line, @" ?\[.*?\]", string.Empty);
        //            if (dialogueString != "")
        //            {
        //                AddDialogueAction(dialogueString);
        //            }
        //        }
        //    }

        //    foreach (SerializableTrigger trigger in triggers)
        //    {
        //        _toLink.Enqueue(DevEditorDataContainer.CreateComponent<SerializableTrigger, DevTrigger>(trigger));
        //    }

        //    foreach (SerializableCharacter character in newCharacters)
        //    {
        //        _toLink.Enqueue(DevEditorDataContainer.CreateComponent<SerializableCharacter, DevCharacter>(character));
        //    }

        //    foreach (SerializableCharacterScript characterScript in characterScripts)
        //    {
        //        _toLink.Enqueue(DevEditorDataContainer.CreateComponent<SerializableCharacterScript, DevCharacterScript>(characterScript));
        //    }

        //    foreach (SerializableScript script in scripts)
        //    {
        //        _toLink.Enqueue(DevEditorDataContainer.CreateComponent<SerializableScript, DevScript>(script));
        //    }

        //    while (_toLink.Count > 0)
        //    {
        //        IDevDataContainer data = _toLink.Dequeue();
        //        data.Link();
        //    }

        //    DevEditorDataContainer.GetComponent<DevImageContainer>(DevEditorDataContainer.GameBookData.SceneSetting).SetData(DevEditorDataContainer.GameBookData.SceneSetting);
        //    DevEditorDataContainer.GetComponent<DevMainThread>(DevEditorDataContainer.GameBookData.MainThread).SetData(DevEditorDataContainer.GameBookData.MainThread);
        //}

        //private SerializableVector3 getNextWorldPos(float shift)
        //{
        //    SerializableVector3 curPos = spawnPos;
        //    spawnPos.X += shift;
        //    return curPos;
        //}

        //private void IterateKeyValuePair(string text, Action<string, string> iterator)
        //{
        //    foreach (Match m in Regex.Matches(text, @"\[(.*?)\]"))
        //    {
        //        string[] valueSet = m.Groups[1].Value.Split('=');
        //        if (valueSet.Length == 2)
        //        {
        //            iterator.Invoke(valueSet[0], valueSet[1]);
        //        }
        //    }
        //}

        //private SerializableCharacterScriptAction BaseCreateCharAction()
        //{
        //    bool createNew = false;
        //    SerializableCharacterScript charScript = null;
        //    if (currentScript is SerializableCharacterScript)
        //    {
        //        charScript = (SerializableCharacterScript)currentScript;
        //        if (charScript.Character != currentCharacter.Name)
        //        {
        //            createNew = true;
        //            if (prevScript is SerializableCharacterScript)
        //            {
        //                SerializableCharacterScript prevCharScript = (SerializableCharacterScript)prevScript;
        //                if ((prevCharScript != null) && (prevCharScript.Character == currentCharacter.Name))
        //                {
        //                    createNew = false;
        //                    BaseSerializableScript temp = currentScript;
        //                    currentScript = prevScript;
        //                    prevScript = temp;
        //                    charScript = (SerializableCharacterScript)currentScript;
        //                }
        //            }
        //        }
        //    } else
        //    {
        //        createNew = true;
        //    }
        //    if (createNew)
        //    {
        //        prevScript = currentScript;
        //        charScript = DevEditorDataContainer.AddCharacterScript();
        //        charScript.Character = currentCharacter.Name;
        //        charScript.CharacterIndex = DevEditorDataContainer.GameBookData.Characters.IndexOf(currentCharacter);
        //        currentScript = charScript;
        //        currentScript.WorldPosition = getNextWorldPos(210f);
        //        characterScripts.Add(charScript);
        //    }

        //    SerializableCharacterScriptAction characterAction = new SerializableCharacterScriptAction();
        //    characterAction.Parent = charScript;
        //    return characterAction;
        //}
        //private void BaseCharActionCreated(SerializableCharacterScriptAction action)
        //{
        //    ((SerializableCharacterScript)currentScript).Actions.Add(action);
        //    if (currentExecutedAction is SerializableCharacterScriptAction)
        //    {
        //        if (((SerializableCharacterScriptAction)currentExecutedAction).Parent != currentScript)
        //        {
        //            currentExecutedAction.NextAction[0] = action;
        //        }
        //    } else
        //    {
        //        currentExecutedAction.NextAction[0] = action;
        //    }
        //    currentExecutedAction = action;
        //}
        //private void ShowAction()
        //{
        //    SerializableCharacterScriptAction action = BaseCreateCharAction();
        //    action.CharacterActionType = CharacterActionTypes.Show;
        //    BaseCharActionCreated(action);
        //}
        //private void HideAction()
        //{
        //    SerializableCharacterScriptAction action = BaseCreateCharAction();
        //    action.CharacterActionType = CharacterActionTypes.Hide;
        //    BaseCharActionCreated(action);
        //}
        //private void AddExpressionAction(SerializableImageText expression)
        //{
        //    SerializableCharacterScriptAction action = BaseCreateCharAction();
        //    action.CharacterActionType = CharacterActionTypes.SetExpression;
        //    action.CharacterExpression = expression.Label;
        //    BaseCharActionCreated(action);
        //}
        //private void AddDialogueAction(string text)
        //{
        //    SerializableCharacterScriptAction action = BaseCreateCharAction();
        //    action.CharacterActionType = CharacterActionTypes.Dialogue;
        //    action.CharacterDialogue = text;
        //    BaseCharActionCreated(action);
        //}

        //private SerializableScriptAction BaseCreateScriptAction()
        //{
        //    bool createNew = false;
        //    SerializableScript script = null;
        //    if (currentScript is SerializableScript)
        //    {
        //        script = (SerializableScript)currentScript;
        //    }
        //    if (script == null)
        //    {
        //        createNew = true;
        //    }
        //    if (createNew)
        //    {
        //        prevScript = currentScript;
        //        script = DevEditorDataContainer.AddScript();
        //        currentScript = script;
        //        currentScript.WorldPosition = getNextWorldPos(210f);
        //        scripts.Add(script);
        //    }

        //    SerializableScriptAction scriptAction = new SerializableScriptAction();
        //    scriptAction.Parent = script;
        //    return scriptAction;
        //}
        //private void BaseScriptActionCreated(SerializableScriptAction action)
        //{
        //    ((SerializableScript)currentScript).Actions.Add(action);
        //    if (currentExecutedAction is SerializableScriptAction)
        //    {
        //        if (((SerializableScriptAction)currentExecutedAction).Parent != currentScript)
        //        {
        //            currentExecutedAction.NextAction[0] = action;
        //        }
        //    }
        //    else
        //    {
        //        currentExecutedAction.NextAction[0] = action;
        //    }
        //    currentExecutedAction = action;
        //}
        //private void ChangeSceneAction(int sceneIndex)
        //{
        //    SerializableScriptAction action = BaseCreateScriptAction();
        //    action.SceneImageIndex = sceneIndex;
        //    BaseScriptActionCreated(action);
        //}
    }

    public class ImportSetting
    {
        public Types Type;
        public enum Types
        {
            VisualNovel,
            InGame,
        }
        public ImportSetting()
        {
            Type = Types.VisualNovel;
        }
    }
}
#endif