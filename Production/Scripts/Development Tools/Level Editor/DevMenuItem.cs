#if DEVELOPMENT_BUILD || UNITY_EDITOR
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DKP.Development.LevelEditor
{
    public class DevMenuItem : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] TMP_Text _displayText;
        [SerializeField] Button _menuButtonPrefab;
        [SerializeField] Transform _menuOpenGO;
        RectTransform _menuOpenRT;

        [Header("Values")]
        [SerializeField] string _displayedText;
        [SerializeField] ButtonEvents[] _events;

        [ContextMenu("Apply values to gameobject")]
        private void Apply()
        {
            if (_displayText)
                _displayText.text = _displayedText;
            gameObject.name = _displayedText;
            if (_menuOpenGO)
            {
                _menuOpenGO.gameObject.SetActive(true);
                for (int i = _menuOpenGO.childCount - 1; i >= 0; i--)
                {
                    DestroyImmediate(_menuOpenGO.GetChild(i).gameObject);
                }
                for (int i = 0; i < _events.Length; i++)
                {
                    int index = i; // create a copy of i for use in lambda expression
                    Button button = Instantiate(_menuButtonPrefab, _menuOpenGO);
                    button.gameObject.name = _events[i].DisplayName;
                    button.GetComponentInChildren<TMP_Text>().text = _events[i].DisplayName;
                    button.onClick = _events[index].OnClick;
                }
                _menuOpenRT = _menuOpenGO.GetComponent<RectTransform>();
                LayoutRebuilder.ForceRebuildLayoutImmediate(_menuOpenRT);
                _menuOpenGO.gameObject.SetActive(false);
            }
        }

        public void UIToggleMenu()
        {
            if (!_menuOpenGO.gameObject.activeSelf)
            {
                StartCoroutine(redraw());
            }
            _menuOpenGO.gameObject.SetActive(!_menuOpenGO.gameObject.activeSelf);
        }

        IEnumerator redraw()
        {
            yield return new WaitForEndOfFrame();
            _menuOpenRT = _menuOpenGO.GetComponent<RectTransform>();
            LayoutRebuilder.MarkLayoutForRebuild(_menuOpenRT);
        }

        [System.Serializable]
        class ButtonEvents
        {
            public string DisplayName;
            public Button.ButtonClickedEvent OnClick;
        }
    }
}
#endif