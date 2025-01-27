using Assets;
using Common;
using Common.Utils;
using UnityEngine;

namespace Core.Inventory
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private SlotView[] m_Slots;
        [SerializeField] private SlotView[] m_ArmorSlots;

        private AssetLoader m_AssetLoader;

        public SlotView[] SlotsView => m_Slots;
        public SlotView[] ArmorSlots => m_ArmorSlots;

        public void Initialize(AssetLoader assetLoader)
        {
            m_AssetLoader = assetLoader;
            for (int i = 0; i < m_Slots.Length; i++)
            {
                m_Slots[i].Initialize(i);
            }
        }

        public void RefreshInventoryUI(SlotData[] slotData, SlotData[] armorSlots)
        {
            Refresh(slotData, m_Slots);
            Refresh(armorSlots, m_ArmorSlots);
        }

        private void Refresh(SlotData[] slotsData, SlotView[] slotsView)
        {
            for (int i = 0; i < slotsView.Length; i++)
            {
                if (slotsData[i].ItemId == null || slotsData[i].ItemId == string.Empty)
                {
                    slotsView[i].UpdateSlotInfo(null, 0);
                    continue;
                }

                Result<Sprite> spriteRes = LoadSprite(slotsData[i].ItemId + "_sprite");
                Sprite sprite = null;

                if (spriteRes.IsExist)
                {
                    sprite = spriteRes.Object;
                }

                slotsView[i].UpdateSlotInfo(sprite, slotsData[i].ItemsCount);
            }
        }

        private Result<Sprite> LoadSprite(AssetName assetName)
        {
            Result<Sprite> resultSprite = new Result<Sprite>();

            var sprite = m_AssetLoader.LoadSync<Sprite>(assetName);

            if (sprite != null)
            {
                resultSprite = new Result<Sprite>(sprite, true);
            }

            return resultSprite;
        }
    }
}
