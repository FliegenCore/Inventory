using Core.Weapons;

namespace Core.Items
{
    public class Cartridges : IItem
    {
        private readonly int m_StuckCount;

        private IWeapon m_Weapon;

        public Cartridges(IWeapon weapon, int stuckCount)
        {
            m_Weapon = weapon;
            m_StuckCount = stuckCount;
        }

        public string GetAddedInfo()
        {
            string retString ;

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

        }

        private int CalcNeedCount()
        {
            int count = 0;

            count = m_Weapon.WeaponData.MagazineSize - m_Weapon.WeaponData.BulletCount;

            return count;
        }
    }
}
