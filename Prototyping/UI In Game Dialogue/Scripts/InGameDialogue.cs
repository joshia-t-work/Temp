using DKP.FlowSystem;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DKP.DialogueSystem
{
    public class InGameDialogue : MonoBehaviour
    {
        [SerializeField]
        RectTransform textRect;

        [SerializeField]
        TMP_Text dialogueText;
        [SerializeField]
        LayoutElement textLayout;

        [SerializeField]
        RectTransform imageLayoutGroup;
        [SerializeField]
        float maxWidth;
        [ReadOnly]
        public bool IsTyping = false;
        [ReadOnly]
        public bool Skip = false;
        [SerializeField, ReadOnly]
        bool shouldContinue = false;

        public async Task TypeText(string text, bool shouldEnd, CancellationToken ct)
        {
            IsTyping = true;
            textLayout.enabled = false;
            textLayout.preferredWidth = maxWidth;
            if (!shouldContinue)
            {
                dialogueText.text = "";
            }
            shouldContinue = !shouldEnd;
            for (int i = 0; i < text.Length; i++)
            {
                dialogueText.text += text[i];
                if (textRect.sizeDelta.x >= maxWidth)
                {
                    textLayout.enabled = true;
                }
                LayoutRebuilder.ForceRebuildLayoutImmediate(imageLayoutGroup);
                if (!Skip)
                {
                    await Task.Delay(Flow.FlowTypingDelay, ct);
                }
                else
                {
                    continue;
                }
            }
            Skip = false;
            IsTyping = false;
            if (shouldEnd)
            {
                await Task.Delay(500, ct);
                dialogueText.text = "";
            }
        }
    }
}