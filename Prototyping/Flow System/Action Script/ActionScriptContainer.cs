using System;
using System.Collections.Generic;

namespace DKP.SaveSystem.Data
{
    /// <summary>
    /// Represents an List of ActionScript
    /// </summary>
    [Serializable]
    public class ActionScriptContainer
    {
        public List<SerializableActionScript> ActionScripts = new List<SerializableActionScript>();
        public SerializableActionScript this[int index]
        {
            get
            {
                return ActionScripts[index];
            }
            set
            {
                ActionScripts[index] = value;
            }
        }
        public SerializableActionScript this[string index]
        {
            get
            {
                return ActionScripts.Find(x => x.ObjectName == index);
            }
            set
            {
                ActionScripts[ActionScripts.FindIndex(x => x.ObjectName == index)] = value;
            }
        }

        public int Count => ActionScripts.Count;

        public SerializableActionScript AddNew()
        {
            SerializableActionScript newActionScript = new SerializableActionScript();
            ActionScripts.Add(newActionScript);
            return newActionScript;
        }

        public ActionScriptContainer()
        {
            
        }
    }
}