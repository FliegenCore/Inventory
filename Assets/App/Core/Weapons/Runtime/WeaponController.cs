using Assets;
using Common;
using Core.Common;
using Core.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Weapons
{
    [Order(150)]
    public class WeaponController : MonoBehaviour, IControllerEntity
    {
        private event Action<int> m_OnShoot;

        [Inject] private EventManager m_EventManager;
        [Inject] private CoreUIController m_CoreUIController;
        [Inject] private AssetLoader m_AssetLoader;

        private IWeapon m_CurrentWeapon;

        private List<IWeapon> m_Weapons = new List<IWeapon>();
        private Pistol m_Pistol = new Pistol();
        private Rifle m_Rifle = new Rifle();

        public Pistol Pistol => m_Pistol;
        public Rifle Rifle => m_Rifle;  

        public void PreInit()
        {
            m_EventManager.Subscribe<SetWeaponSignal, IWeapon>(this, SetWeapon);

            m_Rifle.WeaponData = LoadWeaponData("Rifle");
            m_Pistol.WeaponData = LoadWeaponData("Pistol");

            m_Pistol.OnBulletCountChange += UpdatePistolView;
            m_Rifle.OnBulletCountChange += UpdateRifleView;

            m_Pistol.Initialize(m_OnShoot);
            m_Rifle.Initialize(m_OnShoot);

            m_Weapons.Add(m_Pistol);
            m_Weapons.Add(m_Rifle);
        }

        public void Init()
        {
            m_CoreUIController.CoreUI.BottomUI.PistolButton.Initialize(0);
            m_CoreUIController.CoreUI.BottomUI.RifleButton.Initialize(1);

            m_CoreUIController.CoreUI.BottomUI.PistolButton.OnClick += ChooseWeapon;
            m_CoreUIController.CoreUI.BottomUI.RifleButton.OnClick += ChooseWeapon;
            m_CoreUIController.CoreUI.BottomUI.ShootButton.OnClick += Shoot;
        }

        public void SubscribeOnShoot(Action<int> action)
        {
            m_OnShoot += action;
        }

        public void UnsubscribeOnShoot(Action<int> action)
        {
            m_OnShoot -= action;
        }

        private void UpdatePistolView(int count)
        {
            m_CoreUIController.CoreUI.BottomUI.PistolWeaponView.SetBulletText($"{count}/{m_Pistol.WeaponData.MagazineSize}");
        }

        private void UpdateRifleView(int count)
        {
            m_CoreUIController.CoreUI.BottomUI.RifleWeaponView.SetBulletText($"{count}/{m_Rifle.WeaponData.MagazineSize}");
        }

        private void ChooseWeapon(int index)
        {
            m_CurrentWeapon = m_Weapons[index];
        }

        private void SetWeapon(IWeapon weapon)
        {
            m_CurrentWeapon = weapon;
        }

        private void Shoot()
        {
            if(m_CurrentWeapon == null)
            {
                Debug.Log("Choose weapon");

                return;
            }

            m_CurrentWeapon.Shoot();
        }

        private WeaponData LoadWeaponData(AssetName assetName)
        {
            TextAsset jsonTextAsset = m_AssetLoader.LoadSync<TextAsset>(assetName);
            WeaponData weaponData = JsonUtility.FromJson<WeaponData>(jsonTextAsset.text);

            return weaponData;
        }

        private void OnDestroy()
        {
            m_EventManager.Unsubscribe<SetWeaponSignal>(this);
            m_Pistol.OnBulletCountChange -= UpdatePistolView;
            m_Rifle.OnBulletCountChange -= UpdateRifleView;
            m_CoreUIController.CoreUI.BottomUI.PistolButton.OnClick -= ChooseWeapon;
            m_CoreUIController.CoreUI.BottomUI.RifleButton.OnClick -= ChooseWeapon;
            m_CoreUIController.CoreUI.BottomUI.ShootButton.OnClick -= Shoot;
        }
    }
}
