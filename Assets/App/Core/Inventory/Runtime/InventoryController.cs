using Assets;
using Core.UI;
using System;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEditor;
using UnityEngine;

namespace Core.Inventory
{
    [Order(0)]
    public class InventoryController : MonoBehaviour, IControllerEntity
    {
        private event Action<SlotData[], SlotData[]> m_OnSlotsDataChanged;

        [Inject] private AssetLoader m_AssetLoader;
        [Inject] private CoreUIController m_CoreUIController;

        private InventoryView m_InventoryView;
        private PlayerInput m_PlayerInput;

        private SlotView m_CurrentSlotView;
        private SlotView m_NearestSlot;

        private SlotData[] m_SlotsData;
        private SlotData[] m_ArmorSlots;

        public SlotData[] ArmorSlots => m_ArmorSlots;
        public SlotData[] SlotsData => m_SlotsData;

        public void PreInit()
        {
            m_InventoryView = m_CoreUIController.CoreUI.InventoryView;
            m_PlayerInput = m_CoreUIController.CoreUI.PlayerInput;
            m_SlotsData = new SlotData[m_InventoryView.SlotsView.Length];
            m_ArmorSlots = new SlotData[m_InventoryView.ArmorSlots.Length];

            for (int i = 0; i < m_SlotsData.Length; i++)
            {
                m_SlotsData[i] = new SlotData();
            }

            for (int i = 0; i < m_ArmorSlots.Length; i++)
            {
                m_ArmorSlots[i] = new SlotData();
            }

            m_SlotsData[0].ItemsCount = 1;
            m_SlotsData[0].ItemId = "medkit";

            m_SlotsData[10].ItemsCount = 11;
            m_SlotsData[10].ItemId = "9x18";

            m_SlotsData[11].ItemId = "cap";
            m_SlotsData[11].ItemsCount = 1;

            m_SlotsData[12].ItemId = "cap";
            m_SlotsData[12].ItemsCount = 1;

            m_SlotsData[13].ItemId = "helmet";
            m_SlotsData[13].ItemsCount = 1;

            m_SlotsData[14].ItemId = "jacket";
            m_SlotsData[14].ItemsCount = 1;

            m_SlotsData[15].ItemId = "bulletproof";
            m_SlotsData[15].ItemsCount = 1;
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

        public void DeleteItem(int index)
        {
            m_SlotsData[index].ItemsCount--;

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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                foreach (var item in m_SlotsData)
                {
                    if (item.ItemsCount == 0 || item.ItemId == string.Empty || item.ItemId == "")
                    {
                        continue;
                    }
                    Debug.Log($"{item.ItemsCount}       {item.ItemId}");
                }
            }
        }

        private void OnDestroy()
        {
            m_PlayerInput.OnStartDrag -= CashDragItem;
            m_PlayerInput.OnEndDrag -= TrySwitch;
            m_PlayerInput.OnDragging -= FindNearestSlot;
        }
    }
}
