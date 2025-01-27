using Core.Common;
using Core.Entitas;
using Core.Info;
using Core.Inventory;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Items
{
    [Order(12)]
    public class UseItemController : MonoBehaviour, IControllerEntity
    {
        [Inject] private PlayerController m_PlayerController;
        [Inject] private ItemInfoController m_ItemInfoController;
        [Inject] private InventoryController m_InventoryController;
        [Inject] private EventManager m_EventManager;

        private Dictionary<string, IItem> m_ItemsActions = new Dictionary<string, IItem>();

        public void PreInit()
        {
            m_ItemsActions.Add("medkit", new Medkit(m_PlayerController.Unit, () =>
            {
                m_InventoryController.DeleteItem(m_ItemInfoController.CurrentIndexSlot);
            }));


            m_ItemsActions.Add("cap", new Armor(m_PlayerController.Unit.Head, "cap", 3));
            m_ItemsActions.Add("helmet", new Armor(m_PlayerController.Unit.Head, "helmet", 10));
            m_ItemsActions.Add("jacket", new Armor(m_PlayerController.Unit.Body, "jacket", 3));
            m_ItemsActions.Add("bulletproof", new Armor(m_PlayerController.Unit.Body, "bulletproof", 10));
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

        public string GetAddedInfo(string id)
        {
            if (!m_ItemsActions.ContainsKey(id))
            {
                return string.Empty;
            }

            return m_ItemsActions[id].GetAddedInfo();
        }

        private void OnDestroy()
        {
            m_ItemInfoController.ItemInfoView.UnsubscribeOnUseButton(DoAction);
        }
    }
}
