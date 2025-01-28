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

        public void UpdateSlotInfo(Sprite itemSprite, int count)
        {
            m_ItemsCount = count;

            if (m_ItemSprite.sprite != itemSprite)
            { 
                m_ItemSprite.sprite = itemSprite;
            }

            if (m_ItemsCountText == null)
            {
                return;
            }

            if (count <= 1)
            {
                m_ItemsCountText.text = string.Empty;
            }
            else
            {
                m_ItemsCountText.text = $"{count}";
            }
        }

        public void UpdateCountText(int count)
        {
            m_ItemsCountText.text = $"{count}";
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
