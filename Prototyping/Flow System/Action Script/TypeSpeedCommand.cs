using DKP.CutsceneSystem;
using DKP.Development.LevelEditor;
using DKP.FlowSystem;
using System;
using System.Threading;
using System.Threading.Tasks;
using static DKP.Development.LevelEditor.CommandTextManipulator;

namespace DKP.SaveSystem.Data
{
    /// <summary>
    /// Represents a command to set the delay between dialogue typing
    /// </summary>
    [Serializable]
    public class TypeSpeedCommand : DelayCommand
    {
        public override Task<BaseCommand> Execute(SerializableGameBook gamebook, CancellationToken ct)
        {
            if (CommandTextManipulator.IsVariable(Delay))
            {
                Flow.FlowTypingDelay = int.Parse(Flow.GetVariable(Delay.Substring(1, Delay.Length - 2)));
            }
            else
            {
                Flow.FlowTypingDelay = int.Parse(Delay);
            }
            return Task.FromResult<BaseCommand>(null);
        }
        public TypeSpeedCommand(string commandString, string delay) : base(commandString, delay)
        {
        }

        public static new CommandSyntax Syntax = new CommandSyntax(new HighlightedText[]{
            new HighlightedText("TypeSpeed", CodeSyntax.Command),
            new HighlightedText("{milliseconds}", LintInt)
        }, FromString, true);

        private static TypeSpeedCommand FromString(string text, string[] split)
        {
            return new TypeSpeedCommand(text, split[1]);
        }
    }
}