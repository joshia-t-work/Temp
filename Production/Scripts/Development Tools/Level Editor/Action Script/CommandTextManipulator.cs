#if DEVELOPMENT_BUILD || UNITY_EDITOR
using DKP.SaveSystem.Data;
using MyBox;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static DKP.Development.LevelEditor.DevActionScriptLine;

namespace DKP.Development.LevelEditor
{
    public static class CommandTextManipulator
    {
        public static string LintStyle(string val)
        {
            if (SetStyleCommand.STYLES.Contains(val.Replace("\"", "").Trim()))
            {
                return encapsulate(val, DevActionScriptEditor.Theme.CodeVariableValueColor);
            }
            else
            {
                return encapsulate(val, DevActionScriptEditor.Theme.CodeInvalidColor);
            }
        }
        public static string LintBackground(string val)
        {
            string bgname = val.Replace("\"", "").Trim();
            if (DevEditorDataContainer.GameBookData.Backgrounds != null)
            {
                for (int i = 0; i < DevEditorDataContainer.GameBookData.Backgrounds.Images.Count; i++)
                {
                    if (DevEditorDataContainer.GameBookData.Backgrounds.Images[i].Name == bgname)
                    {
                        return encapsulate(val, DevActionScriptEditor.Theme.CodeVariableValueColor);
                    }
                }
            }
            return encapsulate(val, DevActionScriptEditor.Theme.CodeWarnColor);
        }
        public static string LintExecute(string val)
        {
            string scriptname = val.Replace("\"", "").Trim();
            for (int i = 0; i < DevEditorDataContainer.GameBookData.ActionScripts.ActionScripts.Count; i++)
            {
                if (DevEditorDataContainer.GameBookData.ActionScripts.ActionScripts[i].ObjectName == scriptname)
                {
                    return encapsulate(val, DevActionScriptEditor.Theme.CodeVariableValueColor);
                }
            }
            return encapsulate(val, DevActionScriptEditor.Theme.CodeWarnColor);
        }
        public static string LintCharacter(string val)
        {
            string charactername = val.Replace("\"", "").Trim();
            for (int i = 0; i < DevEditorDataContainer.GameBookData.Images.Count; i++)
            {
                if (DevEditorDataContainer.GameBookData.Images[i].ObjectName == charactername)
                {
                    return encapsulate(val, DevActionScriptEditor.Theme.CodeVariableValueColor);
                }
            }
            return encapsulate(val, DevActionScriptEditor.Theme.CodeWarnColor);
        }
        public static string LintUnit(string val)
        {
            string unitname = val.Replace("\"", "").Trim();
            if (DevActionScriptEditor.SpawnerData.ContainsUnit(unitname))
            {
                return encapsulate(val, DevActionScriptEditor.Theme.CodeVariableValueColor);
            }
            return encapsulate(val, DevActionScriptEditor.Theme.CodeWarnColor);
        }
        public static string LintSpawner(string val)
        {
            string spawnername = val.Replace("\"", "").Trim();
            if (DevActionScriptEditor.SpawnerData.ContainsSpawner(spawnername))
            {
                return encapsulate(val, DevActionScriptEditor.Theme.CodeVariableValueColor);
            }
            return encapsulate(val, DevActionScriptEditor.Theme.CodeWarnColor);
        }
        public static bool IsVariable(string val)
        {
            return val.StartsWith("$") && val.EndsWith("$");
        }
        public static string LintInt(string val)
        {
            if (IsVariable(val))
            {
                return encapsulate(val, DevActionScriptEditor.Theme.CodeVariableColor);
            }
            try
            {
                int intval = int.Parse(val);
                return encapsulate(val, DevActionScriptEditor.Theme.CodeStruct);
            }
            catch (Exception)
            {
                return encapsulate(val, DevActionScriptEditor.Theme.CodeWarnColor);
            }
        }
        public static string LintFloat(string val)
        {
            if (IsVariable(val))
            {
                return encapsulate(val, DevActionScriptEditor.Theme.CodeVariableColor);
            }
            try
            {
                float floatval = float.Parse(val);
                return encapsulate(val, DevActionScriptEditor.Theme.CodeStruct);
            }
            catch (Exception)
            {
                return encapsulate(val, DevActionScriptEditor.Theme.CodeWarnColor);
            }
        }
        public static Dictionary<string, CommandSyntax> Commands = new Dictionary<string, CommandSyntax>()
        {
            { "Set", SetCommand.Syntax },
            { "SetStyle", SetStyleCommand.Syntax },
            { "SetBackground", SetBackgroundCommand.Syntax },
            { "Delay", DelayCommand.Syntax },
            { "TypeSpeed", TypeSpeedCommand.Syntax },
            { "Execute", ExecuteCommand.Syntax },
            { "Move", MoveCommand.Syntax },
            { "Spawn", SpawnCommand.Syntax },
            { "Objective", ObjectiveKillCommand.Syntax },
        };

