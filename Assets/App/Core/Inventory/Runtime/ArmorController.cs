using Assets;
using Core.Entitas;
using Core.Info;
using Core.Inventory;
using Core.Items;
using Core.UI;
using UnityEngine;

namespace Core.Armor
{
    [Order(1000)]
    public class ArmorController : MonoBehaviour, IControllerEntity
    {
        [Inject] private InventoryController m_InventoryController;
        [Inject] private UseItemController m_UseItemController;
        [Inject] private ItemInfoController m_ItemInfoController;
        [Inject] private PlayerController m_PlayerController;
        [Inject] private CoreUIController m_CoreUIController;
        [Inject] private AssetLoader m_AssetLoader;

        private CoreUI m_CoreUI;

        public void PreInit()
        {
            m_PlayerController.Unit.Head.OnArmorSet += SetHeadDataArmor;
            m_PlayerController.Unit.Body.OnArmorSet += BodyDataArmor;
            m_CoreUI = m_CoreUIController.CoreUI;
        }

        public void Init()
        {
            foreach (var armorSlot in m_InventoryController.ArmorSlots)
            {
                if (armorSlot.ItemsCount != 0)
                {
                    LoadArmor(armorSlot.ItemId);
                }
            }
        }

        private void SetHeadDataArmor(string id, int armorCount)
        {
            if (m_ItemInfoController.IsItemSelected)
            {
                SlotData data = m_InventoryController.ArmorSlots[0];
                m_InventoryController.ArmorSlots[0] = m_InventoryController.SlotsData[m_ItemInfoController.CurrentIndexSlot];
                m_InventoryController.SlotsData[m_ItemInfoController.CurrentIndexSlot] = data;
            }

            m_CoreUI.TopUI.HeadArmorSlot.SetArmorText(armorCount);
           
            m_InventoryController.RefreshUI();
        }

        private void BodyDataArmor(string id, int armorCount)
        {
            if (m_ItemInfoController.IsItemSelected)
            {
                SlotData data = m_InventoryController.ArmorSlots[1];
                m_InventoryController.ArmorSlots[1] = m_InventoryController.SlotsData[m_ItemInfoController.CurrentIndexSlot];
                m_InventoryController.SlotsData[m_ItemInfoController.CurrentIndexSlot] = data;
            }

            m_CoreUI.TopUI.BodyArmorSlot.SetArmorText(armorCount);
            
            m_InventoryController.RefreshUI();
        }

        private void LoadArmor(string id)
        {
            m_UseItemController.UseItem(id);
        }

        private void OnDestroy()
        {
            m_PlayerController.Unit.Head.OnArmorSet -= SetHeadDataArmor;
            m_PlayerController.Unit.Body.OnArmorSet -= BodyDataArmor;
        }
    }
}
