#if DEVELOPMENT_BUILD || UNITY_EDITOR
using DKP.FileSystem;
using DKP.Input;
using DKP.ObserverSystem;
using DKP.SaveSystem.Data;
using SimpleFileBrowser;
using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DKP.Development.LevelEditor.Data
{
    public class DevSerializedImage : DevBaseDataContainer
	{
		public override ISerializableData Data => serializableImage;
		[SerializeField]
        Image image;

		Sprite defaultSprite;

		SerializableImage serializableImage;

        protected override void Awake()
        {
			defaultSprite = image.sprite;
		}

        public void SetImageByDialog()
		{
			FileExplorer.LoadImage((bytes) =>
			{
				serializableImage.SetImage(bytes);
				if (bytes == null)
				{
					image.sprite = defaultSprite;
				} else
				{
					image.sprite = serializableImage.Sprite;
				}
				ChangeEvent.Invoke(serializableImage);
			});
		}

		public override void SetData(ISerializableData data)
		{
			SerializableImage newImage = (SerializableImage)data;
			serializableImage = newImage;
			if (serializableImage.hasImage)
			{
				image.sprite = serializableImage.Sprite;
			}
		}
	}
}
#endif