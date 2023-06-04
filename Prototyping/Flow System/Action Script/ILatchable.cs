#if DEVELOPMENT_BUILD || UNITY_EDITOR
using DKP.CutsceneSystem;
using DKP.Development.LevelEditor;
#endif
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DKP.SaveSystem.Data
{
    /// <summary>
    /// Represents a latchable that updates changes from ActionScript
    /// </summary>
    /// <remarks>UNITY EDITOR</remarks>
    public interface ILatchable
    {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        /// <summary>
        /// Latches onto the ActionScript and update changes
        /// </summary>
        /// <param name="callback"></param>
        public void Latch(Action<CommandResponse, string> callback = null);
        /// <summary>
        /// Unlatches from the ActionScript
        /// </summary>
        public void Unlatch();
#endif
    }
}