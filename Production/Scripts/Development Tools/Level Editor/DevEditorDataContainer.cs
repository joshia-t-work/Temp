#if DEVELOPMENT_BUILD || UNITY_EDITOR
using DKP.Development.LevelEditor.Data;
using DKP.Input;
using DKP.ObserverSystem;
using DKP.ObserverSystem.GameEvents;
using DKP.SaveSystem;
using DKP.SaveSystem.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace DKP.Development.LevelEditor
{
    public class DevEditorDataContainer : MonoBehaviour
    {
        [SerializeField]
        public DevUpdateLog VersionHistory;

        [HideInInspector]
        public SerializableVersion Version;

        public static string EditingWindow = "Edit";

        [SerializeField]
        TMP_Text VersionDisplay;

        [SerializeField]
        DevLoader Loader;

        public static DevEditorDataContainer Instance;
        public static SerializableGameBook GameBookData => Instance._gameBook;

        public static Queue<GameObject> Objects = new Queue<GameObject>();

        //public static Dictionary<ISerializableData, IDevDataContainer> Components = new Dictionary<ISerializableData, IDevDataContainer>();
        //public static Dictionary<SerializableImageContainer, DevCharacter> Characters = new Dictionary<SerializableImageContainer, DevCharacter>();
        //public static Dictionary<SerializableCharacterScript, DevCharacterScript> CharacterScripts = new Dictionary<SerializableCharacterScript, DevCharacterScript>();
        //public static Dictionary<SerializableTrigger, DevTrigger> Links = new Dictionary<SerializableTrigger, DevTrigger>();

        //public static ObservableList<SerializableCharacter> CharactersObserver;
        //public static ObservableList<SerializableCharacterScript> CharacterScriptsObserver;
        //public static ObservableList<SerializableActionScript> ActionScriptsObserver;
        //public static ObservableList<SerializableTrigger> TriggersObserver;

        private static Dictionary<Type, DevPrefabInstantiator> Instantiators = new Dictionary<Type, DevPrefabInstantiator>();

        static GameEvent<object> _changeEvent;
        public static GameEvent<object> ChangeEvent
        {
            get
            {
                if (_changeEvent == null)
                {
                    _changeEvent = new GameEvent<object>();
                }
                return _changeEvent;
            }
        }

        [SerializeField, ReadOnly]
        private SerializableGameBook _gameBook;
        [SerializeField]
        DevMouseControls camerahandler;
        //[SerializeField]
        //DevPrefabInstantiator characterScript;
        //[SerializeField]
        //DevPrefabInstantiator trigger;
        [SerializeField]
        DevPrefabInstantiator imageContainer;
        [SerializeField]
        DevPrefabInstantiator actionScript;
        //[SerializeField]
        //DevPrefabInstantiator mainThread;
        //[SerializeField]
        //DevPrefabInstantiator script;
        public static void TrySetData(SerializableGameBook gameBookData)
        {
            if (gameBookData == null)
            {
                Debug.LogWarning("Save file corrupt.");
                gameBookData = new SerializableGameBook();
            }
            Instance.Loader.SetData(gameBookData);
            if (gameBookData.Version == null)
            {
                Debug.LogWarning("Save file will not be loaded properly due to major outdate.");
                Instance.Loader.gameObject.SetActive(true);
                return;
            }
            else
            {
                if (Instance.Version > gameBookData.Version)
                {
                    if (Instance.Version.Major > gameBookData.Version.Major)
                    {
                        Debug.LogWarning("Save file will not be loaded properly due to major outdate.");
                        Instance.Loader.gameObject.SetActive(true);
                        return;
                    }
                    else
                    {
                        Debug.Log("Attempting to load outdated save file");
                    }
                }
            }
            SetData(gameBookData);
        }
        public static void SetData(SerializableGameBook gameBookData)
        {
            DevDKPUpdate.Update(gameBookData);
            Instance.VersionDisplay.text = $"Editor Version: {gameBookData.Version}";

            // Cleanup
            Queue<IDevDataContainer> _toLink = new Queue<IDevDataContainer>();

            //Components.Clear();
            //Characters.Clear();
            //CharacterScripts.Clear();
            //Links.Clear();
            while (Objects.Count > 0)
            {
                GameObject go = Objects.Dequeue();
                if (go != null)
                {
                    Destroy(go);
                }
            }

            // Init
            //LinkObservers();

            Instance._gameBook = gameBookData;
            switch (EditingWindow)
            {
                case "Design":
                    Instance.camerahandler.SetData(gameBookData.DesignerCamera);
                    break;
                default:
                    Instance.camerahandler.SetData(gameBookData.EditorCamera);
                    break;
            }

            //Transform scene = Instance.imageContainer.Add();
            //DevImageContainer sceneScript = scene.GetComponent<DevImageContainer>();
            //sceneScript.SetData(gameBookData.SceneSetting);
            //Components.Add(gameBookData.SceneSetting, sceneScript);

            //Transform mainThread = Instance.mainThread.Add();
            //DevMainThread mainThreadScript = mainThread.GetComponent<DevMainThread>();
            //mainThreadScript.SetData(gameBookData.MainThread);
            //Components.Add(gameBookData.MainThread, mainThreadScript);

            for (int i = 0; i < gameBookData.Images.Count; i++)
            {
                SerializableImageContainer character = gameBookData.Images[i];
                _toLink.Enqueue(CreateComponent<SerializableImageContainer, DevImageContainer>(character));
            }
            for (int i = 0; i < gameBookData.ActionScripts.Count; i++)
            {
                SerializableActionScript actionScript = gameBookData.ActionScripts[i];
                _toLink.Enqueue(CreateComponent<SerializableActionScript, DevActionScript>(actionScript));
            }
            //for (int i = 0; i < gameBookData.CharacterScripts.Count; i++)
            //{
            //    SerializableCharacterScript characterScript = gameBookData.CharacterScripts[i];
            //    _toLink.Enqueue(CreateComponent<SerializableCharacterScript, DevCharacterScript>(characterScript));
            //}
            //for (int i = 0; i < gameBookData.Triggers.Count; i++)
            //{
            //    SerializableTrigger trigger = gameBookData.Triggers[i];
            //    _toLink.Enqueue(CreateComponent<SerializableTrigger, DevTrigger>(trigger));
            //}
            //for (int i = 0; i < gameBookData.Scripts.Count; i++)
            //{
            //    SerializableScript script = gameBookData.Scripts[i];
            //    _toLink.Enqueue(CreateComponent<SerializableScript, DevScript>(script));
            //}

            while (_toLink.Count > 0)
            {
                _toLink.Dequeue().Link();
            }
            //sceneScript.Link();
            //mainThreadScript.Link();
        }
        public static TComponent GetComponent<TComponent>(ISerializableData data)
            where TComponent : IDevDataContainer
        {
            IDevDataContainer container = null;
            //Components.TryGetValue(data, out container);
            return (TComponent)container;
        }
        public static TComponent CreateComponent<TData, TComponent>(TData data)
            where TComponent : IDevDataContainer
            where TData : ISerializableData
        {
            Transform newTrans = Instantiators[typeof(TComponent)].Add();
            TComponent newComponent = newTrans.GetComponent<TComponent>();
            newComponent.SetData(data);
            //Components.Add(data, newComponent);
            return newComponent;
        }
        public static SerializableImageContainer AddImageContainer()
        {
            return Add(GameBookData.Images.ImageContainers);
        }
        public static SerializableActionScript AddActionScript()
        {
            SerializableActionScript newActionScript = Add(GameBookData.ActionScripts.ActionScripts);
            string prefix = "Untitled";
            string newName = prefix;
            if (GameBookData.ActionScripts[prefix] != null)
            {
                int suffix = 0;
                while (GameBookData.ActionScripts[$"{prefix} {suffix}"] != null)
                {
                    suffix += 1;
                }
                newName = $"{prefix} {suffix}";
            }
            newActionScript.SetObjectName(newName);
            return newActionScript;
        }
        //public static SerializableCharacterScript AddCharacterScript()
        //{
        //    return Add(GameBookData.CharacterScripts);
        //}
        //public static SerializableScript AddScript()
        //{
        //    return Add(GameBookData.Scripts);
        //}
        //public static SerializableTrigger AddTrigger()
        //{
        //    return Add(GameBookData.Triggers);
        //}
        public static TData Add<TData>(List<TData> dataContainer)
            where TData : new()
        {
            TData data = new TData();
            dataContainer.Add(data);
            ChangeEvent.Invoke(dataContainer);
            return data;
        }
        //public static DevCharacter InitializeNewCharacter(SerializableCharacter data)
        //{
        //    return CreateComponent<SerializableCharacter, DevCharacter>(data);
        //}
        //public static DevCharacterScript InitializeNewCharacterScript(SerializableCharacterScript characterScript)
        //{
        //    return CreateComponent<SerializableCharacterScript, DevCharacterScript>(characterScript);
        //}
        //public static DevTrigger InitializeNewTrigger(SerializableTrigger trigger)
        //{
        //    return CreateComponent<SerializableTrigger, DevTrigger>(trigger);
        //}

        private void Awake()
        {
            EditingWindow = "Edit";
            Version = new SerializableVersion(VersionHistory.CurrentVersion);
        }

        private void Start()
        {
            //Instantiators.Add(typeof(DevMainThread), mainThread);
            Instantiators.Add(typeof(DevImageContainer), imageContainer);
            Instantiators.Add(typeof(DevActionScript), actionScript);
            //Instantiators.Add(typeof(DevScript), script);
            //Instantiators.Add(typeof(DevCharacterScript), characterScript);
            //Instantiators.Add(typeof(DevTrigger), trigger);

            Instance = this;
            SerializableGameBook gameBook = new SerializableGameBook();
            string path = PlayerPrefs.GetString(DevEditorPrefs.EDITOR_SAVED_FILE_PATH, "");
            if (path == "")
            {
                if (gameBook == null)
                {
                    gameBook = new SerializableGameBook();
                }
                gameBook.Version = Version;
                SetData(gameBook);
            } else
            {
                DevFileHandler.TryLoadPath(path);
            }
        }

        private void OnDestroy()
        {
            Instantiators.Clear();
        }

        //private static void LinkObservers()
        //{
        //    CharactersObserver = new ObservableList<SerializableCharacter>(GameBookData.Characters);
        //    CharacterScriptsObserver = new ObservableList<SerializableCharacterScript>(GameBookData.CharacterScripts);
        //    TriggersObserver = new ObservableList<SerializableTrigger>(GameBookData.Triggers);
        //}
    }
}
#endif