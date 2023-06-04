using DKP.Development.LevelEditor;
using DKP.FlowSystem;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace DKP.SaveSystem.Data
{
    /// <summary>
    /// Represents a command that does nothing
    /// </summary>
    [Serializable]
    public class CommentCommand : BaseCommand
    {
        public override async Task<BaseCommand> Execute(SerializableGameBook gamebook, CancellationToken ct)
        {
            return await base.Execute(gamebook, ct);
        }
        public CommentCommand(string commandString) : base(commandString) { }
    }
}