using System.IO;
using UnityEngine;

namespace Common
{
    public class SaveSystem
    {
        private string m_SaveDirectory;

        public SaveSystem()
        {
            m_SaveDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Saves");

            if (!Directory.Exists(m_SaveDirectory))
            {
                Directory.CreateDirectory(m_SaveDirectory);
            }
        }

        public void Save<T>(T data, string fileName)
        {
            string filePath = Path.Combine(m_SaveDirectory, fileName + ".json");
            string json = JsonUtility.ToJson(data, true);

            File.WriteAllText(filePath, json);
        }

        public T Load<T>(string fileName)
        {
            string filePath = Path.Combine(m_SaveDirectory, fileName + ".json");

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                T data = JsonUtility.FromJson<T>(json);
                Debug.Log($"Loaded data from {filePath}");
                return data;
            }
            else
            {
                Debug.LogWarning($"File not found: {filePath}");
                return default;
            }
        }
    }
}
