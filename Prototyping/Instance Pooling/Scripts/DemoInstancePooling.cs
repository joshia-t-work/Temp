using DKP.Input;
using DKP.InstancePooling;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DKP.Debugging
{
    public class DemoInstancePooling : MonoBehaviour
    {
        public static DemoInstancePooling Instance;
        [SerializeField]
        Transform _transform;
        [SerializeField]
        InstancePool _circles;
        public InstancePool Circles { get { return _circles; } }
        private void Awake()
        {
            Instance = this;
            _circles = new InstancePool(_transform, transform);
            InputManager.AttackInput.Value1.AddObserver(OnClickListener);
        }

        private void OnDestroy()
        {
            InputManager.AttackInput.Value1.RemoveObserver(OnClickListener);
        }

        private void OnClickListener(bool click)
        {
            if (click)
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(InputManager.PointerInput.Value1.Value);
                _circles.InstantiateFromPool(pos, Quaternion.identity);
            }
        }
    }
}