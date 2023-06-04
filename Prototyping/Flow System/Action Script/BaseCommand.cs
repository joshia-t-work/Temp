using DKP.Development.LevelEditor;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DKP.SaveSystem.Data
{
    /// <summary>
    /// Represents an BaseCommand, actually does nothing on its own
    /// </summary>
    [Serializable]
    public class BaseCommand
    {
        public string CommandString;
        public virtual Task<BaseCommand> Execute(SerializableGameBook gamebook, CancellationToken ct)
        {
            return Task.FromResult<BaseCommand>(null);
        }
        public BaseCommand(string commandString)
        {
            CommandString = commandString;
        }

        protected string GenerateError(string error)
        {
            if (DevActionScriptEditor.ActionScript != null)
            {
                int scriptIndex = DevActionScriptEditor.ActionScript.Actions.IndexOf(this);
                return $"{DevActionScriptEditor.ActionScript.ObjectName} (Ln {scriptIndex + 1}): {error}";
            } else
            {
                return error;
            }
        }
    }
    
    public enum CommandResponse
    {
        Success,
        Fail
    }
}