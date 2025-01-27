using Assets;
using Common;
using Common.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Inventory
{
    [Order(1)]
    public partial class ItemsController : MonoBehaviour, IControllerEntity
    {
        private readonly AssetName m_UseAssetName = "UseNameJson";
        private readonly AssetName m_GeneralAssetName = "GeneralJson";

        [Inject] private AssetLoader m_AssetLoader;

        private List<ItemData> m_ItemsData = new List<ItemData>();

        private TextAsset m_UseNameTextAsset;
        private Data m_UseNameData;

        private TextAsset m_GeneralTextAsset;
        private Data m_GeneralData;

        public void PreInit()
        {
            m_UseNameTextAsset = m_AssetLoader.LoadSync<TextAsset>(m_UseAssetName);
            m_GeneralTextAsset = m_AssetLoader.LoadSync<TextAsset>(m_GeneralAssetName);
        }

        public void Init()
        {
            m_UseNameData = JsonUtility.FromJson<Data>(m_UseNameTextAsset.text);
            m_GeneralData = JsonUtility.FromJson<Data>(m_GeneralTextAsset.text);
        }

        public Result<ItemData> GetItemData(string id)
        {
            Result<ItemData> resData = new Result<ItemData>();

            foreach (var item in m_ItemsData)
            {
                if (item.Id == id)
                {
                    resData = new Result<ItemData>(item, true);
                    return resData;
                }
            }

            TextAsset asset = m_AssetLoader.LoadSync<TextAsset>(id);
            ItemData itemData = JsonUtility.FromJson<ItemData>(asset.text);

            if (itemData != null)
            {
                resData = new Result<ItemData>(itemData, true);
                m_ItemsData.Add(itemData);
            }

            return resData;
        }

        public string GetUseName(string id)
        {
            string resName = "";

            foreach (var item in m_UseNameData.ItemsWrapper)
            {
                if (item.Key == id)
                {
                    resName = item.Value;
                    break;
                }
            }

            return resName;
        }

        public string GetItemName(string id)
        {
            string resName = "";

            foreach (var item in m_GeneralData.ItemsWrapper)
            {
                if (item.Key == id)
                {
                    resName = item.Value;
                    break;
                }
            }

            return resName;
        }
    }
}
