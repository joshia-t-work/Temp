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
    /// Represents a command that is an objective
    /// </summary>
    public interface IObjective
    {
        public string ObjectiveName { get; }
    }
}