#if DEVELOPMENT_BUILD || UNITY_EDITOR
using DKP.SaveSystem.Data;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static DKP.Development.LevelEditor.CommandTextManipulator;

namespace DKP.Development.LevelEditor
{
    public class DevActionScriptLine : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TMP_Text lineNumber;
        [SerializeField] private Image lineMarker;

        [SerializeField] private TMP_Text _displayText;
        [SerializeField] private TMP_InputField _inputText;
        
        bool isSelected = false;
        public string Value => _inputText.text;
        public static string BackspaceTracker = "";

        private void Awake()
        {
            lineNumber.color = DevActionScriptEditor.Theme.UILineLabelColor;
            lineMarker.color = DevActionScriptEditor.Theme.UILineLabelColor;
            _inputText.selectionColor = DevActionScriptEditor.Theme.CodeSelectionColor;
            _inputText.caretColor = DevActionScriptEditor.Theme.UILineLabelColor;
            if (BackspaceTracker == "")
            {
                BackspaceTracker = encapsulate("", DevActionScriptEditor.Theme.UIBackgroundColor);
            }
            _inputText.SetTextWithoutNotify(BackspaceTracker);
        }
        private void Update()
        {
            if (isSelected)
            {
                UpdateCommand(_inputText.text);
            }
        }
        public void SetLineNumber(int val)
        {
            lineNumber.text = val.ToString();
        }
        public void SelectLine()
        {
            isSelected = true;
            lineNumber.color = DevActionScriptEditor.Theme.UILineSelectedColor;
        }
        public void SelectEndLine()
        {
            _inputText.Select();
            _inputText.MoveTextEnd(false);
            isSelected = true;
            lineNumber.color = DevActionScriptEditor.Theme.UILineSelectedColor;
        }
        public void DeselectLine()
        {
            isSelected = false;
            lineNumber.color = DevActionScriptEditor.Theme.UILineLabelColor;
        }
        public void SetCommand(string value)
        {
            _inputText.text = $"{BackspaceTracker}{value}";
            UpdateCommand(_inputText.text);
        }
        public void UpdateCommand(string value)
        {
            string val = value;
            if (val.StartsWith(BackspaceTracker + BackspaceTracker))
            {
                val = val.Substring(BackspaceTracker.Length);
            }
            if (!val.StartsWith(BackspaceTracker))
            {
                int cutIndex = 0;
                for (int i = 0; i < Mathf.Min(val.Length, BackspaceTracker.Length); i++)
                {
                    cutIndex = i;
                    if (val[i] != BackspaceTracker[i])
                    {
                        break;
                    }
                    cutIndex = i + 1;
                }
                DevActionScriptEditor.I.ConcatUp(val.Substring(cutIndex), this).SelectEndLine();
                DevActionScriptEditor.I.RemoveItem(this);
                DevActionScriptEditor.I.UpdateNumbers();
                return;
            }
            if (val.Contains('\n'))
            {
                string[] split =val.Split('\n');
                val = split[0];
                DevActionScriptLine last = this;
                for (int i = 1; i < split.Length; i++)
                {
                    if (split[i].StartsWith(BackspaceTracker))
                    {
                        last = DevActionScriptEditor.I.AddCommand(split[i], last);
                    } else
                    {
                        last = DevActionScriptEditor.I.AddCommand(BackspaceTracker + split[i], last);
                    }
                }
                last.SelectEndLine();
            }
            _displayText.text = LintCommand(_inputText.stringPosition, val);
            _inputText.text = val;
        }

        
        public BaseCommand GetCommand()
        {
            string inputCommand = _inputText.text.Trim().Substring(BackspaceTracker.Length);
            return ParseCommand(inputCommand);
        }
    }
}
#endif