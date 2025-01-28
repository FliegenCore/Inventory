using Common;
using Common.Utils;
using Core.Inventory;
using Core.Weapons;
using UnityEngine;

namespace Core.Entitas
{
    [Order(-1)]
    public class PlayerController : MonoBehaviour, IControllerEntity
    {
        [SerializeField] private Unit m_Unit;
        [SerializeField] private HealthView m_HealthView;
        [SerializeField] private SlotView m_ArmorSlotView;

        [Inject] private SaveSystem m_SaveSystem;
        [Inject] private WeaponController m_WeaponController;
        [Inject] private EnemyController m_EnemyController;

        private HealthData m_HealthData;

        public Unit Unit => m_Unit;

        public void PreInit()
        {
            Result<HealthData> healthDataRes = m_SaveSystem.Load<HealthData>("PlayerHealthData");

            if (healthDataRes.IsExist)
            {
                m_HealthData = healthDataRes.Object;
            }
            else
            {
                m_HealthData = new HealthData();
                m_HealthData.CurrentHp = 100;
                m_HealthData.MaxHp = 100;
            }

            m_Unit.Initialize();

            m_Unit.SubscribeOnHealthChanged(m_HealthView.UpdateHealth);
            m_WeaponController.SubscribeOnShoot(DamageEnemy);
        }

        public void Init()
        {
            m_Unit.SetHealthData(m_HealthData);
        }

        private void DamageEnemy(int damage)
        {
            m_EnemyController.Unit.TakeDamage(damage, 0);
        }

        private void OnApplicationQuit()
        {
            m_SaveSystem.Save<HealthData>(m_HealthData, "PlayerHealthData");
        }

        private void OnDestroy()
        {
            m_Unit.UnsubscribeOnHealthChanged(m_HealthView.UpdateHealth);
            m_WeaponController.UnsubscribeOnShoot(DamageEnemy);
        }
    }
}
