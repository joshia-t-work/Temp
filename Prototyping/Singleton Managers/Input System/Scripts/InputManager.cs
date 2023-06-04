using DKP.ObserverSystem;
using DKP.ObserverSystem.GameEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DKP.Input
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager I;
        [SerializeField] CachedGameEventVector2 _movementInput;
        public static CachedGameEventVector2 MovementInput { get { return I._movementInput; } }
        [SerializeField] CachedGameEventBool _dashInput;
        public static CachedGameEventBool DashInput { get { return I._dashInput; } }
        [SerializeField] CachedGameEventBool _attackInput;
        public static CachedGameEventBool AttackInput { get { return I._attackInput; } }
        //public static Observable<Vector2> MovementInput = new Observable<Vector2>(Vector2.zero);
        //public static Observable<bool> DashInput = new Observable<bool>(false);
        //public static Observable<bool> AttackInput = new Observable<bool>(false);

        #region UI

        [SerializeField] CachedGameEventVector2 _pointerInput;
        public static CachedGameEventVector2 PointerInput { get { return I._pointerInput; } }
        [SerializeField] CachedGameEventBool _leftClickInput;
        public static CachedGameEventBool LeftClickInput { get { return I._leftClickInput; } }
        [SerializeField] CachedGameEventBool _middleClickInput;
        public static CachedGameEventBool MiddleClickInput { get { return I._middleClickInput; } }
        [SerializeField] CachedGameEventBool _rightClickInput;
        public static CachedGameEventBool RightClickInput { get { return I._rightClickInput; } }
        //public static Observable<Vector2> PointerInput = new Observable<Vector2>(Vector2.zero);
        //public static Observable<bool> LeftClickInput = new Observable<bool>(false);
        //public static Observable<bool> MiddleClickInput = new Observable<bool>(false);
        //public static Observable<bool> RightClickInput = new Observable<bool>(false);

        #endregion

        private void Awake()
        {
            I = this;
        }

        public void Move(InputAction.CallbackContext context)
        {
            MovementInput.Invoke(context.ReadValue<Vector2>());
        }
        public void Dash(InputAction.CallbackContext context)
        {
            DashInput.Invoke(context.ReadValueAsButton());
        }
        public void Attack(InputAction.CallbackContext context)
        {
            AttackInput.Invoke(context.ReadValueAsButton());
        }
        public void Point(InputAction.CallbackContext context)
        {
            PointerInput.Invoke(context.ReadValue<Vector2>());
        }
        public void RightClick(InputAction.CallbackContext context)
        {
            RightClickInput.Invoke(context.ReadValueAsButton());
        }
        public void MiddleClick(InputAction.CallbackContext context)
        {
            MiddleClickInput.Invoke(context.ReadValueAsButton());
        }
        public void LeftClick(InputAction.CallbackContext context)
        {
            LeftClickInput.Invoke(context.ReadValueAsButton());
        }
    }
}