#if DEVELOPMENT_BUILD || UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKP.Development.LevelEditor
{
    [CreateAssetMenu(fileName = "Update Log", menuName = "SO/Update Log")]
    public class DevUpdateLog : ScriptableObject
    {
        [SerializeField]
        public string CurrentVersion;

        [SerializeField]
        public VersionLog[] Log;
    }

    [Serializable]
    public class VersionLog
    {
        public string Version;
        [TextArea]
        public string Desc;
    }
}
#endif