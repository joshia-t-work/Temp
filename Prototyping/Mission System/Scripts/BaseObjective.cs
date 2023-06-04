using DKP.Game;
using DKP.ObserverSystem;
using System.Threading.Tasks;
using UnityEngine;

namespace DKP.ObjectiveSystem
{
    /// <summary>
    /// Defines a base Objective class for all objectives. Used by missions.
    /// </summary>
    public abstract class BaseObjective : ScriptableObject
    {
        protected TaskCompletionSource<bool> ObjectiveCompleted;
        public GameEvent ObjectiveUpdateEvent = new GameEvent();
        public Task StartObjective(GameEvents gameEvents)
        {
            ObjectiveCompleted = new TaskCompletionSource<bool>();
            startObjective(gameEvents);
            return ObjectiveCompleted.Task;
        }
        /// <summary>
        /// Call when cleaning the objective to free memory
        /// </summary>
        public virtual void EndObjective() { }
        /// <summary>
        /// Method called when the objective is triggered, when the objective is completed call ObjectiveCompleted.TrySetResult(true);
        /// </summary>
        /// <param name="gameEvents">GameEvents</param>
        protected abstract void startObjective(GameEvents gameEvents);
        /// <summary>
        /// Method called to evaluate objective and return readable objective text.
        /// </summary>
        /// <returns>string to be displayed</returns>
        public abstract string GetDisplayText();
    }
}