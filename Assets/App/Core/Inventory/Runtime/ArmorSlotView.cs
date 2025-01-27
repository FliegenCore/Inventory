using UnityEngine;
using UnityEngine.UI;

namespace Core.Inventory
{

    public class ArmorSlotView : MonoBehaviour
    {
        [SerializeField] private Image m_ItemSprite;

        private int m_CurrentIndex;

        public int CurrentIndex => m_CurrentIndex;

        public void Initialize(int index)
        {
            m_CurrentIndex = index;
        }

        public void SetItemSprite(Sprite sprite)
        {
            m_ItemSprite.sprite = sprite;
        }
    }
}
