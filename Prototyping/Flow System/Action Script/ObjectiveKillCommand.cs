using DKP.CutsceneSystem;
using DKP.Development.LevelEditor;
using DKP.FlowSystem;
using DKP.UnitSystem;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using static DKP.Development.LevelEditor.CommandTextManipulator;

namespace DKP.SaveSystem.Data
{
    /// <summary>
    /// Represents a command to start a kill objective and wait until it is finished
    /// </summary>
    [Serializable]
    public class ObjectiveKillCommand : ObjectiveCommand
    {
        public override string ObjectiveName => "Kill Objective";
        public string Count;
        public string Unit;
        public ObjectiveKillCommand(string commandString, string count, string unit) : base(commandString)
        {
            Count = count;
            Unit = unit;
        }

        public override async Task AwaitObjectiveCompletion(CancellationToken ct, Action<ObjectiveParams> progressCallback)
        {
            TaskCompletionSource<bool> objectiveTask = new TaskCompletionSource<bool>(false);
            ObjectiveKillParams objective = new ObjectiveKillParams();
            objective.Killed = 0;
            objective.Count = int.Parse(Count);
            objective.Unit = Unit;
            progressCallback.Invoke(objective);
            Task UnitKilledCallback(CancellationToken ct, Unit unit)
            {
                if (unit.gameObject.name == Unit)
                {
                    objective.Killed += 1;
                    progressCallback.Invoke(objective);
                    if (objective.Count == objective.Killed)
                    {
                        objectiveTask.TrySetResult(true);
                    }
                }
                return Task.CompletedTask;
            }
            CutsceneEvents.I.UnitDied += UnitKilledCallback;
            try
            {
                await Task.Run(async () =>
                {
                    await objectiveTask.Task;
                }, ct);
            }
            catch (TaskCanceledException) { }
            finally
            {
                CutsceneEvents.I.UnitDied -= UnitKilledCallback;
            }
            await base.AwaitObjectiveCompletion(ct, progressCallback);
        }

        public static CommandSyntax Syntax = new CommandSyntax(new HighlightedText[]{
            new HighlightedText("Objective", CodeSyntax.Command),
            new HighlightedText("kill", CodeSyntax.Default),
            new HighlightedText("{count}", LintInt),
            new HighlightedText("{unit}", LintUnit)
        }, FromString);

        private static ObjectiveKillCommand FromString(string text, string[] split)
        {
            return new ObjectiveKillCommand(text, split[2], split[3]);
        }
    }

    public class ObjectiveKillParams : ObjectiveParams
    {
        public int Killed;
        public int Count;
        public string Unit;
    }
}