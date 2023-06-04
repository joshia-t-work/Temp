using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKP.StateMachineSystem
{
    /// <summary>
    /// Use this instead of MonoBehaviour in order to use the StateMachineSystem
    /// </summary>
    public abstract class StateMachineMono<TData> : MonoBehaviour, IMonoBehaviour
    {
        #region Variables

        [SerializeField]
        private State<TData>[] _states;

        [SerializeField, InitializationField]
        private string _initialState;

        [SerializeField]
        [ReadOnly]
        private State<TData> _currentState;
        public State<TData> CurrentState => _currentState;

        private string _currentStateName;
        public string CurrentStateName => _currentStateName;

        /// <summary>
        /// Data for the statemachine
        /// </summary>
        [HideInInspector]
        public abstract TData data { get; }

        #endregion

        #region MonoBehaviour

        public virtual void Awake()
        {
            for (int i = 0; i < _states.Length; i++)
            {
                _states[i] = Instantiate(_states[i]);
                _states[i]?.SetStateMachine(this);
                _states[i]?.Awake();
            }
            SetState(_initialState);
        }
        public virtual void FixedUpdate()
        {
            _currentState?.FixedUpdate();
        }
        public virtual void LateUpdate()
        {
            _currentState?.LateUpdate();
        }
        public virtual void OnAnimatorIK(int layerIndex)
        {
            _currentState?.OnAnimatorIK(layerIndex);
        }
        public virtual void OnAnimatorMove()
        {
            _currentState?.OnAnimatorMove();
        }
        public virtual void OnApplicationFocus(bool focus)
        {
            _currentState?.OnApplicationFocus(focus);
        }
        public virtual void OnApplicationPause(bool pause)
        {
            _currentState?.OnApplicationPause(pause);
        }
        public virtual void OnApplicationQuit()
        {
            _currentState?.OnApplicationQuit();
        }
        public virtual void OnAudioFilterRead(float[] data, int channels)
        {
            _currentState?.OnAudioFilterRead(data, channels);
        }
        public virtual void OnBecameInvisible()
        {
            _currentState?.OnBecameInvisible();
        }
        public virtual void OnBecameVisible()
        {
            _currentState?.OnBecameVisible();
        }
        public virtual void OnCollisionEnter(Collision collision)
        {
            _currentState?.OnCollisionEnter(collision);
        }
        public virtual void OnCollisionEnter2D(Collision2D collision)
        {
            _currentState?.OnCollisionEnter2D(collision);
        }
        public virtual void OnCollisionExit(Collision collision)
        {
            _currentState?.OnCollisionExit(collision);
        }
        public virtual void OnCollisionExit2D(Collision2D collision)
        {
            _currentState?.OnCollisionExit2D(collision);
        }
        public virtual void OnCollisionStay(Collision collision)
        {
            _currentState?.OnCollisionStay(collision);
        }
        public virtual void OnCollisionStay2D(Collision2D collision)
        {
            _currentState?.OnCollisionStay2D(collision);
        }
        public virtual void OnControllerColliderHit(ControllerColliderHit hit)
        {
            _currentState?.OnControllerColliderHit(hit);
        }
        public virtual void OnDestroy()
        {
            _currentState?.OnDestroy();
        }
        public virtual void OnDisable()
        {
            _currentState?.OnDisable();
        }
        public virtual void OnDrawGizmos()
        {
            _currentState?.OnDrawGizmos();
        }
        public virtual void OnDrawGizmosSelected()
        {
            _currentState?.OnDrawGizmosSelected();
        }
        public virtual void OnEnable()
        {
            _currentState?.OnEnable();
        }
        public virtual void OnGUI()
        {
            _currentState?.OnGUI();
        }
        public virtual void OnJointBreak(float breakForce)
        {
            _currentState?.OnJointBreak(breakForce);
        }
        public virtual void OnJointBreak2D(Joint2D joint)
        {
            _currentState?.OnJointBreak2D(joint);
        }
        public virtual void OnMouseDown()
        {
            _currentState?.OnMouseDown();
        }
        public virtual void OnMouseDrag()
        {
            _currentState?.OnMouseDrag();
        }
        public virtual void OnMouseEnter()
        {
            _currentState?.OnMouseEnter();
        }
        public virtual void OnMouseExit()
        {
            _currentState?.OnMouseExit();
        }
        public virtual void OnMouseOver()
        {
            _currentState?.OnMouseOver();
        }
        public virtual void OnMouseUp()
        {
            _currentState?.OnMouseUp();
        }
        public virtual void OnMouseUpAsButton()
        {
            _currentState?.OnMouseUpAsButton();
        }
        public virtual void OnParticleCollision(GameObject other)
        {
            _currentState?.OnParticleCollision(other);
        }
        public virtual void OnParticleSystemStopped()
        {
            _currentState?.OnParticleSystemStopped();
        }
        public virtual void OnParticleTrigger()
        {
            _currentState?.OnParticleTrigger();
        }
        public virtual void OnParticleUpdateJobScheduled()
        {
            _currentState?.OnParticleUpdateJobScheduled();
        }
        public virtual void OnPostRender()
        {
            _currentState?.OnPostRender();
        }
        public virtual void OnPreCull()
        {
            _currentState?.OnPreCull();
        }
        public virtual void OnPreRender()
        {
            _currentState?.OnPreRender();
        }
        public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            _currentState?.OnRenderImage(source, destination);
        }
        public virtual void OnRenderObject()
        {
            _currentState?.OnRenderObject();
        }
        public virtual void OnTransformChildrenChanged()
        {
            _currentState?.OnTransformChildrenChanged();
        }
        public virtual void OnTransformParentChanged()
        {
            _currentState?.OnTransformParentChanged();
        }
        public virtual void OnTriggerEnter(Collider other)
        {
            _currentState?.OnTriggerEnter(other);
        }
        public virtual void OnTriggerEnter2D(Collider2D collision)
        {
            _currentState?.OnTriggerEnter2D(collision);
        }
        public virtual void OnTriggerExit(Collider other)
        {
            _currentState?.OnTriggerExit(other);
        }
        public virtual void OnTriggerExit2D(Collider2D collision)
        {
            _currentState?.OnTriggerExit2D(collision);
        }
        public virtual void OnTriggerStay(Collider other)
        {
            _currentState?.OnTriggerStay(other);
        }
        public virtual void OnTriggerStay2D(Collider2D collision)
        {
            _currentState?.OnTriggerStay2D(collision);
        }
        public virtual void OnValidate()
        {
            _currentState?.OnValidate();
        }
        public virtual void OnWillRenderObject()
        {
            _currentState?.OnWillRenderObject();
        }
        public virtual void Reset()
        {
            _currentState?.Reset();
        }
        public virtual void Start()
        {
            _currentState?.Start();
        }
        public virtual void Update()
        {
            _currentState?.Update();
        }

        #endregion

        public void SetState(string state)
        {
            string statefrom = _currentState?.Name;
            _currentState?.OnStateExit(state);
            for (int i = 0; i < _states.Length; i++)
            {
                if (_states[i].Name == state)
                {
                    _currentState = _states[i];
                    _currentState?.OnStateEnter(statefrom);
                    _currentStateName = _currentState?.name;
                    return;
                }
            }
            Debug.LogWarning($"{state} is not found!", this);
        }
    }
}