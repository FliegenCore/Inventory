using System;

namespace Core.Entitas
{
    public class BodyPart
    {
        public event Action<string, int> OnArmorSet;

        private int m_ArmorCount;
        private string m_ItemId;

        public int ArmorCount => m_ArmorCount;
        public string ItemId => m_ItemId;

        public void SetArmor(int armor, string itemId)
        {
            m_ArmorCount = armor;
            m_ItemId = itemId;

            OnArmorSet?.Invoke(m_ItemId, m_ArmorCount);
        }
    }
}
