#if DEVELOPMENT_BUILD || UNITY_EDITOR
using DKP.SaveSystem.Data;
using MyBox;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace DKP.Development.LevelEditor
{
    [CreateAssetMenu(fileName = "Editor Theme", menuName = "SO/Action Script Editor Theme")]
    public class ActionScriptEditorTheme : ScriptableObject
    {
        [Header("UI")]
        public Color UIBackgroundColor;
        public Color UITextColor;
        public Color UIButtonColor;
        public Color UILineLabelColor;
        public Color UILineSelectedColor;
        [Header("Syntax Highlighting")]
        public Color CodeDefaultColor;
        public Color CodeCommandColor;
        public Color CodeSelectionColor;
        public Color CodeAutocompleteColor;
        public Color CodeVariableColor;
        public Color CodeVariableValueColor;
        public Color CodeWarnColor;
        public Color CodeInvalidColor;
        public Color CodeStruct;
        public Color CodeCommentColor;
        public ColorValuePair[] CustomColoredStrings;
        [Header("Syntaxs")]
        public Color FILE_EDIT;
        public Color TAB;
        public Color TABTEXT;
        public Color CODEBG;
        public Color LINENUMBG;
        public Color LINENUM;
        public Color LINENUMSELECT;
        public Color SERIALIZEFIELD;
        public Color PRIVATE;
        public Color HEADERREFERENCES;
        public Color UPDATECOMMAND;
        public Color ADDREADONLYMODIFIER;
        public Color STRINGVALUEPARAM;
        // from https://github.com/mono/monodevelop/blob/main/main/src/core/MonoDevelop.Ide/MonoDevelop.Ide.Editor.Highlighting/themes/DarkStyle.json
        [Header("Searching")]
        public string DICT_SEARCH;
        public string DICT_WORD;
        public Color DICT_FOUND;
        [ReadOnly]
        public ColorValuePair[] DICT;
        public string COLOR_SEARCH;
        public string COLOR_WORD;
        public Color[] COLOR_FOUND;
        [ReadOnly]
        public ColorPair[] COLORS;
        //private string pallet = "butter2|#edd400_orange1|#fcaf3e_orange2|#f57900_chocolate1|#e9b96e_chocolate2|#c17d11_chocolate3|#8f5902_chameleon1|#8ae234_chameleon2|#73d216_chameleon3|#4e9a06_chameleon4|#356904_skyblue1|#729fcf_skyblue2|#3465a4_skyblue3|#204a87_plum1|#ad7fa8_scarletred1|#ef2929_scarletred2|#cc0000_aluminium1|#eeeeec_aluminium2|#d3d7cf_aluminium3|#babdb6_aluminium4|#888a85_aluminium5|#555753_aluminium6|#2e3436_aluminium7|#0e1416";
        //private string tempData = "Background(Read Only)|aluminium7_Search result background|#006060_Search result background (highlighted)|#008080_Column Ruler|#2a2c2f_Fold Square|aluminium5|#1c1e1f_Fold Cross|aluminium5|#1c1e1f_Indentation Guide|#444a4d_Indicator Margin|#303030_Indicator Margin(Separator)|#303030_Tooltip Pager Top|aluminium5_Tooltip Pager Triangle|aluminium2_Tooltip Pager Text|aluminium2_Notification Border|aluminium1_Completion Window|aluminium6|aluminium1_Completion Tooltip Window|aluminium5|aluminium1_Completion Selection Bar Border|aluminium5_Completion Selection Bar Border(Inactive)|aluminium7_Completion Selection Bar Background|aluminium5|aluminium5_Completion Selection Bar Background(Inactive)|aluminium7|aluminium7_Bookmarks|aluminium1|aluminium4_Underline(Error)|scarletred2_Underline(Warning)|butter2_Underline(Suggestion)|chameleon3_Underline(Hint)|chameleon2_Quick Diff(Dirty)|butter2_Quick Diff(Changed)|chameleon2_Brace Matching(Rectangle)|#476a93|#476a93_Usages(Rectangle)|skyblue3|skyblue3|skyblue2_Changing usages(Rectangle)|chameleon4|chameleon4|chameleon3_Breakpoint Marker|#6f3535|#6f3535_Breakpoint Marker(Disabled)|#4d4d4d|#4d4d4d_Breakpoint Marker(Invalid)|#604343|#604343_Current Line Marker|#2a2c2f|#2a2c2f_Current Line Marker(Inactive)|#2a2c2f|#2a2c2f_Debugger Current Line Marker|#69684c|#69684c_Debugger Stack Line Marker|#5f7247|#5f7247_Primary Link|#7C97A6|chocolate3_Primary Link(Highlighted)|#7C97A6|chocolate2_Secondary Link|white|aluminium6_Secondary Link(Highlighted)|aluminium1|aluminium5_Message Bubble Error Marker|#b28d37_Message Bubble Error Tag|#e3a6a1|black_Message Bubble Error Counter|black|#e3a6a1_Message Bubble Error IconMargin|#735c54|#805b4d_Message Bubble Error Line|#7b645c_Message Bubble Error Tooltip|#e3a6a1_Message Bubble Warning Tag|#efe89d|black_Message Bubble Warning Counter|black|#efe89d_Message Bubble Warning IconMargin|#777553|#948e51_Message Bubble Warning Line|#807e5c_Message Bubble Warning Tooltip|#efe89d_Link Color|#41e2cb_Link Color(Active)|#41e2cb_Plain Text|aluminium1|#1c1e1f_Selected Text|#245176_Selected Text(Inactive)|aluminium5_Collapsed Text|aluminium4|#1c1e1f_Line Numbers|aluminium5|#242424_Punctuation|aluminium1_Punctuation(Brackets)|aluminium1_Comment(Line)|#7a976b_Comment(Block)|#7a976b_Comment(Doc)|#7a976b_Comment(DocTag)|aluminium4_Comment Tag|#ff37ff_Excluded Code|aluminium4_String|#E6DB74_String(Escape)|#A6AB34_String(C# @ Verbatim)|#E6DB74_String(Regex Set Constructs)|chameleon2_String(Regex Character Class)|skyblue1_String(Regex Grouping Constructs)|plum1_String(Regex Escape Character)|orange2_String(Regex Alt Escape Character)|orange1_Number|chameleon1_Preprocessor|plum1_Preprocessor(Region Name)|aluminium1_Xml Text|aluminium1_Xml Delimiter|aluminium1_Xml Name|skyblue1_Xml Attribute|#9CDCFE_Xml Attribute Quotes|#E6DB74_Xml Attribute Value|#E6DB74_Xml Comment|aluminium4_Xml CData Section|aluminium1_Html Attribute Name|aluminium1_Html Attribute Value|#E6DB74_Html Comment|aluminium4_Html Element Name|skyblue1_Html Entity|skyblue1_Html Operator|aluminium1_Html Server-Side Script|black|#d2d295_Html Tag Delimiter|aluminium1_Razor Code|aluminium1|aluminium7_Tooltip Text|#d1d1cd|#525759_Notification Text|aluminium1|aluminium5_Completion Text|aluminium1_Completion Matching Substring|plum1_Completion Selected Text|aluminium1_Completion Selected Matching Substring|plum1_Completion Selected Text(Inactive)|aluminium1_Completion Selected Matching Substring(Inactive)|plum1_Keyword(Access)|skyblue1_Keyword(Type)|skyblue1_Keyword(Operator)|skyblue1_Keyword(Selection)|skyblue1_Keyword(Iteration)|skyblue1_Keyword(Jump)|skyblue1_Keyword(Context)|skyblue1_Keyword(Exception)|skyblue1_Keyword(Modifiers)|skyblue1_Keyword(Constants)|skyblue1_Keyword(Void)|skyblue1_Keyword(Namespace)|skyblue1_Keyword(Property)|skyblue1_Keyword(Declaration)|skyblue1_Keyword(Parameter)|skyblue1_Keyword(Operator Declaration)|skyblue1_Keyword(Other)|skyblue1_User Types|#4ec9b0_User Types(Enums)|#b8d7a3_User Types(Interfaces)|#b8d7a3_User Types(Delegates)|#4ec9b0_User Types(Value types)|#4ec9b0_User Types(Type parameters)|#4ec9b0_User Types(Mutable)|#ffd21c_User Field Usage|aluminium1_User Field Declaration|aluminium1_User Property Usage|aluminium1_User Property Declaration|aluminium1_User Event Usage|aluminium1_User Event Declaration|aluminium1_User Method Usage|aluminium1_User Method Declaration|aluminium1_User Parameter Usage|aluminium1_User Parameter Declaration|aluminium1_User Variable Usage|aluminium1_User Variable Declaration|aluminium1_Syntax Error|scarletred1_String Format Items|aluminium1_Breakpoint Text|white|#6f3535_Debugger Current Statement|white|#69684c_Debugger Stack Line|white|#5c6b4d_Diff Line(Added)|chameleon1_Diff Line(Removed)|scarletred2_Diff Line(Changed)|plum1_Diff Header|chameleon1|bold_Diff Header(Separator)|aluminium4|bold_Diff Header(Old)|scarletred2|bold_Diff Header(New)|chameleon1|bold_Diff Location|chameleon1|bold_Preview Diff Removed Line|#5c2c2c|#dcb4b4_Preview Diff Added Line|#235423|#a4d9a4_Css Comment|aluminium4_Css Property Name|aluminium1_Css Property Value|butter2_Css Selector|aluminium1_Css String Value|butter2_Css Keyword|plum1_Script Comment|aluminium4_Script Keyword|plum1_Script Number|butter2_Script String|butter2";
        private void OnValidate()
        {
            for (int i = 0; i < DICT.Length; i++)
            {
                if (DICT[i].name.Trim().StartsWith(DICT_SEARCH, StringComparison.OrdinalIgnoreCase))
                {
                    DICT_FOUND = DICT[i].color;
                    DICT_WORD = DICT[i].name;
                    break;
                }
            }
            for (int i = 0; i < COLORS.Length; i++)
            {
                if (COLORS[i].name.Trim().StartsWith(COLOR_SEARCH, StringComparison.OrdinalIgnoreCase))
                {
                    COLOR_FOUND = COLORS[i].color;
                    COLOR_WORD = COLORS[i].name;
                    break;
                }
            }
            //if (DICT.Length < 10)
            //{
            //    List<ColorValuePair> dict = new List<ColorValuePair>();
            //    string[] pcols = pallet.Split('_');
            //    for (int i = 0; i < pcols.Length; i++)
            //    {
            //        string[] namecol = pcols[i].Split('|');
            //        ColorValuePair cvp = new ColorValuePair();
            //        cvp.name = namecol[0];
            //        if (ColorUtility.TryParseHtmlString(namecol[1], out Color cvpc))
            //        {
            //            cvp.color = cvpc;
            //        }
            //        dict.Add(cvp);
            //    }
            //    DICT = dict.ToArray();
            //}
            //if (COLORS.Length < 10)
            //{
            //    Dictionary<string, Color> keyValuePairs = new Dictionary<string, Color>();
            //    for (int i = 0; i < DICT.Length; i++)
            //    {
            //        keyValuePairs.Add(DICT[i].name, DICT[i].color);
            //    }
            //    List<ColorPair> colors = new List<ColorPair>();
            //    List<Color> cs = new List<Color>();
            //    string[] color = tempData.Split('_');
            //    for (int ii = 0; ii < color.Length; ii++)
            //    {
            //        string[] things = color[ii].Split('|');
            //        ColorPair c = new ColorPair();
            //        colors.Add(c);
            //        c.name = things[0];
            //        cs.Clear();
            //        for (int iii = 1; iii < things.Length; iii++)
            //        {
            //            if (keyValuePairs.TryGetValue(things[iii], out Color col))
            //            {
            //                cs.Add(col);
            //            }
            //            else
            //            {
            //                if (ColorUtility.TryParseHtmlString(things[iii], out Color colr))
            //                {
            //                    cs.Add(colr);
            //                }
            //            }
            //        }
            //        c.color = cs.ToArray();
            //    }
            //    COLORS = colors.ToArray();
            //}
        }
    }
    [Serializable]
    public class ColorValuePair
    {
        public string name;
        public Color color;
    }
    [Serializable]
    public class ColorPair
    {
        public string name;
        public Color[] color;
    }
}
#endif