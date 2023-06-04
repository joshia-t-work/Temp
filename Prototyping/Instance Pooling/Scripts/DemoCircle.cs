using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKP.Debugging
{
    public class DemoCircle : MonoBehaviour
    {
        void Update()
        {
            if (transform.position.y < 0)
            {
                DemoInstancePooling.Instance.Circles.DestroyToPool(transform);
            }
        }
    }
}