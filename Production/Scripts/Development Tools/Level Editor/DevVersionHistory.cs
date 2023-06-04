#if DEVELOPMENT_BUILD || UNITY_EDITOR
using DKP.Development.LevelEditor.Data;
using DKP.Input;
using DKP.SaveSystem.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace DKP.Development.LevelEditor
{
    [Obsolete("Removed in 2.0")]
    public class DevVersionHistory : MonoBehaviour
    {
        [SerializeField]
        public string CurrentVersion;

        [SerializeField]
        public VersionLog[] Log;
    }
}
#endif