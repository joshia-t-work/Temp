using UnityEngine;

namespace DKP.StateMachineSystem
{
    /// <summary>
    /// Represents a State in a StateMachine. OnEnable is OnStateEnter. OnDisable is OnStateExit.
    /// </summary>
    /// <typeparam name="TStateMachine">State Machine class</typeparam>
    /// <typeparam name="TData">Data class</typeparam>
    public abstract class StateWithType<TStateMachine, TData> : State<TData>
        where TStateMachine : StateMachineMono<TData>
    {
        /// <summary>
        /// Accessor to the unique StateMachine Instance
        /// </summary>
        protected TStateMachine ThisSTM => (TStateMachine)STM;
    }
}