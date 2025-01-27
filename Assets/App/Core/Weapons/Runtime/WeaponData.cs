
using System;
using UnityEngine;

namespace Core.Weapons 
{
    [Serializable]
    public class WeaponData 
    {
        [SerializeField] private int m_MagazineSize;
        [SerializeField] private int m_Damage;
        [SerializeField] private int m_BulletCount;

        public int MagazineSize => m_MagazineSize;
        public int Damage => m_Damage;
        public int BulletCount
        {
            get => m_BulletCount;
            set => m_BulletCount = value;
        }
    }
}