        public enum CodeSyntax
        {
            Default,
            Command,
            Variable,
            VariableValue,
            Autocomplete
        }

        public enum ParamType
        {
            Any,
            AnyOrSceneSetting,
            VariableName,
            ActionScript
        }

        public class CommandSyntax
        {
            public HighlightedText[] Syntax;
            public Func<string, string[], BaseCommand> GetCommand;
            public bool AllowExtraParams;
            /// <summary>
            /// Creates a new command syntax
            /// </summary>
            /// <param name="syntax">Syntax Highlighting</param>
            /// <param name="command">Method to create command instance</param>
            /// <param name="allowExtraParams">When disallowed shows error on additional params?</param>
            public CommandSyntax(HighlightedText[] syntax, Func<string, string[], BaseCommand> command, bool allowExtraParams = false)
            {
                Syntax = syntax;
                GetCommand = command;
                AllowExtraParams = allowExtraParams;
            }

            public string Lint(int cursorPos, string[] args)
            {
                string suffix = "";
                for (int i = 0; i < Mathf.Min(args.Length, Syntax.Length); i++)
                {
                    if (args[i] == "")
                    {
                        if (cursorPos == 0)
                        {
                            args[i] = encapsulate(Syntax[i].Text, DevActionScriptEditor.Theme.CodeAutocompleteColor);
                        }
                    }
                    else
                    {
                        args[i] = Syntax[i].Lint(args[i]);
                    }
                    cursorPos -= args[i].Length + 1;
                }
                if (!AllowExtraParams)
                {
                    if (args.Length > Syntax.Length)
                    {
                        SetInvalidExtraArgs(args, Syntax.Length);
                    }
                }
                for (int i = Syntax.Length; i-- > Mathf.Min(args.Length, Syntax.Length);)
                {
                    suffix = " " + Syntax[i].Text + suffix;
                }
                return suffix;
            }

            private void SetInvalidExtraArgs(string[] args, int countRequired)
            {
                for (int i = countRequired; i < args.Length; i++)
                {
                    if (i == countRequired)
                    {
                        args[i] = encapsulate(args[i], DevActionScriptEditor.Theme.CodeInvalidColor, EncapsulationType.Left);
                    }
                    if (i == args.Length - 1)
                    {
                        args[i] = encapsulate($"{args[i]} <-- unexpected", DevActionScriptEditor.Theme.CodeInvalidColor, EncapsulationType.Right);
                    }
                }
            }
        }

        public class HighlightedText
        {
            public string Text;
            public Color Color
            {
                get
                {
                    if (isDirty)
                    {
                        isDirty = false;
                        switch (codeSyntax)
                        {
                            case CodeSyntax.Default:
                                color = DevActionScriptEditor.Theme.CodeDefaultColor;
                                break;
                            case CodeSyntax.Command:
                                color = DevActionScriptEditor.Theme.CodeCommandColor;
                                break;
                            case CodeSyntax.Variable:
                                color = DevActionScriptEditor.Theme.CodeVariableColor;
                                break;
                            case CodeSyntax.VariableValue:
                                color = DevActionScriptEditor.Theme.CodeVariableValueColor;
                                break;
                            case CodeSyntax.Autocomplete:
                                color = DevActionScriptEditor.Theme.CodeAutocompleteColor;
                                break;
                            default:
                                throw new NotImplementedException();
                        }
                    }
                    return color;
                }
            }
            private bool isDirty = true;
            private Color color;
            private CodeSyntax codeSyntax;
            private Func<string, string> lint;

            public HighlightedText(string text, CodeSyntax codeSyntax)
            {
                Text = text;
                this.codeSyntax = codeSyntax;
            }
            public HighlightedText(string text, Func<string, string> lint)
            {
                Text = text;
                this.lint = lint;
            }
            public string Lint(string val)
            {
                if (lint == null)
                {
                    return encapsulate(val, Color);
                }
                else
                {
                    return lint(val);
                }
            }

        }
        public static string[] SpecialSplit(string input)
        {
            List<string> result = new List<string>();
            bool insideQuotation = false;
            int startIndex = 0;

            for (int i = 0; i < input.Length; i++)
            {
                char currentChar = input[i];

                if (currentChar == '"')
                {
                    insideQuotation = !insideQuotation;
                }

                if (currentChar == ' ' && !insideQuotation)
                {
                    result.Add(input.Substring(startIndex, i - startIndex));
                    startIndex = i + 1;
                }
            }

            result.Add(input.Substring(startIndex));

            return result.ToArray();
        }

