using Core.Common;
using Core.Entitas;
using Core.Info;
using Core.Inventory;
using Core.Weapons;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Items
{
    [Order(12)]
    public class UseItemController : MonoBehaviour, IControllerEntity
    {
        [Inject] private PlayerController m_PlayerController;
        [Inject] private WeaponController m_WeaponController;
        [Inject] private ItemInfoController m_ItemInfoController;
        [Inject] private InventoryController m_InventoryController;
        [Inject] private EventManager m_EventManager;

        private Dictionary<string, IItem> m_ItemsActions = new Dictionary<string, IItem>();

        public void PreInit()
        {
            m_ItemsActions.Add("medkit", new Medkit(m_PlayerController.Unit, RemoveItems));


            m_ItemsActions.Add("cap", new Armor(m_PlayerController.Unit.Head, "cap", 3));
            m_ItemsActions.Add("helmet", new Armor(m_PlayerController.Unit.Head, "helmet", 10));
            m_ItemsActions.Add("jacket", new Armor(m_PlayerController.Unit.Body, "jacket", 3));
            m_ItemsActions.Add("bulletproof", new Armor(m_PlayerController.Unit.Body, "bulletproof", 10));


            m_ItemsActions.Add("5.35x39", new Cartridges(m_WeaponController.Pistol,
                m_ItemInfoController, m_InventoryController, RemoveItems));
            m_ItemsActions.Add("9x18", new Cartridges(m_WeaponController.Rifle,
                m_ItemInfoController, m_InventoryController, RemoveItems));
        }

        public void Init()
        {
            m_ItemInfoController.ItemInfoView.SubscribeOnUseButton(DoAction);
        }

        public void DoAction()
        {
            m_ItemsActions[m_ItemInfoController.CurrentItemId].Use();

            m_EventManager.TriggerEvenet<CloseItemInfoSignal>();
        }

        public void UseItem(string id)
        {
            m_ItemsActions[id].Use();
        }

        public string GetAddedInfo(string id)
        {
            if (!m_ItemsActions.ContainsKey(id))
            {
                return string.Empty;
            }

            return m_ItemsActions[id].GetAddedInfo();
        }

        private void RemoveItems(int count)
        {
            m_InventoryController.DeleteItem(m_ItemInfoController.CurrentIndexSlot, count);
        }

        private void OnDestroy()
        {
            m_ItemInfoController.ItemInfoView.UnsubscribeOnUseButton(DoAction);
        }
    }
}
