using System;

namespace Core.Weapons
{
    public interface IWeapon
    {
        event Action<int> OnBulletCountChange;
        void Initialize(Action<int> action);
        WeaponData WeaponData { get; set; }
        void Shoot();
    }
}
