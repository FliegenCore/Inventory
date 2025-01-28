using UnityEngine;

namespace Core.UI
{
    public class TopUI : MonoBehaviour
    {
        [SerializeField] private ArmorSlotView m_HeadArmorSlot;
        [SerializeField] private ArmorSlotView m_BodyArmorSlot;

        public ArmorSlotView HeadArmorSlot => m_HeadArmorSlot;
        public ArmorSlotView BodyArmorSlot => m_BodyArmorSlot;
    }
}
