using System;
using System.Collections.Generic;

namespace DKP.SaveSystem.Data
{
    /// <summary>
    /// Represents an ActionScript
    /// </summary>
    [Serializable]
    public class SerializableActionScript : BaseSerializableWorldObject
    {
        public static string START_SCRIPT_NAME = "START";
        /// <summary>
        /// User created description
        /// </summary>
        public string Description;
        /// <summary>
        /// Actions the script will execute
        /// </summary>
        public List<BaseCommand> Actions = new List<BaseCommand>();
        [NonSerialized]
        public Action<CommandResponse, string> ErrorCallback;
        /// <summary>
        /// Called when the actions are updated
        /// </summary>
        [field: NonSerialized]
        public event Action OnModified;
        public void Modify()
        {
            OnModified?.Invoke();
        }
    }
}