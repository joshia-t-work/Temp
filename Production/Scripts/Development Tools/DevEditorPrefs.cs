#if DEVELOPMENT_BUILD || UNITY_EDITOR
using DKP.SaveSystem.Data;
using Newtonsoft.Json;
using UnityEngine;

namespace DKP.Development.LevelEditor
{
    public static class DevEditorPrefs
    {
        public static string EDITOR_SAVED_FILE_PATH = "editor_dkpbook_path";
    }
}
#endif