#if DEVELOPMENT_BUILD || UNITY_EDITOR
using DKP.SaveSystem.Data;
using DKP.SpawnSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DKP.Development.LevelEditor
{
    public class DevActionScriptEditor : MonoBehaviour
    {
        public static DevActionScriptEditor I;
        public static SerializableActionScript ActionScript;

        [Header("References")]
        [SerializeField] InternalData _spawnerData;
        [SerializeField] ActionScriptEditorTheme ColorTheme;
        [SerializeField] DevActionScriptLine Prefab;
        [SerializeField] Transform Content;
        [SerializeField] Image Background;
        [SerializeField] TMP_Text[] Texts;
        [SerializeField] Image[] Buttons;

        public static InternalData SpawnerData => I._spawnerData;
        public static ActionScriptEditorTheme Theme => I.ColorTheme;

        [ReadOnly] public List<DevActionScriptLine> ScriptLines = new List<DevActionScriptLine>();
        public DevActionScriptEditor()
        {
            I = this;
        }
        private void Awake()
        {
            if (ScriptLines.Count == 0)
            {
                AddCommand();
            }
        }

        private void OnEnable()
        {
            while (ScriptLines.Count > 1)
            {
                RemoveItem(ScriptLines[1]);
            }
            if (ActionScript.Actions.Count > 0)
            {
                ScriptLines[0].SetCommand(ActionScript.Actions[0].CommandString);
            } else
            {
                ScriptLines[0].SetCommand("// Insert command here");
            }
            for (int i = 1; i < ActionScript.Actions.Count; i++)
            {
                AddCommand(ActionScript.Actions[i].CommandString);
            }
            UpdateNumbers();
        }

        private void OnValidate()
        {
            SetTheme(ColorTheme);
        }
        private void SetTheme(ActionScriptEditorTheme theme)
        {
            Background.color = theme.UIBackgroundColor;
            for (int i = 0; i < Texts.Length; i++)
            {
                Texts[i].color = theme.UITextColor;
            }
            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].color = theme.UIButtonColor;
            }
        }
        public DevActionScriptLine AddCommand()
        {
            DevActionScriptLine devActionScriptLine;
            try
            {
                devActionScriptLine = Instantiate(Prefab, Content);
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
                throw;
            }
            devActionScriptLine.gameObject.SetActive(true);
            ScriptLines.Add(devActionScriptLine);
            return devActionScriptLine;
        }
        public DevActionScriptLine AddCommand(string command)
        {
            DevActionScriptLine devActionScriptLine = AddCommand();
            devActionScriptLine.SetCommand(command);
            return devActionScriptLine;
        }
        public DevActionScriptLine AddCommand(string command, DevActionScriptLine above)
        {
            DevActionScriptLine devActionScriptLine = AddCommand(command);
            int index = ScriptLines.IndexOf(above) + 1;
            ScriptLines.Remove(devActionScriptLine);
            ScriptLines.Insert(index, devActionScriptLine);
            devActionScriptLine.transform.SetSiblingIndex(index);
            return UpdateNumbers(devActionScriptLine);
        }

        public DevActionScriptLine UpdateNumbers(DevActionScriptLine wrapped)
        {
            UpdateNumbers();
            return wrapped;
        }
        
        public void UpdateNumbers()
        {
            for (int i = 0; i < ScriptLines.Count; i++)
            {
                ScriptLines[i].SetLineNumber(i + 1);
            }
        }

        public DevActionScriptLine ConcatUp(string command, DevActionScriptLine item)
        {
            int index = ScriptLines.IndexOf(item);
            if (index > 0)
            {
                ScriptLines[index - 1].SetCommand(ScriptLines[index - 1].Value + command);
                return ScriptLines[index - 1];
            } else
            {
                return AddCommand(DevActionScriptLine.BackspaceTracker + command, item);
            }
        }
        public void RemoveItem(DevActionScriptLine item)
        {
            ScriptLines.Remove(item);
            Destroy(item.gameObject);
        }
        public void ValidateScript()
        {
            ActionScript.Actions.Clear();
            for (int i = 0; i < ScriptLines.Count; i++)
            {
                ActionScript.Actions.Add(ScriptLines[i].GetCommand());
            }
            ActionScript.Modify();
        }
    }
}
#endif