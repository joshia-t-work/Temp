using System;
using System.Collections.Generic;

namespace DKP.SaveSystem.Data
{
    /// <summary>
    /// Represents an List of ImageContainer
    /// </summary>
    [Serializable]
    public class ImageContainerContainer
    {
        public List<SerializableImageContainer> ImageContainers = new List<SerializableImageContainer>();
        public SerializableImageContainer this[int index]
        {
            get
            {
                return ImageContainers[index];
            }
            set
            {
                ImageContainers[index] = value;
            }
        }
        public SerializableImageContainer this[string index]
        {
            get
            {
                return ImageContainers.Find(x => x.ObjectName == index);
            }
            set
            {
                ImageContainers[ImageContainers.FindIndex(x => x.ObjectName == index)] = value;
            }
        }

        public int Count => ImageContainers.Count;

        public SerializableImageContainer AddNew()
        {
            SerializableImageContainer newImageContainer = new SerializableImageContainer();
            ImageContainers.Add(newImageContainer);
            return newImageContainer;
        }

        public ImageContainerContainer()
        {

        }
    }
}