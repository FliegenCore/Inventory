using Core.Entitas;
using Core.Info;
using Core.Inventory;
using UnityEngine;

namespace Core.Armor
{
    [Order(1000)]
    public class ArmorController : MonoBehaviour, IControllerEntity
    {
        [Inject] private InventoryController m_InventoryController;
        [Inject] private ItemInfoController m_ItemInfoController;
        [Inject] private PlayerController m_PlayerController;

        public void PreInit()
        {
            m_PlayerController.Unit.Head.OnArmorSet += SetHeadDataArmor;
            m_PlayerController.Unit.Body.OnArmorSet += SetBodyDataArmor;
        }

        public void Init()
        {
        }

        private void SetHeadDataArmor(string id)
        {
            SlotData data = m_InventoryController.ArmorSlots[0];
            m_InventoryController.ArmorSlots[0] = m_InventoryController.SlotsData[m_ItemInfoController.CurrentIndexSlot];
            m_InventoryController.SlotsData[m_ItemInfoController.CurrentIndexSlot] = data;

            m_InventoryController.RefreshUI();
        }

        private void SetBodyDataArmor(string id)
        {
            SlotData data = m_InventoryController.ArmorSlots[1];
            m_InventoryController.ArmorSlots[1] = m_InventoryController.SlotsData[m_ItemInfoController.CurrentIndexSlot];
            m_InventoryController.SlotsData[m_ItemInfoController.CurrentIndexSlot] = data;

            m_InventoryController.RefreshUI();
        }

        private void OnDestroy()
        {
            m_PlayerController.Unit.Head.OnArmorSet -= SetHeadDataArmor;
            m_PlayerController.Unit.Body.OnArmorSet -= SetBodyDataArmor;
        }
    }
}
