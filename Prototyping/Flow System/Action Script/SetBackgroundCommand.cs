using DKP.CutsceneSystem;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static DKP.Development.LevelEditor.CommandTextManipulator;

namespace DKP.SaveSystem.Data
{
    /// <summary>
    /// Represents a command to set the background of the image
    /// </summary>
    [Serializable]
    public class SetBackgroundCommand : SetCommand
    {
        public async override Task<BaseCommand> Execute(SerializableGameBook gamebook, CancellationToken ct)
        {
            await CutsceneEvents.I.InvokeSetBackground(ct, VariableValue);
            return await base.Execute(gamebook, ct);
        }
        public SetBackgroundCommand(string commandString, string variableValue) : base(commandString, "BACKGROUND", variableValue)
        {
            VariableValue = variableValue;
        }

        public static new CommandSyntax Syntax = new CommandSyntax(new HighlightedText[]{
            new HighlightedText("SetBackground", CodeSyntax.Command),
            new HighlightedText("{value}", LintBackground)
        }, FromString);

        private static SetBackgroundCommand FromString(string text, string[] split)
        {
            return new SetBackgroundCommand(text, split[1]);
        }
    }
}