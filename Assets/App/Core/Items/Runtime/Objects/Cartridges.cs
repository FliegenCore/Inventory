using Core.Info;
using Core.Inventory;
using Core.Weapons;
using System;

namespace Core.Items
{
    public class Cartridges : IItem
    {
        private int m_StuckCount;
        private event Action<int> m_Callback;

        private ItemInfoController m_ItemInfoController;
        private InventoryController m_InventoryController;
        private IWeapon m_Weapon;
        private int m_UsesCount;

        public Cartridges(IWeapon weapon,
            ItemInfoController itemInfoController,
            InventoryController inventoryController,
            Action<int> callback)
        {
            m_ItemInfoController = itemInfoController;
            m_InventoryController = inventoryController;
            m_Weapon = weapon;
            m_Callback = callback;
        }

        public string GetAddedInfo()
        {
            m_StuckCount = m_InventoryController.GetCountInStuck(m_ItemInfoController.CurrentIndexSlot);

            string retString;
            if (CalcNeedCount() > m_StuckCount)
            {
                retString = $"{m_StuckCount}";
            }
            else
            {
                retString = $"{CalcNeedCount()}";
            }

            return retString;
        }

        public void Use()
        {
            if (CalcNeedCount() > m_StuckCount)
            {
                m_UsesCount = m_StuckCount;
            }
            else
            {
                m_UsesCount = CalcNeedCount();
            }

            m_Weapon.AddBulletInMagazine(m_UsesCount);

            m_Callback?.Invoke(m_UsesCount);
        }

        private int CalcNeedCount()
        {
            int count = 0;

            count = m_Weapon.WeaponData.MagazineSize - m_Weapon.WeaponData.BulletCount;

            return count;
        }
    }
}
