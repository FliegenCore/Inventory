using Assets;
using Common.Utils;
using Core.Common;
using Core.Inventory;
using Core.Items;
using Core.UI;
using UnityEngine;

namespace Core.Info
{
    [Order(10)]
    public class ItemInfoController : MonoBehaviour, IControllerEntity
    {
        [Inject] private ItemsController m_ItemsController;
        [Inject] private CoreUIController m_CoreUIController;
        [Inject] private AssetLoader m_AssetLoader;
        [Inject] private InventoryController m_InventoryController;
        [Inject] private EventManager m_EventManager;
        [Inject] private UseItemController m_UseItemController;
        [Inject] private PopupController m_PopupController;

        private ItemInfoView m_ItemInfoView;
        private UI.PlayerInput m_PlayerInput;

        private int m_CurrentIndexSlot;
        private string m_CurrentItemId;
        private bool m_IsItemSelected;

        public ItemInfoView ItemInfoView => m_ItemInfoView;
        public int CurrentIndexSlot => m_CurrentIndexSlot;
        public string CurrentItemId => m_CurrentItemId;
        public bool IsItemSelected => m_IsItemSelected; 

        public void PreInit()
        {
            var itemInfoViewAsset = m_AssetLoader.LoadGameObjectSync<ItemInfoView>("ItemInfoView");
            m_ItemInfoView = m_AssetLoader.InstantiateSync(itemInfoViewAsset, m_PopupController.PopupTransform);

            m_ItemInfoView.Initialize(m_EventManager);
            m_ItemInfoView.Disable();
            m_PlayerInput = m_CoreUIController.CoreUI.PlayerInput;
        }

        public void Init()
        {
            m_PlayerInput.OnClick += OpenWindow;

            m_EventManager.Subscribe<CloseItemInfoSignal>(this, DisableWindow);
        }

        private void OpenWindow(IDragAndDrop dragAndDrop)
        {
            if (dragAndDrop is not SlotView)
            {
                return;
            }

            SlotView currentSlot = (SlotView)dragAndDrop;

            if (currentSlot.ItemsCount <= 0)
            {
                return;
            }

            m_IsItemSelected = true;
            m_CurrentIndexSlot = currentSlot.CurrentIndex;
            Sprite itemSprite = LoadSprite(m_InventoryController.GetId(currentSlot.CurrentIndex) + "_sprite");
            m_CurrentItemId = m_InventoryController.GetId(currentSlot.CurrentIndex);

            Result<ItemData> itemDataRes = m_ItemsController.GetItemData(m_CurrentItemId);

            if (!itemDataRes.IsExist)
            {
                return;
            }


            Sprite icon = LoadSprite(itemDataRes.Object.Type + "_icon");
            string itemTextUse = m_ItemsController.GetUseName(itemDataRes.Object.Type);
            string itemName = m_ItemsController.GetItemName(itemDataRes.Object.Id);

            float weight = currentSlot.ItemsCount * itemDataRes.Object.Weight;

            m_EventManager.TriggerEvenet<EnableFadeSignal>();

            m_ItemInfoView
                .SetStatText(m_UseItemController.GetAddedInfo(itemDataRes.Object.Id))
                .SetItemName(itemName)
                .SetUseText(itemTextUse)
                .SetItemSprite(itemSprite)
                .SetIcon(icon)
                .SetWeight(weight)
                .Enable();
        }

        private void DisableWindow()
        {
            m_EventManager.TriggerEvenet<DisableFadeSignal>();
            m_ItemInfoView.Disable();
        }

        private Sprite LoadSprite(string id)
        {
            Sprite retSprite = null;

            retSprite = m_AssetLoader.LoadSync<Sprite>(id);

            return retSprite;
        }

        private void OnDestroy()
        {
            m_EventManager.Unsubscribe<CloseItemInfoSignal>(this);
            m_PlayerInput.OnClick -= OpenWindow;
        }
    }
}
