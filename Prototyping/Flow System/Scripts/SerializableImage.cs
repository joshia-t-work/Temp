using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace DKP.SaveSystem.Data
{
    /// <summary>
    /// Serialized Texture2D that can be accessed as a Texture or Sprite
    /// </summary>
    [Serializable]
    public class SerializableImage : ISerializableData
    {
        [SerializeField, HideInInspector]
        private byte[] image;

        [SerializeField, ReadOnly]
        public bool hasImage = false;

        [NonSerialized]
        bool isTextureDirty = true;
        [NonSerialized]
        bool isSpriteDirty = true;

        // not serialized
        /// <summary>
        /// The texture of the image
        /// </summary>
        public Texture2D Texture
        {
            get
            {
                if (texture == null || isTextureDirty)
                {
                    texture = new Texture2D(1, 1);
                    texture.LoadImage(image);
                }
                return texture;
            }
        }
        [NonSerialized]
        private Texture2D texture;
        /// <summary>
        /// The sprite of the image
        /// </summary>
        public Sprite Sprite
        {
            get
            {
                if (sprite == null || isSpriteDirty)
                {
                    isSpriteDirty = false;
                    Rect rec = new Rect(0, 0, Texture.width, Texture.height);
                    sprite = Sprite.Create(Texture, rec, new Vector2(0, 0));
                }
                return sprite;
            }
        }
        [NonSerialized]
        private Sprite sprite;

        /// <summary>
        /// Deserializes sprite by creating new texture
        /// </summary>
        /// <remarks>Must be run in Unity thread</remarks>
        public void Deserialize()
        {
            if (sprite == null || isSpriteDirty)
            {
                isSpriteDirty = false;
                Rect rec = new Rect(0, 0, Texture.width, Texture.height);
                sprite = Sprite.Create(Texture, rec, new Vector2(0, 0));
            }
        }
        public void SetImage(byte[] imageData)
        {
            isTextureDirty = true;
            isSpriteDirty = true;
            image = imageData;
            hasImage = (imageData != null);
        }

        //public override bool Equals(object obj)
        //{
        //    //Check for null and compare run-time types.
        //    if ((obj == null) || !this.GetType().Equals(obj.GetType()))
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        SerializableImage img = (SerializableImage)obj;
        //        return (img.image == image);
        //    }
        //}
        //public override int GetHashCode()
        //{
        //    return image.GetHashCode();
        //}
    }
}