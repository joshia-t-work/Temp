using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DKP.SceneSystem
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField]
        GameObject canvas;
        [SerializeField]
        Slider progress;
        public static SceneLoader Instance { get; private set; }
        private void Awake()
        {
            if (Instance == null)
            {
                if (transform.root == transform)
                {
                    DontDestroyOnLoad(gameObject);
                }
                Instance = this;
            } else
            {
                Destroy(gameObject);
            }
        }
        public static void LoadScene(string scene)
        {
            Instance.StartCoroutine(LoadSceneAsync(scene));
        }
        public static IEnumerator LoadSceneAsync(string scene)
        {
            Instance.canvas.SetActive(true);
            Instance.progress.value = 0f;
            AsyncOperation asyncOp = SceneManager.LoadSceneAsync(scene);
            while (!asyncOp.isDone)
            {
                Instance.progress.value = Mathf.Clamp01(asyncOp.progress / 0.9f);
                yield return null;
            }
            Instance.canvas.SetActive(false);
        }
    }
}