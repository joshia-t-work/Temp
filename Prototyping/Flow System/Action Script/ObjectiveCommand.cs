using DKP.CutsceneSystem;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DKP.SaveSystem.Data
{
    /// <summary>
    /// Represents a command to start a generic objective and wait until it is finished
    /// </summary>
    [Serializable]
    public class ObjectiveCommand : BaseCommand, IObjective
    {
        public virtual string ObjectiveName => "Default Objective";

        public override async Task<BaseCommand> Execute(SerializableGameBook gamebook, CancellationToken ct)
        {
            await CutsceneEvents.I.InvokeStartObjective(ct, this);
            await CutsceneEvents.I.InvokeStopObjective(ct);
            return await base.Execute(gamebook, ct);
        }
        public ObjectiveCommand(string commandString) : base(commandString) { }
        public virtual Task AwaitObjectiveCompletion(CancellationToken ct, Action<ObjectiveParams> progressCallback) { return Task.CompletedTask; }
    }

    public class ObjectiveParams { }
}