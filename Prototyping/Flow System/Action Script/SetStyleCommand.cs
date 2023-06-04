using DKP.CutsceneSystem;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static DKP.Development.LevelEditor.CommandTextManipulator;

namespace DKP.SaveSystem.Data
{
    /// <summary>
    /// Represents a command to set the narrative style
    /// </summary>
    [Serializable]
    public class SetStyleCommand : SetCommand
    {
        public static readonly string[] STYLES = new string[] { "VISUALNOVEL", "INGAME" };
        public async override Task<BaseCommand> Execute(SerializableGameBook gamebook, CancellationToken ct)
        {
            switch (VariableValue)
            {
                case "VISUALNOVEL":
                    await CutsceneEvents.I.InvokeSetVNStyle(ct);
                    break;
                case "INGAME":
                    await CutsceneEvents.I.InvokeSetInGameStyle(ct);
                    break;
                default:
                    break;
            }
            return await base.Execute(gamebook, ct);
        }
        public SetStyleCommand(string commandString, string variableValue) : base(commandString, "STYLE", variableValue)
        {
            VariableValue = variableValue;
        }

        public static new CommandSyntax Syntax = new CommandSyntax(new HighlightedText[]{
            new HighlightedText("SetStyle", CodeSyntax.Command),
            new HighlightedText($"{{{string.Join("/", SetStyleCommand.STYLES)}}}", LintStyle)
        }, FromString);

        private static SetStyleCommand FromString(string text, string[] split)
        {
            return new SetStyleCommand(text, split[1]);
        }
    }
}