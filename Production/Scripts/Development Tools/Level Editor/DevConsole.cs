#if DEVELOPMENT_BUILD || UNITY_EDITOR
using DKP.Development.LevelEditor.Data;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DKP.Development.LevelEditor
{
    public class DevConsole : MonoBehaviour
    {
        public static DevConsole I;

        [SerializeField]
        TMP_Text console;

        private static Queue<string> consoleText = new Queue<string>(10);
        private static int textIndex = 0;

        private void Awake()
        {
            I = this;
        }

        public static void PushText(string text)
        {
            textIndex += 1;
            if (consoleText.Count >= 10)
            {
                consoleText.Dequeue();
            }
            consoleText.Enqueue($"[{textIndex}] {text}");
            Render();
        }

        public static void ClearText()
        {
            textIndex = 0;
            consoleText.Clear();
            Render();
        }

        private static void Render()
        {
            I.console.text = $"Console\n{string.Join("\n", consoleText.ToArray())}";
        }
    }
}
#endif