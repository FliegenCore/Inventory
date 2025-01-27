using Core.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Inventory
{
    public class SlotView : MonoBehaviour, IDragAndDrop
    {
        [SerializeField] private ItemView m_ItemView;
        [SerializeField] private Image m_ItemSprite;
        [SerializeField] private TMP_Text m_ItemsCountText;

        private int m_ItemsCount;
        private int m_CurrentIndex;

        public int CurrentIndex => m_CurrentIndex;
        public int ItemsCount => m_ItemsCount;
        public Vector3 ItemPosition => m_ItemView.transform.position;

        public void Initialize(int slotIndex)
        {
            m_CurrentIndex = slotIndex;
        }

        public void UpdateSlotInfo(Sprite itemSprite, int itemCount)
        {
            m_ItemsCount = itemCount;

            if (m_ItemSprite.sprite != itemSprite)
            { 
                m_ItemSprite.sprite = itemSprite;
            }

            if (itemCount <= 1)
            {
                m_ItemsCountText.text = string.Empty;
            }
            else
            {
                m_ItemsCountText.text = $"{itemCount}";
            }
        }

        public void Drag(Vector2 position)
        {
            if (m_ItemsCount <= 0)
            {
                return;
            }

            m_ItemView.transform.position = position;
        }

        public void SetPosition(Vector2 position)
        {
            if (m_ItemsCount <= 0)
            {
                return;
            }

            m_ItemView.transform.position = position;
        }
    }
}