        public static string LintCommand(int textPosition, string value)
        {
            // TODO: Recolor parenthesis when they are not closed
            //Debug.Log($"{_inputText.selectionStringAnchorPosition} {_inputText.selectionStringFocusPosition}");
            ////_inputText.stringPosition
            //if (_inputText.stringPosition - 1 > 0)
            //{
            //    //Debug.Log(value[_inputText.stringPosition - 1]);
            //}

            if (value.Trim().Substring(BackspaceTracker.Length).StartsWith("//"))
            {
                // Check if a comment
                return encapsulate(value.Trim().Substring(BackspaceTracker.Length), DevActionScriptEditor.Theme.CodeCommentColor);
            }
            else
            {
                string[] split = SpecialSplit(value.Trim().Substring(BackspaceTracker.Length));
                string suffix = "";
                if (split.Length > 0)
                {
                    if (Commands.TryGetValue(split[0], out CommandSyntax syntax))
                    {
                        suffix = syntax.Lint(textPosition, split);
                        //split[0] = encapsulate(split[0], Theme.CodeCommandColor);
                    }
                }
                //for (int i = 0; i < split.Length; i++)
                //{
                //    Debug.Log(split[i]);
                //    if (commands.Contains(split[i]))
                //    {
                //        split[i] = encapsulate(split[i], Theme.CommandColor);
                //    }
                //}
                string modifiedString = string.Join(" ", split);

                // set string colors
                for (int i = 0; i < DevActionScriptEditor.Theme.CustomColoredStrings.Length; i++)
                {
                    modifiedString = modifiedString.Replace(DevActionScriptEditor.Theme.CustomColoredStrings[i].name, encapsulate(DevActionScriptEditor.Theme.CustomColoredStrings[i].name, DevActionScriptEditor.Theme.CustomColoredStrings[i].color));
                }

                // add suffix to modifiedString
                modifiedString += encapsulate(suffix, DevActionScriptEditor.Theme.CodeAutocompleteColor);

                // encapsulate with default color
                modifiedString = encapsulate(modifiedString, DevActionScriptEditor.Theme.CodeDefaultColor);
                return modifiedString;
            }
        }
        static string TrimQuotes(string val)
        {
            if (val.StartsWith("\""))
            {
                return val.Substring(1, val.Length - 2);
            }
            else
            {
                return val;
            }
        }

        public static BaseCommand ParseCommand(string text)
        {
            if (text.StartsWith("//"))
            {
                return new CommentCommand(text);
            }
            else
            {
                string[] split = SpecialSplit(text);
                if (split.Length > 0)
                {
                    for (int i = 0; i < split.Length; i++)
                    {
                        split[i] = TrimQuotes(split[i]);
                    }
                    if (Commands.TryGetValue(split[0], out CommandSyntax syntax))
                    {
                        if (split.Length == syntax.Syntax.Length)
                        {
                            return syntax.GetCommand(text, split);
                        }
                    }
                }
                int closeIndex = text.IndexOf(']');
                string dialogue = text.Substring(closeIndex + 1);
                if (dialogue.Length > 0)
                {
                    if (text[closeIndex + 1] == ' ')
                    {
                        dialogue = text.Substring(closeIndex + 2);
                    }
                }
                string[] characterdialogue = text.Split('[', ']');
                if (characterdialogue.Length > 1)
                {
                    string[] characterData = characterdialogue[1].Split(new string[] { "->" }, StringSplitOptions.None);
                    if (characterData.Length == 2)
                    {
                        return new CharacterCommand(text, dialogue, characterData[0].Trim(), characterData[1].Trim());
                    }
                    else if (characterData.Length == 1)
                    {
                        return new CharacterCommand(text, dialogue, characterData[0].Trim(), "");
                    }
                }
                return new SpeakCommand(text, dialogue);
            }
        }

        public static string encapsulate(string value, Color color, EncapsulationType encapsulationType = EncapsulationType.Both)
        {
            switch (encapsulationType)
            {
                case EncapsulationType.Both:
                    return $"<color={color.ToHex()}>{value}</color>";
                case EncapsulationType.Left:
                    return $"<color={color.ToHex()}>{value}";
                case EncapsulationType.Right:
                    return $"{value}</color>";
                default:
                    return value;
            }
        }

        public enum EncapsulationType
        {
            Both,
            Left,
            Right
        }
    }
}
#endif