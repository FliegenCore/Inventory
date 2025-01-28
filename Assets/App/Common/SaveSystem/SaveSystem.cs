using Common.Utils;
using System.IO;
using System.Threading.Tasks;
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

        public Result<T> Load<T>(string fileName)
        {
            string filePath = Path.Combine(m_SaveDirectory, fileName + ".json");
            Result<T> result = new Result<T>();

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                T data = JsonUtility.FromJson<T>(json);
                result = new Result<T>(data, true);
            }

            return result;
        }

        public async Task DeleteSave()
        {
            await Task.Run(() =>
            {
                string[] jsonFiles = Directory.GetFiles(m_SaveDirectory, "*.json");

                foreach (string filePath in jsonFiles)
                {
                    File.Delete(filePath);
                }
            });
        }
    }
}
