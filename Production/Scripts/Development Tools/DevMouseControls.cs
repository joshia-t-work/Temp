#if DEVELOPMENT_BUILD || UNITY_EDITOR
using DKP.CameraSystem;
using DKP.Input;
using DKP.ObserverSystem;
using DKP.SaveSystem.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace DKP.Development.LevelEditor
{
    public abstract class DevMouseControls : MonoBehaviour
    {
        public static List<RaycastResult> MouseHit = new List<RaycastResult>();
        public static Vector3 rightClickPos;

        [SerializeField]
        Canvas _world;
        RectTransform _worldRect;
        Transform _worldPivot;

        [SerializeField]
        float _cameraMinSize;

        protected Camera _camera;
        protected CameraSTM _cameraSTM;

        [SerializeField]
        GameObject _rightClickPanel;

        public static DevRightclickable rightClicked = null;

        float scrollMagnifier;
        Observable<bool> f11key = new Observable<bool>(false);
        Vector2 _mousePos;
        Vector3 _heldPos;
        float _camOrthoSize;
        Vector3 _camPos;
        // TODO: Add Selection Box
        //List<DevRaycastable> _selected;
        DevRaycastable _held;
        [SerializeField, ReadOnly]
        bool drag;
        protected virtual void Awake()
        {
            _worldRect = _world.GetComponent<RectTransform>();
            _worldPivot = _worldRect.GetChild(0);
            f11key = new Observable<bool>(false);
            _rightClickPanel.SetActive(false);
            InputManager.LeftClickInput.Value1.AddObserver(clickInputListener);
            InputManager.MiddleClickInput.Value1.AddObserver(middleClickInputListener);
            InputManager.RightClickInput.Value1.AddObserver(rightClickInputListener);
            f11key.AddObserver(f11keyPressListener);
            StartCoroutine(Start());
        }

        private IEnumerator Start()
        {
            _camera = Camera.main;
            _cameraSTM = _camera.GetComponentInParent<CameraSTM>();
            _camOrthoSize = _camera.orthographicSize;
            _world.worldCamera = _camera;
            _camPos = _cameraSTM.transform.position;
            yield return new WaitForSeconds(1f);
        }

        private void OnDestroy()
        {
            InputManager.LeftClickInput.Value1.RemoveObserver(clickInputListener);
            InputManager.MiddleClickInput.Value1.AddObserver(middleClickInputListener);
            InputManager.RightClickInput.Value1.RemoveObserver(rightClickInputListener);
            //InputManager.PointerInput.Value1.RemoveObserver(moveMouseListener);
            f11key.RemoveObserver(f11keyPressListener);
        }
        protected virtual void setCamOrtho(float val) {
            _camOrthoSize = val;
        }
        protected virtual void setCamPos(Vector3 pos)
        {
            _camPos = pos;
        }

        private void clickInputListener(bool clicked)
        {
            if (clicked)
            {
                if (MouseHit.Count == 0)
                {
                    _rightClickPanel.SetActive(false);
                }
                else
                {
                    _held = MouseHit[0].gameObject.GetComponent<DevRaycastable>();
                    if (_held is DevHandle)
                    {
                        _heldPos = ((DevHandle)_held).HandleParent.position;
                    }
                    //if (_held is DevLinker)
                    //{
                    //    DevLinker heldLinker = (DevLinker)_held;
                    //    if (heldLinker.LinkType == LinkType.To)
                    //    {
                    //        _held = null;
                    //    }
                    //    if (heldLinker.LinkedObject != null)
                    //    {
                    //        //heldLinker.Link.SetLink(null);
                    //        heldLinker.SetLink(null);
                    //    }
                    //}
                }
            }
            else
            {
                //if (_held is DevLinker)
                //{
                //    if (MouseHit.Count > 0)
                //    {
                //        DevRaycastable targetObject = MouseHit[0].gameObject.GetComponent<DevRaycastable>();
                //        if (targetObject is DevLinker)
                //        {
                //            if (((DevLinker)targetObject).LinkType == LinkType.To)
                //            {
                //                DevLinker target = ((DevLinker)targetObject);
                //                DevLinker held = ((DevLinker)_held);
                //                //target.SetLink(held);
                //                held.SetLink(target);
                //            }
                //        }
                //    }
                //}
                if (_held != null)
                {
                    _held = null;
                }
            }
        }

        private void middleClickInputListener(bool clicked)
        {
            if (clicked)
            {
                if (MouseHit.Count == 0)
                {
                    drag = true;
                }
            }
            else
            {
                drag = false;
            }
        }

        private void rightClickInputListener(bool clicked)
        {
            if (clicked)
            {
                rightClicked = null;
                _rightClickPanel.transform.position = _mousePos;
                if (MouseHit.Count == 0)
                {
                    _rightClickPanel.SetActive(true);
                    rightClickPos = Camera.main.ScreenToWorldPoint(InputManager.PointerInput.Value1.Value);
                    rightClickPos = new Vector3(gridRound(rightClickPos.x), gridRound(rightClickPos.y), gridRound(rightClickPos.z));
                }
                else
                {
                    rightClicked = MouseHit[0].gameObject.GetComponent<DevRightclickable>();
                    if (rightClicked == null)
                    {
                        rightClicked = MouseHit[0].gameObject.GetComponentInParent<DevRightclickable>();
                    }
                    if (rightClicked != null)
                    {
                        _rightClickPanel.SetActive(true);
                        rightClickPos = Camera.main.ScreenToWorldPoint(InputManager.PointerInput.Value1.Value);
                        rightClickPos = new Vector3(gridRound(rightClickPos.x), gridRound(rightClickPos.y), gridRound(rightClickPos.z));
                    }
                }
            }
        }
        protected virtual float gridRound(float val)
        {
            return Mathf.Round(val / 10f) * 10f;
        }

        public void SetData(SerializableCameraPosition camPos)
        {
            setCamPos(new Vector3(camPos.Position.X, camPos.Position.Y, camPos.Position.Z));
            setCamOrtho(Mathf.Max(_cameraMinSize, camPos.Zoom));

            //float height = 2f * _camera.orthographicSize;
            //float width = height * _camera.aspect;
            //_worldRect.sizeDelta = new Vector2(width, height);
            //Vector3 childPos = _worldPivot.position;
            //_worldRect.position = _cameraSTM.transform.position;
            //_worldPivot.position = childPos;
        }

        private void f11keyPressListener(bool obj)
        {
            if (obj)
            {
                Screen.fullScreen = !Screen.fullScreen;
            }
        }

        void Update()
        {
            f11key.Value = Keyboard.current.f11Key.isPressed;
            scrollMagnifier = Mathf.Lerp(scrollMagnifier, 0f, Time.deltaTime * 2f);
        }

        private void FixedUpdate()
        {
            HandleMouseMovement();
            HandleScroll();
        }

        private void LateUpdate()
        {
            HandleCamera();
        }

        private void HandleMouseMovement()
        {
            Vector2 mousePos = InputManager.PointerInput.Value1.Value;

            EventSystem eventSystem = EventSystem.current;
            PointerEventData pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.position = _mousePos;
            EventSystem.current.RaycastAll(pointerEventData, MouseHit);

            Vector3 moveVec = (mousePos - _mousePos) * _camOrthoSize / Screen.height * 2f;
            if (drag)
            {
                setCamPos(_camPos - moveVec);
            }
            else if (_held != null)
            {
                if (_held is DevHandle)
                {
                    _heldPos += moveVec;
                    ((DevHandle)_held).HandleParent.position = new Vector3(gridRound(_heldPos.x), gridRound(_heldPos.y), gridRound(_heldPos.z));
                }
                //if (_held is DevLinker)
                //{
                //    ((DevLinker)_held).SetHeld(true, worldMousePos);
                //}
            }
            _mousePos = mousePos;
        }

        private void HandleScroll()
        {
            float scrollVal = Mouse.current.scroll.ReadValue().y;
            if (scrollVal != 0f)
            {
                const float SCROLL_MULT = 0.2f;
                const float SCROLL_MAGNIFIER_MULT = 0.6f;
                float scrollAdded = (_camOrthoSize / 300f * scrollMagnifier * SCROLL_MAGNIFIER_MULT + Mathf.Abs(scrollVal * SCROLL_MULT)) * Mathf.Sign(scrollVal);
                Vector3 panMovement = _camera.ScreenToWorldPoint(_mousePos) - _camPos;
                panMovement = new Vector3(panMovement.x, panMovement.y, 0f);
                float newSize = Mathf.Max(_cameraMinSize, _camOrthoSize - scrollAdded);
                setCamPos(_camPos - panMovement / _camOrthoSize * (newSize - _camOrthoSize));
                setCamOrtho(newSize);

                scrollMagnifier += 1;
            }
        }

        private void HandleCamera()
        {
            _cameraSTM.transform.position = _camPos;
            _camera.orthographicSize = _camOrthoSize;
        }
    }
}
#endif