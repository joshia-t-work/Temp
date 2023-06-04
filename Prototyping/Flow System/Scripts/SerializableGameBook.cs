using System;
using System.Collections.Generic;
using UnityEngine;

namespace DKP.SaveSystem.Data
{
    /// <summary>
    /// Represents data for one whole scene/act where Characters and Backgrounds persist
    /// </summary>
    [Serializable]
    public class SerializableGameBook : ISerializableData, IData
    {
        /// <summary>
        /// Default saved filename
        /// </summary>
        public string DataFileName => "Game Book Data.dkpbook";

        /// <summary>
        /// The version of the save file
        /// </summary>
        public SerializableVersion Version = new SerializableVersion(0, 0);

        [Obsolete("Use EditorCamera instead, deprecated as of 1.5")]
        [SerializeField, HideInInspector, ReadOnly]
        public SerializableVector3 WorldPos = new SerializableVector3(0f, 0f, -10f);

        /// <summary>
        /// The position of the controls help box
        /// </summary>
        [Obsolete("Deprecated as of 2.0"), SerializeField, HideInInspector, ReadOnly]
        public SerializableVector3 ControlsHelpWorldPos = new SerializableVector3(0f, 0f, 0f);

        /// <summary>
        /// The camera position of the story editor mode
        /// </summary>
        [SerializeField, ReadOnly]
        public SerializableCameraPosition EditorCamera = new SerializableCameraPosition(0f, 0f, -10f);

        /// <summary>
        /// The camera position of the level designer mode
        /// </summary>
        [SerializeField, ReadOnly]
        public SerializableCameraPosition DesignerCamera = new SerializableCameraPosition(0f, 0f, -10f);

        [Obsolete("Use EditorCamera instead, deprecated as of 1.5")]
        [SerializeField, HideInInspector, ReadOnly]
        public float CamZoom;

        /// <summary>
        /// A scene object containing images
        /// </summary>
        [Obsolete("Use Images instead, deprecated as of 2.0"), SerializeField, HideInInspector, ReadOnly]
        public SerializableImageContainer SceneSetting = new SerializableImageContainer();

        [Obsolete("Use ActionScript instead, deprecated as of 2.0")]
        [SerializeField, HideInInspector, ReadOnly]
        public SerializableMainThread MainThread = new SerializableMainThread();

        [Obsolete("Use ActionScript instead, deprecated as of 2.0")]
        [SerializeField, HideInInspector, ReadOnly]
        public List<SerializableScript> Scripts = new List<SerializableScript>();

        [Obsolete("Use Images instead, deprecated as of 2.0")]
        [SerializeField, HideInInspector, ReadOnly]
        public List<SerializableCharacter> Characters = new List<SerializableCharacter>();

        /// <summary>
        /// Get background images
        /// </summary>
        /// <remarks>Can be null</remarks>
        public SerializableImageContainer Backgrounds => Images[SerializableImageContainer.BACKGROUND_CONTAINER_NAME];
        /// <summary>
        /// A list of character objects that contains expressions
        /// </summary>
        [SerializeField, ReadOnly]
        public ImageContainerContainer Images = new ImageContainerContainer();

        [Obsolete("Use ActionScript instead, deprecated as of 2.0")]
        [SerializeField, HideInInspector, ReadOnly]
        public List<SerializableCharacterScript> CharacterScripts = new List<SerializableCharacterScript>();

        [Obsolete("Use ActionScript instead, deprecated as of 2.0")]
        [SerializeField, HideInInspector, ReadOnly]
        public List<SerializableTrigger> Triggers = new List<SerializableTrigger>();

        public SerializableActionScript Start => ActionScripts[SerializableActionScript.START_SCRIPT_NAME];
        /// <summary>
        /// ActionScripts container
        /// </summary>
        [SerializeField, ReadOnly]
        public ActionScriptContainer ActionScripts = new ActionScriptContainer();

        public SerializableGameBook()
        {
            SerializableActionScript start = new SerializableActionScript();
            start.SetObjectName(SerializableActionScript.START_SCRIPT_NAME);
            start.WorldPosition = new SerializableVector3(0, 0, 0);
            ActionScripts.ActionScripts.Add(start);
            SerializableImageContainer background = new SerializableImageContainer();
            background.SetObjectName(SerializableImageContainer.BACKGROUND_CONTAINER_NAME);
            background.WorldPosition = new SerializableVector3(310, 0, 0);
            Images.ImageContainers.Add(background);
        }

        /// <summary>
        /// Deserializes Characters' images and SceneSetting's images
        /// </summary>
        /// <remarks>Must be run in Unity thread</remarks>
        public void Deserialize()
        {
            for (int i = 0; i < Images.Count; i++)
            {
                Images[i].Deserialize();
            }
            //SceneSetting.Deserialize();
        }
    }
}