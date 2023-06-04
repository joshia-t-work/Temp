using DKP.ObserverSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DKP.Debugging
{
    public class DemoNumberDisplay : MonoBehaviour
    {
        [SerializeField] TMP_Text tmpText;
        public void SetText(string text)
        {
            tmpText.text = text;
        }
    }
}