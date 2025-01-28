using System;
using System.Collections.Generic;
using Common;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Assets
{
    public class AssetLoader 
    {
        private Dictionary<string, object> m_LoadedAssets = new Dictionary<string, object>();

        public async UniTask<T> LoadAsync<T>(AssetName key) 
        {
            if (m_LoadedAssets.ContainsKey(key.Name))
            {
                return (T)m_LoadedAssets[key.Name];
            }

            var opHandle = Addressables.LoadAssetAsync<T>(key.Name);

            await opHandle.ToUniTask();

            if (opHandle.Status == AsyncOperationStatus.Failed)
            {
                throw new Exception($"Не удалось загрузить ассет {key.Name}");
            }

            m_LoadedAssets.Add(key.Name, opHandle.Result);

            return opHandle.Result;
        }

        public T LoadSync<T>(AssetName key)
        {
            if (m_LoadedAssets.ContainsKey(key.Name))
            {
                return (T)m_LoadedAssets[key.Name];
            }

            var opHandle = Addressables.LoadAssetAsync<T>(key.Name);
            opHandle.WaitForCompletion();

            if (opHandle.Status == AsyncOperationStatus.Failed)
            {
                throw new Exception($"Не удалось загрузить ассет {key.Name}");
            }

            m_LoadedAssets.Add(key.Name, opHandle.Result);

            return opHandle.Result;
        }

        public T LoadGameObjectSync<T>(AssetName key)
        {
            var opHandle = Addressables.LoadAssetAsync<GameObject>(key.Name);
            opHandle.WaitForCompletion();

            if (opHandle.Status == AsyncOperationStatus.Failed)
            {
                throw new Exception($"Íå óäàëîñü çàãðóçèòü àññåò {key.Name}");
            }

            var obj = opHandle.Result.GetComponent<T>();

            if (obj == null)
            {
                throw new Exception($"Êîìïîíåíò òèïà {typeof(T)} íå íàéäåí íà çàãðóæåííîì GameObject {key.Name}");
            }

            return obj;
        }


        public T InstantiateSync<T>(T handle, Transform parent) where T : Component
        {
            T obj = handle;
            var result = UnityEngine.Object.Instantiate(obj, parent);

            if (result.GetComponent<T>())
            {
                return result.GetComponent<T>();
            }
            else
            {
                throw new Exception($"{nameof(T)} не найден");
            }
        }

        public T InstantiateSync<T>(T handle, Vector3 position, Quaternion rotation) where T : Component
        {
            T obj = handle;
            var result = UnityEngine.Object.Instantiate(obj, position, rotation);

            if (result.GetComponent<T>())
            {
                return result.GetComponent<T>();
            }
            else
            {
                throw new Exception($"{nameof(T)} не найден");
            }
        }

        public T InstantiateSync<T>(T handle, Vector3 position, Quaternion rotation, Transform parent) where T : Component
        {
            T obj = handle;
            var result = UnityEngine.Object.Instantiate(obj, position, rotation, parent);

            if (result.GetComponent<T>())
            {
                return result.GetComponent<T>();
            }
            else
            {
                throw new Exception($"{nameof(T)} не найден");
            }
        }
    }
}
