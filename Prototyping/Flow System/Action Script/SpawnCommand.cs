using DKP.CutsceneSystem;
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
    /// Represents a command to add a unit to the spawning queue
    /// </summary>
    [Serializable]
    public class SpawnCommand : BaseCommand
    {
        public string Unit;
        public string SpawnPosition;
        public string Delay;
        public string Count;
        public string Interval;
        public override async Task<BaseCommand> Execute(SerializableGameBook gamebook, CancellationToken ct)
        {
            await CutsceneEvents.I.InvokeSpawn(ct, this);
            return await base.Execute(gamebook, ct);
        }
        public SpawnCommand(string commandString, string count, string unit, string spawnPosition, string interval, string delay) : base(commandString)
        {
            Unit = unit;
            SpawnPosition = spawnPosition;
            Delay = delay;
            Count = count;
            Interval = interval;
        }

        public static CommandSyntax Syntax = new CommandSyntax(new HighlightedText[]{
            new HighlightedText("Spawn", CodeSyntax.Command),
            new HighlightedText("{count}", LintInt),
            new HighlightedText("{unit}", LintUnit),
            new HighlightedText("at", CodeSyntax.Default),
            new HighlightedText("{position}", LintSpawner),
            new HighlightedText("every", CodeSyntax.Default),
            new HighlightedText("{interval}", LintFloat),
            new HighlightedText("wait", CodeSyntax.Default),
            new HighlightedText("{delay}", LintFloat)
        }, FromString);

        private static SpawnCommand FromString(string text, string[] split)
        {
            return new SpawnCommand(text, split[1], split[2], split[4], split[6], split[8]);
        }
    }
}