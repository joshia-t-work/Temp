using DKP.CutsceneSystem;
using DKP.Development.LevelEditor;
using DKP.Singletonmanager;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using static DKP.Development.LevelEditor.CommandTextManipulator;

namespace DKP.SaveSystem.Data
{
    /// <summary>
    /// Represents a command to move the character to the specified location
    /// </summary>
    [Serializable]
    public class MoveCommand : BaseCommand, ILatchable
    {
        public string CharacterName = "";
        public float CharacterPosition;
        public float CharacterSpeed;
        public void SetPosition(string position)
        {
            CharacterPosition = float.Parse(position);
        }
        public void SetSpeed(string speed)
        {
            CharacterSpeed = float.Parse(speed);
        }
        public async override Task<BaseCommand> Execute(SerializableGameBook gamebook, CancellationToken ct)
        {
            await CutsceneEvents.I.InvokeCharacterMoveCommand(ct, CharacterName, CharacterPosition, CharacterSpeed);
            return await base.Execute(gamebook, ct);
        }
        public MoveCommand(string commandString, string characterName, string characterPosition, string characterSpeed) : base(commandString)
        {
            CharacterName = characterName;
            SetPosition(characterPosition);
            SetSpeed(characterSpeed);
        }

        public static CommandSyntax Syntax = new CommandSyntax(new HighlightedText[]{
            new HighlightedText("Move", CodeSyntax.Command),
            new HighlightedText("{Character name}", LintCharacter),
            new HighlightedText("{position}", LintFloat),
            new HighlightedText("{speed multiplier}", LintFloat)
        }, FromString);

        private static MoveCommand FromString(string text, string[] split)
        {
            return new MoveCommand(text, split[1], split[2], split[3]);
        }
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        [NonSerialized]
        Action<CommandResponse, string> callback;
        [NonSerialized]
        SerializableImageContainer latchedCharacter;
        private void OnCharNameChange(string oldName, string newName)
        {
            CommandString = StringManipulation.ReplaceFirst(CommandString, oldName, newName);
            CharacterName = StringManipulation.ReplaceFirst(CharacterName, oldName, newName);
            callback?.Invoke(CommandResponse.Success, GenerateError($"Updated CharacterName from {oldName} to {newName}"));
        }
        private void OnCharDestroy()
        {
            callback?.Invoke(CommandResponse.Fail, GenerateError($"Character {CharacterName} destroyed"));
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
            }
            callback = null;
        }
#endif
    }
}