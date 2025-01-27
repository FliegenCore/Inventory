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

        [Inject] private WeaponController m_WeaponController;
        [Inject] private EnemyController m_EnemyController;

        public Unit Unit => m_Unit;

        public void PreInit()
        {
            m_Unit.Initialize();

            m_Unit.SubscribeOnHealthChanged(m_HealthView.UpdateHealth);
            m_WeaponController.SubscribeOnShoot(DamageEnemy);
        }

        public void Init()
        {

        }

        private void DamageEnemy(int damage)
        {
            m_EnemyController.Unit.TakeDamage(damage, 0);
        }

        private void OnDestroy()
        {
            m_Unit.UnsubscribeOnHealthChanged(m_HealthView.UpdateHealth);
            m_WeaponController.UnsubscribeOnShoot(DamageEnemy);
        }
    }
}
