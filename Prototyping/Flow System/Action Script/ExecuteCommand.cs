#if DEVELOPMENT_BUILD || UNITY_EDITOR
using DKP.CutsceneSystem;
using DKP.Development.LevelEditor;
#endif
using System;
using System.Threading;
using System.Threading.Tasks;
using static DKP.Development.LevelEditor.CommandTextManipulator;

namespace DKP.SaveSystem.Data
{
    /// <summary>
    /// Represents a command to run another script
    /// </summary>
    [Serializable]
    public class ExecuteCommand : BaseCommand, ILatchable
    {
        public string ExecutedCommand = "";
        public async override Task<BaseCommand> Execute(SerializableGameBook gamebook, CancellationToken ct)
        {
            await CutsceneEvents.I.InvokeExecute(ct, ExecutedCommand);
            return await base.Execute(gamebook, ct);
        }
        public ExecuteCommand(string commandString, string executedCommand) : base(commandString)
        {
            ExecutedCommand = executedCommand;
        }

        public static CommandSyntax Syntax = new CommandSyntax(new HighlightedText[]{
            new HighlightedText("Execute", CodeSyntax.Command),
            new HighlightedText("{ActionScript name}", LintExecute)
        }, FromString);

        private static ExecuteCommand FromString(string text, string[] split)
        {
            return new ExecuteCommand(text, split[1]);
        }
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        [NonSerialized]
        Action<CommandResponse, string> callback;
        [NonSerialized]
        SerializableActionScript latchedScript;
        private void OnNameChange(string oldName, string newName)
        {
            CommandString = StringManipulation.ReplaceFirst(CommandString, oldName, newName);
            ExecutedCommand = StringManipulation.ReplaceFirst(ExecutedCommand, oldName, newName);
            callback?.Invoke(CommandResponse.Success, GenerateError($"Updated command {oldName} to {newName}"));
        }
        private void OnDestroy()
        {
            callback?.Invoke(CommandResponse.Fail, GenerateError($"ActionScript {ExecutedCommand} was destroyed"));
        }

        /// <summary>
        /// Latches onto the ActionScript and update changes
        /// </summary>
        /// <param name="callback"></param>
        public void Latch(Action<CommandResponse, string> callback = null)
        {
            this.callback = callback;
            latchedScript = DevEditorDataContainer.GameBookData.ActionScripts[ExecutedCommand];
            if (latchedScript != null)
            {
                latchedScript.OnNameChanged += OnNameChange;
                latchedScript.OnDestroy += OnDestroy;
            }
            else
            {
                callback?.Invoke(CommandResponse.Fail, GenerateError($"ActionScript {ExecutedCommand} not found"));
            }
        }
        /// <summary>
        /// Unlatches from the ActionScript
        /// </summary>
        public void Unlatch()
        {
            if (latchedScript != null)
            {
                latchedScript.OnNameChanged -= OnNameChange;
                latchedScript.OnDestroy -= OnDestroy;
            }
            callback = null;
        }
#endif
    }
}