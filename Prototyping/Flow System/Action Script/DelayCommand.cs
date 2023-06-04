using DKP.Development.LevelEditor;
using DKP.FlowSystem;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using static DKP.Development.LevelEditor.CommandTextManipulator;

namespace DKP.SaveSystem.Data
{
    /// <summary>
    /// Represents a command to wait for a set amount of time
    /// </summary>
    [Serializable]
    public class DelayCommand : BaseCommand
    {
        public string Delay;
        public override async Task<BaseCommand> Execute(SerializableGameBook gamebook, CancellationToken ct)
        {
            if (CommandTextManipulator.IsVariable(Delay))
            {
                await Task.Delay(int.Parse(Flow.GetVariable(Delay.Substring(1, Delay.Length - 2))), ct);
            } else
            {
                await Task.Delay(int.Parse(Delay), ct);
            }
            return await base.Execute(gamebook, ct);
        }
        public DelayCommand(string commandString, string delay) : base(commandString)
        {
            Delay = delay;
        }

        public static CommandSyntax Syntax = new CommandSyntax(new HighlightedText[]{
            new HighlightedText("Delay", CodeSyntax.Command),
            new HighlightedText("{milliseconds}", LintInt),
        }, FromString);

        private static DelayCommand FromString(string text, string[] split)
        {
            return new DelayCommand(text, split[1]);
        }
    }
}