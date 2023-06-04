using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace DKP.SaveSystem
{
    public static class SaveManager
    {
        private const string FOLDER_DATA_PATH = "/Saves/";
        private static void ensureFoldersExist()
        {
            if (!Directory.Exists(Path(FOLDER_DATA_PATH)))
            {
                Directory.CreateDirectory(Path(FOLDER_DATA_PATH));
            }
        }

        /// <summary>
        /// Saves the IData object into a file located in Saves folder
        /// </summary>
        /// <param name="data">Any serializable object</param>
        /// <returns>true if success</returns>
        public static bool SaveGameData(IData data)
        {
            return SaveGameData(data, Path(FOLDER_DATA_PATH + data.DataFileName));
        }

        /// <summary>
        /// Saves the IData object into a file
        /// </summary>
        /// <param name="data">Any serializable object</param>
        /// <returns>true if success</returns>
        public static bool SaveGameData(IData data, string path)
        {
            ensureFoldersExist();
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);

            bf.Serialize(stream, data);
            stream.Close();
            return true;
        }

        /// <summary>
        /// Loads the file from path into a casted object
        /// </summary>
        /// <param name="dataFileName">Filename located in Saves folder</param>
        /// <returns>Data object or null</returns>
        public static IData LoadGameData<IData>(string dataFileName)
        {
            return LoadGameData<IData>(Path(FOLDER_DATA_PATH), dataFileName);
        }
        /// <summary>
        /// Loads the file from path into a casted object
        /// </summary>
        /// <param name="dataFileName">Filename located in Saves folder</param>
        /// <returns>Data object or null</returns>
        public static IData LoadGameData<IData>(string folderPath = "", string dataFileName = "", System.Action errorCallback = null)
        {
            ensureFoldersExist();
            string fpath = folderPath + dataFileName;
            if (File.Exists(fpath))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream stream = new FileStream(fpath, FileMode.Open);

                try
                {
                    IData data = (IData)bf.Deserialize(stream);
                    stream.Close();
                    return data;
                }
                catch (System.Exception)
                {
                    stream.Close();
                    Debug.LogWarning("Corrupted data");
                    errorCallback?.Invoke();
                    return default(IData);
                }
            }
            else
            {
                return default(IData);
            }
        }
        private static string Path(string path)
        {
            return Application.persistentDataPath + path;
        }
    }
}