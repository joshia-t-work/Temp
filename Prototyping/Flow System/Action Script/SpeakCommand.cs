using DKP.CutsceneSystem;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace DKP.SaveSystem.Data
{
    /// <summary>
    /// Represents a command to set the character to speak and change its expression image, SHOW or HIDE as expression to show/hide the character
    /// </summary>
    [Serializable]
    public class SpeakCommand : BaseCommand
    {
        public string CharacterDialogue;
        public bool ShouldContinue;
        public async override Task<BaseCommand> Execute(SerializableGameBook gamebook, CancellationToken ct)
        {
            await CutsceneEvents.I.InvokeDialogueShowText(ct, CharacterDialogue, ShouldContinue);
            return await base.Execute(gamebook, ct);
        }
        public SpeakCommand(string commandString, string dialogueString) : base(commandString)
        {
            CharacterDialogue = dialogueString;
        }
    }
}