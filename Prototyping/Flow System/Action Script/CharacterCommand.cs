#if DEVELOPMENT_BUILD || UNITY_EDITOR
using DKP.CutsceneSystem;
using DKP.Development.LevelEditor;
#endif
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DKP.SaveSystem.Data
{
    /// <summary>
    /// Represents a command to set the character to speak and change its expression image, SHOW or HIDE as expression to show/hide the character
    /// </summary>
    [Serializable]
    public class CharacterCommand : SpeakCommand, ILatchable
    {
        public string CharacterName = "";
        public string CharacterExpression = "";
        public List<string> ExpressionConst = new List<string> { "SHOW", "HIDE", "NAME_KNOWN", "NAME_UNKNOWN" };
        public async override Task<BaseCommand> Execute(SerializableGameBook gamebook, CancellationToken ct)
        {
            switch (CharacterExpression)
            {
                case "SHOW":
                    await CutsceneEvents.I.InvokeShowCharacter(ct, CharacterName);
                    break;
                case "HIDE":
                    await CutsceneEvents.I.InvokeHideCharacter(ct, CharacterName);
                    break;
                case "NAME_KNOWN":
                    await CutsceneEvents.I.InvokeSetCharacterName(ct, CharacterName, "???");
                    break;
                case "NAME_UNKNOWN":
                    await CutsceneEvents.I.InvokeSetCharacterName(ct, CharacterName, CharacterName);
                    break;
                default:
                    if (CharacterExpression != "")
                    {
                        await CutsceneEvents.I.InvokeSetExpression(ct, CharacterName, CharacterExpression);
                    }
                    break;
            }
            await CutsceneEvents.I.InvokeDialogueSetCharacter(ct, CharacterName);
            return await base.Execute(gamebook, ct);
        }
        public CharacterCommand(string commandString, string dialogueString, string characterName, string characterExpression) : base(commandString, dialogueString)
        {
            CharacterName = characterName;
            CharacterExpression = characterExpression;
        }
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        [NonSerialized]
        Action<CommandResponse, string> callback;
        [NonSerialized]
        SerializableImageContainer latchedCharacter;
        [NonSerialized]
        SerializableImageText latchedExpression;
        private void OnCharNameChange(string oldName, string newName)
        {
            CommandString = StringManipulation.ReplaceFirst(CommandString, oldName, newName);
            CharacterName = StringManipulation.ReplaceFirst(CharacterName, oldName, newName);
            callback?.Invoke(CommandResponse.Success, GenerateError($"Updated CharacterName from {oldName} to {newName}"));
        }
        private void OnExpressionNameChange(string oldName, string newName)
        {
            CommandString = StringManipulation.ReplaceFirst(CommandString, oldName, newName);
            CharacterExpression = StringManipulation.ReplaceFirst(CharacterExpression, oldName, newName);
            callback?.Invoke(CommandResponse.Success, GenerateError($"Updated CharacterExpression from {oldName} to {newName}"));
        }
        private void OnCharDestroy()
        {
            callback?.Invoke(CommandResponse.Fail, GenerateError($"Character {CharacterName} destroyed"));
        }
        private void OnExpressionDestroy()
        {
            callback?.Invoke(CommandResponse.Fail, GenerateError($"Expression {CharacterExpression} destroyed on {CharacterName}"));
        }

        /// <summary>
        /// Latches onto the ActionScript and update changes
        /// </summary>
        /// <param name="callback"></param>
        public void Latch(Action<CommandResponse, string> callback = null)
        {
            this.callback = callback;
            latchedCharacter = DevEditorDataContainer.GameBookData.Images[CharacterName];
            if (latchedCharacter != null)
            {
                latchedCharacter.OnNameChanged += OnCharNameChange;
                latchedCharacter.OnDestroy += OnCharDestroy;
                int expressionIndex = -1;
                string expressionCheckString = CharacterExpression.Replace("\"", "").Trim();
                for (int i = 0; i < latchedCharacter.Images.Count; i++)
                {
                    if (latchedCharacter.Images[i].Name == expressionCheckString)
                    {
                        expressionIndex = i;
                        break;
                    }
                }
                if (CharacterExpression != "")
                {
                    if (!ExpressionConst.Contains(expressionCheckString))
                    {
                        if (expressionIndex > -1)
                        {
                            latchedExpression = latchedCharacter.Images[expressionIndex];
                            latchedExpression.OnNameChanged += OnExpressionNameChange;
                            latchedExpression.OnDestroy += OnExpressionDestroy;
                        }
                        else
                        {
                            callback?.Invoke(CommandResponse.Fail, GenerateError($"Expression {CharacterExpression} not found on {CharacterName}"));
                        }
                    }
                }
            }
            else
            {
                callback?.Invoke(CommandResponse.Fail, GenerateError($"Character {CharacterName} not found"));
            }
        }
        /// <summary>
        /// Unlatches from the ActionScript
        /// </summary>
        public void Unlatch()
        {
            if (latchedCharacter != null)
            {
                latchedCharacter.OnNameChanged -= OnCharNameChange;
                latchedCharacter.OnDestroy -= OnCharDestroy;
                if (latchedExpression != null)
                {
                    latchedExpression.OnNameChanged -= OnExpressionNameChange;
                    latchedExpression.OnDestroy -= OnExpressionDestroy;
                }
            }
            callback = null;
        }
#endif
    }
}