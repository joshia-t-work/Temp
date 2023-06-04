using DKP.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKP.Debugging
{
    public class DemoColliderScript : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(InputManager.PointerInput.Value1.Value);
            mousePos = new Vector3(mousePos.x, mousePos.y, 0);
            transform.position = mousePos;
        }
    }
}