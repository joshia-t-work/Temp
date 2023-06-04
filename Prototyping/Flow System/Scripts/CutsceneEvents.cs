using DKP.ObjectiveSystem;
using DKP.ObserverSystem.GameEvents;
using DKP.SaveSystem.Data;
using DKP.Singletonmanager;
using DKP.TaskManager;
using DKP.UnitSystem;
using MyBox;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace DKP.CutsceneSystem
{
    /// <summary>
    /// Singleton container for all cutscene events
    /// </summary>
    public class CutsceneEvents : MonoBehaviour
    {
        /// <summary>
        /// Access Singleton
        /// </summary>
        public static CutsceneEvents I;
        private void Awake()
        {
            if (I != null)
            {
                Destroy(gameObject);
            }
            else
            {
                I = this;
            }
        }


        /// <summary>
        /// Called when executing trigger.
        /// Params: Trigger Index
        /// </summary>
        public event Func<CancellationToken, string, Task> ExecuteScript;
        /// <summary>
        /// Called when executing trigger.
        /// Params: Trigger Index
        /// </summary>
        public async Task InvokeExecute(CancellationToken ct, string script)
        {
            if (ExecuteScript == null)
                return;
            Delegate[] invokations = ExecuteScript.GetInvocationList();
            Task[] handlerTasks = new Task[invokations.Length];
            for (int i = 0; i < invokations.Length; i++)
            {
                handlerTasks[i] = ((Func<CancellationToken, string, Task>)invokations[i])(ct, script);
            }
            await Task.WhenAll(handlerTasks);
        }

        /// <summary>
        /// Called after start
        /// </summary>
        public event Func<CancellationToken, Task> Initialize;
        /// <summary>
        /// Called after start
        /// </summary>
        public async Task InvokeInitialize(CancellationToken ct)
        {
            if (Initialize == null)
                return;
            Delegate[] invokations = Initialize.GetInvocationList();
            Task[] handlerTasks = new Task[invokations.Length];
            for (int i = 0; i < invokations.Length; i++)
            {
                handlerTasks[i] = ((Func<CancellationToken, Task>)invokations[i])(ct);
            }
            await Task.WhenAll(handlerTasks);
        }

        /// <summary>
        /// Called when showing text.
        /// Params: Text, should continue?
        /// </summary>
        public event Func<CancellationToken, string, bool, Task> DialogueShowText;
        /// <summary>
        /// Called when showing text.
        /// Params: Text, should continue?
        /// </summary>
        public async Task InvokeDialogueShowText(CancellationToken ct, string text, bool shouldContinue)
        {
            if (DialogueShowText == null)
                return;
            Delegate[] invokations = DialogueShowText.GetInvocationList();
            Task[] handlerTasks = new Task[invokations.Length];
            for (int i = 0; i < invokations.Length; i++)
            {
                handlerTasks[i] = ((Func<CancellationToken, string, bool, Task>)invokations[i])(ct, text, shouldContinue);
            }
            await Task.WhenAll(handlerTasks);
        }

        /// <summary>
        /// Spotlights the character in VN
        /// Params: Character Reference
        /// </summary>
        public event Func<CancellationToken, string, Task> DialogueSetCharacter;
        /// <summary>
        /// Spotlights the character in VN
        /// Params: Character Reference
        /// </summary>
        public async Task InvokeDialogueSetCharacter(CancellationToken ct, string charRef)
        {
            if (DialogueSetCharacter == null)
                return;
            Delegate[] invokations = DialogueSetCharacter.GetInvocationList();
            Task[] handlerTasks = new Task[invokations.Length];
            for (int i = 0; i < invokations.Length; i++)
            {
                handlerTasks[i] = ((Func<CancellationToken, string, Task>)invokations[i])(ct, charRef);
            }
            await Task.WhenAll(handlerTasks);
        }

        /// <summary>
        /// Called when moving a character.
        /// Params: Character Reference, Position
        /// </summary>
        public event Func<CancellationToken, string, float, float, Task> CharacterMoveCommand;
        /// <summary>
        /// Called when moving a character.
        /// Params: Character Reference, Position
        /// </summary>
        public async Task InvokeCharacterMoveCommand(CancellationToken ct, string charRef, float position, float speed)
        {
            if (CharacterMoveCommand == null)
                return;
            Delegate[] invokations = CharacterMoveCommand.GetInvocationList();
            Task[] handlerTasks = new Task[invokations.Length];
            for (int i = 0; i < invokations.Length; i++)
            {
                handlerTasks[i] = ((Func<CancellationToken, string, float, float, Task>)invokations[i])(ct, charRef, position, speed);
            }
            await Task.WhenAll(handlerTasks);
        }

        /// <summary>
        /// Called when showing character.
        /// Params: Character Reference, Position
        /// </summary>
        public event Func<CancellationToken, string, Task> ShowCharacter;
        /// <summary>
        /// Called when showing character.
        /// Params: Character Reference, Position
        /// </summary>
        public async Task InvokeShowCharacter(CancellationToken ct, string charRef)
        {
            if (ShowCharacter == null)
                return;
            Delegate[] invokations = ShowCharacter.GetInvocationList();
            Task[] handlerTasks = new Task[invokations.Length];
            for (int i = 0; i < invokations.Length; i++)
            {
                handlerTasks[i] = ((Func<CancellationToken, string, Task>)invokations[i])(ct, charRef);
            }
            await Task.WhenAll(handlerTasks);
        }

        /// <summary>
        /// Called when hiding character.
        /// Params: Character Reference
        /// </summary>
        public event Func<CancellationToken, string, Task> HideCharacter;
        /// <summary>
        /// Called when hiding character.
        /// Params: Character Reference
        /// </summary>
        public async Task InvokeHideCharacter(CancellationToken ct, string charRef)
        {
            if (HideCharacter == null)
                return;
            Delegate[] invokations = HideCharacter.GetInvocationList();
            Task[] handlerTasks = new Task[invokations.Length];
            for (int i = 0; i < invokations.Length; i++)
            {
                handlerTasks[i] = ((Func<CancellationToken, string, Task>)invokations[i])(ct, charRef);
            }
            await Task.WhenAll(handlerTasks);
        }

        /// <summary>
        /// Called when setting character expression.
        /// Params: Character Reference, Expression Reference
        /// </summary>
        public event Func<CancellationToken, string, string, Task> SetExpression;
        /// <summary>
        /// Called when setting character expression.
        /// Params: Character Reference, Expression Reference
        /// </summary>
        public async Task InvokeSetExpression(CancellationToken ct, string charRef, string expressionRef)
        {
            if (SetExpression == null)
                return;
            Delegate[] invokations = SetExpression.GetInvocationList();
            Task[] handlerTasks = new Task[invokations.Length];
            for (int i = 0; i < invokations.Length; i++)
            {
                handlerTasks[i] = ((Func<CancellationToken, string, string, Task>)invokations[i])(ct, charRef, expressionRef);
            }
            await Task.WhenAll(handlerTasks);
        }

        /// <summary>
        /// Called when setting character display name.
        /// Params: Character Reference, Character Name
        /// </summary>
        public event Func<CancellationToken, string, string, Task> SetCharacterName;
        /// <summary>
        /// Called when setting character display name.
        /// Params: Character Reference, Character Name
        /// </summary>
        public async Task InvokeSetCharacterName(CancellationToken ct, string charRef, string charName)
        {
            if (SetCharacterName == null)
                return;
            Delegate[] invokations = SetCharacterName.GetInvocationList();
            Task[] handlerTasks = new Task[invokations.Length];
            for (int i = 0; i < invokations.Length; i++)
            {
                handlerTasks[i] = ((Func<CancellationToken, string, string, Task>)invokations[i])(ct, charRef, charName);
            }
            await Task.WhenAll(handlerTasks);
        }

        /// <summary>
        /// Called when showing character display name.
        /// Params: Character Reference
        /// </summary>
        public event Func<CancellationToken, string, Task> ShowCharacterName;
        /// <summary>
        /// Called when showing character display name.
        /// Params: Character Reference
        /// </summary>
        public async Task InvokeShowCharacterName(CancellationToken ct, string charRef)
        {
            if (ShowCharacterName == null)
                return;
            Delegate[] invokations = ShowCharacterName.GetInvocationList();
            Task[] handlerTasks = new Task[invokations.Length];
            for (int i = 0; i < invokations.Length; i++)
            {
                handlerTasks[i] = ((Func<CancellationToken, string, Task>)invokations[i])(ct, charRef);
            }
            await Task.WhenAll(handlerTasks);
        }

        /// <summary>
        /// Called when hiding character display name.
        /// Params: Character Reference
        /// </summary>
        public event Func<CancellationToken, string, Task> HideCharacterName;
        /// <summary>
        /// Called when hiding character display name.
        /// Params: Character Reference
        /// </summary>
        public async Task InvokeHideCharacterName(CancellationToken ct, string charRef)
        {
            if (HideCharacterName == null)
                return;
            Delegate[] invokations = HideCharacterName.GetInvocationList();
            Task[] handlerTasks = new Task[invokations.Length];
            for (int i = 0; i < invokations.Length; i++)
            {
                handlerTasks[i] = ((Func<CancellationToken, string, Task>)invokations[i])(ct, charRef);
            }
            await Task.WhenAll(handlerTasks);
        }

        /// <summary>
        /// Called when setting background.
        /// Params: Background Name
        /// </summary>
        public event Func<CancellationToken, string, Task> SetBackground;
        /// <summary>
        /// Called when setting background.
        /// Params: Background Name
        /// </summary>
        public async Task InvokeSetBackground(CancellationToken ct, string background)
        {
            if (SetBackground == null)
                return;
            Delegate[] invokations = SetBackground.GetInvocationList();
            Task[] handlerTasks = new Task[invokations.Length];
            for (int i = 0; i < invokations.Length; i++)
            {
                handlerTasks[i] = ((Func<CancellationToken, string, Task>)invokations[i])(ct, background);
            }
            await Task.WhenAll(handlerTasks);
        }


        /// <summary>
        /// Called when changing mode to VN.
        /// </summary>
        public event Func<CancellationToken, Task> SetVNStyle;
        /// <summary>
        /// Called when changing mode to VN.
        /// </summary>
        public async Task InvokeSetVNStyle(CancellationToken ct)
        {
            if (SetVNStyle == null)
                return;
            Delegate[] invokations = SetVNStyle.GetInvocationList();
            Task[] handlerTasks = new Task[invokations.Length];
            for (int i = 0; i < invokations.Length; i++)
            {
                handlerTasks[i] = ((Func<CancellationToken, Task>)invokations[i])(ct);
            }
            await Task.WhenAll(handlerTasks);
        }

        public Task Wrap(Task t)
        {
            return Task.Run(async () =>
            {
                try
                {
                    await t;
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                    throw;
                }
            });
        }

        /// <summary>
        /// Called when changing mode to InGame.
        /// </summary>
        public event Func<CancellationToken, Task> SetInGameStyle;
        /// <summary>
        /// Called when changing mode to InGame.
        /// </summary>
        public async Task InvokeSetInGameStyle(CancellationToken ct)
        {
            if (SetInGameStyle == null)
                return;
            Delegate[] invokations = SetInGameStyle.GetInvocationList();
            Task[] handlerTasks = new Task[invokations.Length];
            for (int i = 0; i < invokations.Length; i++)
            {
                handlerTasks[i] = ((Func<CancellationToken, Task>)invokations[i])(ct);
            }
            await Task.WhenAll(handlerTasks);
        }

        /// <summary>
        /// Called when spawning a unit.
        /// </summary>
        public event Func<CancellationToken, SpawnCommand, Task> Spawn;
        /// <summary>
        /// Called when starting tutorial.
        /// </summary>
        public async Task InvokeSpawn(CancellationToken ct, SpawnCommand spawnCommand)
        {
            if (Spawn == null)
                return;
            Delegate[] invokations = Spawn.GetInvocationList();
            Task[] handlerTasks = new Task[invokations.Length];
            for (int i = 0; i < invokations.Length; i++)
            {
                handlerTasks[i] = ((Func<CancellationToken, SpawnCommand, Task>)invokations[i])(ct, spawnCommand);
            }
            await Task.WhenAll(handlerTasks);
        }

        /// <summary>
        /// Called when starting objective.
        /// </summary>
        public event Func<CancellationToken, IObjective, Task> StartObjective;
        /// <summary>
        /// Called when starting objective.
        /// </summary>
        public async Task InvokeStartObjective(CancellationToken ct, IObjective objective)
        {
            if (StartObjective == null)
                return;
            Delegate[] invokations = StartObjective.GetInvocationList();
            Task[] handlerTasks = new Task[invokations.Length];
            for (int i = 0; i < invokations.Length; i++)
            {
                handlerTasks[i] = ((Func<CancellationToken, IObjective, Task>)invokations[i])(ct, objective);
            }
            await Task.WhenAll(handlerTasks);
        }

        /// <summary>
        /// Called when an objective is achieved.
        /// </summary>
        public event Func<CancellationToken, Task> StopObjective;
        /// <summary>
        /// Called when an objective is achieved.
        /// </summary>
        public async Task InvokeStopObjective(CancellationToken ct)
        {
            if (StopObjective == null)
                return;
            Delegate[] invokations = StopObjective.GetInvocationList();
            Task[] handlerTasks = new Task[invokations.Length];
            for (int i = 0; i < invokations.Length; i++)
            {
                handlerTasks[i] = ((Func<CancellationToken, Task>)invokations[i])(ct);
            }
            await Task.WhenAll(handlerTasks);
        }

        /// <summary>
        /// Called when a unit dies.
        /// </summary>
        public event Func<CancellationToken, Unit, Task> UnitDied;
        /// <summary>
        /// Called when a unit dies.
        /// </summary>
        public async Task InvokeUnitDied(CancellationToken ct, Unit unit)
        {
            if (UnitDied == null)
                return;
            Delegate[] invokations = UnitDied.GetInvocationList();
            Task[] handlerTasks = new Task[invokations.Length];
            for (int i = 0; i < invokations.Length; i++)
            {
                handlerTasks[i] = ((Func<CancellationToken, Unit, Task>)invokations[i])(ct, unit);
            }
            await Task.WhenAll(handlerTasks);
        }
    }
}