using Core.Entitas;
using Core.Items;
using UnityEngine;

namespace Core.Items
{
    public class Armor : IItem
    {
        private readonly int m_ArmorCount;
        private readonly string m_ItemId;

        private BodyPart m_BodyPart;

        public Armor(BodyPart bodyPart, string itemId, int armorCount)
        {
            m_BodyPart = bodyPart;
            m_ItemId = itemId;
            m_ArmorCount = armorCount;
        }

        public string GetAddedInfo()
        {
            return $"{m_ArmorCount}";
        }

        public void Use()
        {
            m_BodyPart.SetArmor(m_ArmorCount, m_ItemId);
        }
    }
}
