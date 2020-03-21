using System.IO;
using UnityEngine;

namespace Randomizer.ExternalFrameworks.Handlers
{
    public class ExternalJsonHandler
    {
        public static T LoadFromJson<T>(string jsonFilePath)
        {
            var jsonString = File.ReadAllText(jsonFilePath);
            return  JsonUtility.FromJson<T>(jsonString);
        }
        
        public static void LoadFromJson<T>(T data, string jsonFilePath)
        {
            var jsonString = File.ReadAllText(jsonFilePath);
            JsonUtility.FromJsonOverwrite(jsonString, data);
        }
        
        public static void SaveToJson<T>(T data, string jsonFilePath)
        {
            var jsonString = JsonUtility.ToJson(data);
            File.WriteAllText(jsonFilePath, jsonString);
        }

        public static bool IsJsonExist(string jsonFilePath)
        {
            return File.Exists(jsonFilePath);
        }

    }
}