using Assets;
using Common;
using Common.Utils;
using Core.Entitas;
using Core.UI;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Core.Inventory
{
    [Order(0)]
    public class InventoryController : MonoBehaviour, IControllerEntity
    {
        private event Action<SlotData[], SlotData[]> m_OnSlotsDataChanged;

        [Inject] private AssetLoader m_AssetLoader;
        [Inject] private CoreUIController m_CoreUIController;
        [Inject] private SaveSystem m_SaveSystem;
        [Inject] private EnemyController m_EnemyController;

        private InventoryView m_InventoryView;
        private PlayerInput m_PlayerInput;

        private SlotView m_CurrentSlotView;
        private SlotView m_NearestSlot;

        [SerializeField] private SlotData[] m_SlotsData;
        [SerializeField] private SlotData[] m_ArmorSlots;

        private List<string> m_ItemsName = new List<string>();

        public SlotData[] ArmorSlots => m_ArmorSlots;
        public SlotData[] SlotsData => m_SlotsData;

        public void PreInit()
        {
            FillItemsList();
            m_EnemyController.Unit.SubscribeOnDead(AddRandomItem);

            m_InventoryView = m_CoreUIController.CoreUI.InventoryView;
            m_PlayerInput = m_CoreUIController.CoreUI.PlayerInput;
            m_SlotsData = new SlotData[m_InventoryView.SlotsView.Length];
            m_ArmorSlots = new SlotData[m_InventoryView.ArmorSlots.Length];

            Result<SlotDataArray> slotsArray = m_SaveSystem.Load<SlotDataArray>("slots");

            if (slotsArray.IsExist)
            {
                m_SlotsData = slotsArray.Object.Slots;
            }
            else
            {
                for (int i = 0; i < m_SlotsData.Length; i++)
                {
                    m_SlotsData[i] = new SlotData();
                }
            }

            Result<SlotDataArray> armorSlotsArray = m_SaveSystem.Load<SlotDataArray>("armorSlots");

            if (armorSlotsArray.IsExist)
            {
                m_ArmorSlots = armorSlotsArray.Object.Slots;
            }
            else
            {
                for (int i = 0; i < m_ArmorSlots.Length; i++)
                {
                    m_ArmorSlots[i] = new SlotData();
                }
            }

            RefreshUI();
        }

        public void Init()
        {
            m_PlayerInput.OnStartDrag += CashDragItem;
            m_PlayerInput.OnEndDrag += TrySwitch;
            m_PlayerInput.OnDragging += FindNearestSlot;

            m_InventoryView.Initialize(m_AssetLoader);
            m_InventoryView.RefreshInventoryUI(m_SlotsData, m_ArmorSlots);

            m_OnSlotsDataChanged += m_InventoryView.RefreshInventoryUI;
        }

        public int GetCountInStuck(int index)
        {
            return m_SlotsData[index].ItemsCount;
        }

        public string GetId(int index)
        {
            return m_SlotsData[index].ItemId;
        }

        public void DeleteFullyItem(int index)
        {
            m_SlotsData[index].ItemsCount = 0;
            m_SlotsData[index].ItemId = string.Empty;

            m_OnSlotsDataChanged?.Invoke(m_SlotsData, m_ArmorSlots);
        }

        public void DeleteItem(int index, int count = 1)
        {
            m_SlotsData[index].ItemsCount -= count;

            if (m_SlotsData[index].ItemsCount <= 0)
            {
                DeleteFullyItem(index);
            }

            m_OnSlotsDataChanged?.Invoke(m_SlotsData, m_ArmorSlots);
        }

        public void RefreshUI()
        {
            m_OnSlotsDataChanged?.Invoke(m_SlotsData, m_ArmorSlots);
        }

        private void AddRandomItem()
        {
            foreach (var slot in m_SlotsData)
            {
                if (slot.ItemsCount > 0)
                {
                    continue;
                }

                var itemJson = m_AssetLoader.LoadSync<TextAsset>(m_ItemsName[UnityEngine.Random.Range(0, m_ItemsName.Count)]);

                ItemData itemData = JsonUtility.FromJson<ItemData>(itemJson.text);

                slot.ItemId = itemData.Id;
                slot.ItemsCount = itemData.StuckCount;

                break;
            }

            RefreshUI();
        }

        private void CashDragItem(IDragAndDrop dragAndDrop)
        {
            if (dragAndDrop is not SlotView)
            {
                return;
            }

            m_CurrentSlotView = dragAndDrop as SlotView;
            m_CurrentSlotView.transform.SetAsLastSibling();
        }

        private void TrySwitch()
        {
            if (m_CurrentSlotView == null)
            {
                return;
            }

            if (m_NearestSlot == null)
            {
                return;
            }

            SlotData slotData = m_SlotsData[m_CurrentSlotView.CurrentIndex];
            m_SlotsData[m_CurrentSlotView.CurrentIndex] = m_SlotsData[m_NearestSlot.CurrentIndex];
            m_SlotsData[m_NearestSlot.CurrentIndex] = slotData;

            m_OnSlotsDataChanged?.Invoke(m_SlotsData, m_ArmorSlots);

            m_CurrentSlotView.transform.SetAsFirstSibling();
            m_CurrentSlotView = null;
            m_NearestSlot = null;
        }

        private void FindNearestSlot()
        {
            float minDistance = int.MaxValue;
            float distance;

            foreach (var slot in m_InventoryView.SlotsView)
            {
                distance = (m_CurrentSlotView.ItemPosition - slot.transform.position).sqrMagnitude;

                if (distance > minDistance)
                {
                    continue;
                }

                if (distance <= minDistance)
                {
                    minDistance = distance;
                    m_NearestSlot = slot;
                }
            }
        }

        private void FillItemsList()
        {
            string folder = Directory.GetCurrentDirectory() + "\\Assets\\App\\Core\\Items\\Json";

            if (Directory.Exists(folder))
            {
                string[] jsonFiles = Directory.GetFiles(folder, "*.json");

                foreach (string filePath in jsonFiles)
                {
                    string fileName = Path.GetFileNameWithoutExtension(filePath);
                    m_ItemsName.Add(fileName);
                }
            }
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.I))
            {
                AddRandomItem();
            }
        }

        private void OnDestroy()
        {
            m_PlayerInput.OnStartDrag -= CashDragItem;
            m_PlayerInput.OnEndDrag -= TrySwitch;
            m_PlayerInput.OnDragging -= FindNearestSlot;
        }

        private void OnApplicationQuit()
        {
            SlotDataArray slotsArray = new SlotDataArray(m_SlotsData);
            m_SaveSystem.Save(slotsArray, "slots");

            SlotDataArray armorSlots = new SlotDataArray(m_ArmorSlots);
            m_SaveSystem.Save(armorSlots, "armorSlots");
        }
    }
}
