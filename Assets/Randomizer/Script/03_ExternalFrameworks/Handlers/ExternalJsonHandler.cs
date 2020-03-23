using System.IO;
using UnityEngine;

namespace Randomizer.ExternalFrameworks.Handlers
{
    public class ExternalJsonHandler
    {
#if UNITY_ANDROID
        private static readonly string BasePath = Application.persistentDataPath;
#else
        private static readonly string BasePath = Application.dataPath;
#endif
        
        public static void LoadFromJson<T>(T data, string jsonFilename)
        {
            var path = GetFullPath(jsonFilename);
            var jsonString = File.ReadAllText(path);
            Debug.Log($"loading from json.. {path} : {jsonString}");
            JsonUtility.FromJsonOverwrite(jsonString, data);
        }
        
        public static void SaveToJson<T>(T data, string jsonFilename)
        {
            var path = GetFullPath(jsonFilename);
            var jsonString = JsonUtility.ToJson(data);
            Debug.Log($"saving to json.. {path} : {jsonString}");
            File.WriteAllText(path, jsonString);
        }

        public static bool IsJsonExist(string jsonFilename)
        {
            var path = GetFullPath(jsonFilename);
            return File.Exists(path);
        }

        private static string GetFullPath(string filename)
        {
            if (Directory.Exists(BasePath))
                Directory.CreateDirectory(BasePath);
            return BasePath + "/" + filename;
        }
    }
}