using UnityEngine;

namespace DKP.StateMachineSystem
{
    /// <summary>
    /// Represents a State in a StateMachine. OnEnable is OnStateEnter. OnDisable is OnStateExit.
    /// </summary>
    /// <typeparam name="TData">Data class</typeparam>
    public abstract class State<TData> : ScriptableObject, IMonoBehaviour
    {
        /// <summary>
        /// Name to refer to state.
        /// </summary>
        /// <remarks>must be UNIQUE in STM</remarks>
        public abstract string Name { get; }
        /// <summary>
        /// Accessor to the StateMachine Instance
        /// </summary>
        protected StateMachineMono<TData> STM => _stateMachine;
        /// <summary>
        /// Accessor to the Data Instance
        /// </summary>
        protected TData Data;
        private StateMachineMono<TData> _stateMachine;
        public void ChangeState(string state)
        {
            _stateMachine.SetState(state);
        }
        public void SetStateMachine(StateMachineMono<TData> stateMachine)
        {
            _stateMachine = stateMachine;
            Data = stateMachine.data;
        }

        public virtual void OnStateEnter(string stateFrom) { }

        public virtual void OnStateExit(string stateTo) { }

        #region MonoBehaviour

        /// <summary>
        /// Awake() will ALWAYS be called regardless if it is the current state or not. Use this for initializing.
        /// </summary>
        public virtual void Awake() { }

        public virtual void FixedUpdate() { }

        public virtual void LateUpdate() { }

        public virtual void OnAnimatorIK(int layerIndex) { }

        public virtual void OnAnimatorMove() { }

        public virtual void OnApplicationFocus(bool focus) { }

        public virtual void OnApplicationPause(bool pause) { }

        public virtual void OnApplicationQuit() { }

        public virtual void OnAudioFilterRead(float[] data, int channels) { }

        public virtual void OnBecameInvisible() { }

        public virtual void OnBecameVisible() { }

        public virtual void OnCollisionEnter(Collision collision) { }

        public virtual void OnCollisionEnter2D(Collision2D collision) { }

        public virtual void OnCollisionExit(Collision collision) { }

        public virtual void OnCollisionExit2D(Collision2D collision) { }

        public virtual void OnCollisionStay(Collision collision) { }

        public virtual void OnCollisionStay2D(Collision2D collision) { }

        public virtual void OnControllerColliderHit(ControllerColliderHit hit) { }

        public virtual void OnDestroy() { }

        /// <summary>
        /// Also called during destroy
        /// </summary>
        public virtual void OnDisable() { }

        public virtual void OnDrawGizmos() { }

        public virtual void OnDrawGizmosSelected() { }

        /// <summary>
        /// Also called during awake
        /// </summary>
        public virtual void OnEnable() { }

        public virtual void OnGUI() { }

        public virtual void OnJointBreak(float breakForce) { }

        public virtual void OnJointBreak2D(Joint2D joint) { }

        public virtual void OnMouseDown() { }

        public virtual void OnMouseDrag() { }

        public virtual void OnMouseEnter() { }

        public virtual void OnMouseExit() { }

        public virtual void OnMouseOver() { }

        public virtual void OnMouseUp() { }

        public virtual void OnMouseUpAsButton() { }

        public virtual void OnParticleCollision(GameObject other) { }

        public virtual void OnParticleSystemStopped() { }

        public virtual void OnParticleTrigger() { }

        public virtual void OnParticleUpdateJobScheduled() { }

        public virtual void OnPostRender() { }

        public virtual void OnPreCull() { }

        public virtual void OnPreRender() { }

        public virtual void OnRenderImage(RenderTexture source, RenderTexture destination) { }

        public virtual void OnRenderObject() { }

        public virtual void OnTransformChildrenChanged() { }

        public virtual void OnTransformParentChanged() { }

        public virtual void OnTriggerEnter(Collider other) { }

        public virtual void OnTriggerEnter2D(Collider2D collision) { }

        public virtual void OnTriggerExit(Collider other) { }

        public virtual void OnTriggerExit2D(Collider2D collision) { }

        public virtual void OnTriggerStay(Collider other) { }

        public virtual void OnTriggerStay2D(Collider2D collision) { }

        public virtual void OnValidate() { }

        public virtual void OnWillRenderObject() { }

        public virtual void Reset() { }

        public virtual void Start() { }

        public virtual void Update() { }

        #endregion
    }
}