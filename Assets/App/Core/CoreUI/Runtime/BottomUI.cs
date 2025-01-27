using Core.Weapons;
using UnityEngine;

namespace Core.UI
{
    public class BottomUI : MonoBehaviour
    {
        [SerializeField] private WeaponCoreButton m_PistolButton;
        [SerializeField] private WeaponCoreButton m_RifleButton;
        [SerializeField] private CoreButton m_ShootButton;
        [SerializeField] private WeaponView m_PistolWeaponView;
        [SerializeField] private WeaponView m_RifleWeaponView;


        public WeaponCoreButton PistolButton => m_PistolButton;
        public WeaponCoreButton RifleButton => m_RifleButton;
        public CoreButton ShootButton => m_ShootButton;
        public WeaponView PistolWeaponView => m_PistolWeaponView;
        public WeaponView RifleWeaponView => m_RifleWeaponView;
    }
}
