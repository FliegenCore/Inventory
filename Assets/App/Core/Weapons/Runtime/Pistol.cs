using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace Core.Weapons
{
    public class Pistol : IWeapon
    {
        public event Action<int> OnBulletCountChange;

        private event Action<int> m_OnShoot;

        private WeaponData m_WeaponData;

        public WeaponData WeaponData
        {
            get => m_WeaponData;
            set => m_WeaponData = value;
        }

        public void Initialize(Action<int> onShoot)
        {
            OnBulletCountChange?.Invoke(m_WeaponData.BulletCount);
            m_OnShoot = onShoot;
        }

        public async void Shoot()
        {
            if (m_WeaponData.BulletCount <= 0)
            {
                Debug.Log("Нет патрон");
                return;
            }

            m_WeaponData.BulletCount--;
            await UniTask.WaitForSeconds(0.1f);

            m_OnShoot?.Invoke(m_WeaponData.Damage);
            OnBulletCountChange?.Invoke(m_WeaponData.BulletCount);
        }

        public void AddBulletInMagazine(int count)
        {
            m_WeaponData.BulletCount += count;
            OnBulletCountChange?.Invoke(m_WeaponData.BulletCount);
        }
    }
}
