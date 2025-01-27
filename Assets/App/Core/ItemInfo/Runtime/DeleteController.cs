using Core.Common;
using Core.Info;
using Core.Inventory;
using UnityEngine;

namespace Core.UI
{
    [Order(1000)]
    public class DeleteController : MonoBehaviour, IControllerEntity
    {
        [Inject] private ItemInfoController m_ItemInfoController;
        [Inject] private InventoryController m_InventoryController;
        [Inject] private EventManager m_EventManager;

        public void PreInit()
        {
            m_ItemInfoController.ItemInfoView.SubscribeOnDeleteButton(DeleteItem);
        }

        public void Init()
        {

        }

        private void DeleteItem()
        {
            m_InventoryController.DeleteFullyItem(m_ItemInfoController.CurrentIndexSlot);
            m_EventManager.TriggerEvenet<CloseItemInfoSignal>();
        }

        private void OnDestroy()
        {
            m_ItemInfoController.ItemInfoView.UnsubscribeOnDeleteButton(DeleteItem);
        }
    }
}
