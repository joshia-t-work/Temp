using DKP.FlowSystem;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static DKP.Development.LevelEditor.CommandTextManipulator;

namespace DKP.SaveSystem.Data
{
    /// <summary>
    /// Represents a command to set a variable to a specific value
    /// </summary>
    [Serializable]
    public class SetCommand : BaseCommand
    {
        public string VariableName = "";
        public string VariableValue = "";
        public override Task<BaseCommand> Execute(SerializableGameBook gamebook, CancellationToken ct)
        {
            Flow.SetVariable(VariableName, VariableValue);
            return base.Execute(gamebook, ct);
        }
        public SetCommand(string commandString, string variableName, string variableValue) : base(commandString)
        {
            VariableName = variableName;
            VariableValue = variableValue;
        }

        public static CommandSyntax Syntax = new CommandSyntax(new HighlightedText[]{
            new HighlightedText("Set", CodeSyntax.Command),
            new HighlightedText("{variable}", CodeSyntax.Variable),
            new HighlightedText("to", CodeSyntax.Default),
            new HighlightedText("{value}", CodeSyntax.VariableValue)
        }, FromString);

        private static SetCommand FromString(string text, string[] split)
        {
            return new SetCommand(text, split[1], split[3]);
        }
    }
}